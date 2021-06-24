using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class DirectionValue
    {
        public ILetter Letter { get; set; }
        public ILetter LetterToPush { get; set; }
        public ILetter LetterToPop { get; set; }
        public bool ShouldPushToStack { get => PushDown && !LetterToPush.IsEpsilon; }
        public bool ShouldPopFromStack { get => PushDown && !LetterToPop.IsEpsilon; }
        public bool PushDown { get => LetterToPop != null && LetterToPush != null; }

        private ILetter PreviouslyPoppedLetter { get; set; }
        public override bool Equals(object obj)
        {
            return obj is DirectionValue value 
                && this.Letter.Equals(value.Letter) ;
        }

        public override int GetHashCode()
        {
            int hashCode = -157375006;
            hashCode = hashCode * -1521134295 + this.Letter.Value ;
            hashCode = hashCode * -1521134295 + this.Letter.Value ;
            return hashCode;
        }

        public bool PerformStackAction(ref Stack<ILetter> stack)
        {
            if (this.PushDown)
            {
                if (this.ShouldPopFromStack)
                {
                    if (stack.Count == 0)
                    {
                        return false;
                    }
                    this.PreviouslyPoppedLetter = stack.Pop();
                }
                if (this.ShouldPushToStack)
                {
                    stack.Push(this.LetterToPush);
                }
            }
            return true;
        }

        public bool UndoStackAction(ref Stack<ILetter> stack)
        {
            if (this.PushDown)
            {
                if (this.ShouldPushToStack)
                {
                    if(stack.Count == 0)
                    {
                        return false;
                    }
                    stack.Pop();
                }
                if (this.ShouldPopFromStack && this.PreviouslyPoppedLetter != null)
                {
                    stack.Push(this.PreviouslyPoppedLetter);
                }
            }
            return true;
        }
    }
}
