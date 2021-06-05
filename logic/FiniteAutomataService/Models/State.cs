﻿using logic.FiniteAutomataService.Extensions;
using logic.FiniteAutomataService.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace logic.FiniteAutomataService.Models
{
    public class State : IState
    {
        public int Id { get; set; }
        public bool Initial { get; set; }
        public bool Final { get; set; }
        public string Value { get; }
        public Directions Directions { get; set; }

        public State(int Id, string Value)
        {
            this.Id = Id;
            this.Value = Value;
            this.Initial = false;
            this.Final = false;
            this.Directions = new Directions();
        } 
        public State(int Id, string Value, bool Initial)
        {
            this.Id = Id;
            this.Value = Value;
            this.Initial = Initial;
            this.Final = false;
            this.Directions = new Directions();
        } 
        
        public State(int Id, string Value, bool Initial,bool Final)
        {
            this.Id = Id;
            this.Value = Value;
            this.Initial = Initial;
            this.Final = Final;
            this.Directions = new Directions();
        }
       
        public override bool Equals(object obj)
        {
            return obj is State state && this.Id.Equals(state.Id);
        }

        public override int GetHashCode()
        {
            int hashCode = -157375006;
            hashCode = hashCode * -1521134295 + this.Id;
            hashCode = hashCode * -1521134295 + this.Id;
            return hashCode;
        }

        public int CompareTo([DisallowNull] IState other)
        {
            if (this.Id.Equals(other.Id))
            {
                return 0;
            }

            var currentContainsEpsilon = false;
            
            foreach (var item in this.Directions.Values)
            {
                if (item.Contains(Alphabet.EPSILON_LETTER))
                {
                    currentContainsEpsilon = true;
                }
            } 
            
            foreach (var otherItem in other.Directions.Values)
            {
                if (otherItem.Contains(Alphabet.EPSILON_LETTER))
                {
                    return currentContainsEpsilon 
                        ? this.Id.CompareTo(other.Id)
                        : -1;
                }
            }
            return this.Id.CompareTo(other.Id);
        }
        public bool CanReachStates(HashSet<IState> stateToReach)
        {
            Queue<IState> states = new Queue<IState>();
            HashSet<IState> statesTracker = new HashSet<IState>();
            states.Enqueue(this);

            while (states.Count > 0)
            {
                var current = states.Dequeue();
                foreach (var direction in current.Directions)
                {
                    if (!statesTracker.Contains(direction.Key))
                    {
                        if (stateToReach.Contains(direction.Key))
                            return true;

                        states.Enqueue(direction.Key);
                        statesTracker.Add(direction.Key);
                    }
                }
            }

            return false;
        }


        public HashSet<IState> FindEpsilonClosures()
        {
            var closures = new HashSet<IState>() { this };
            var queue = new Queue<IState>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                foreach (var direction in queue.Dequeue().Directions)
                {
                    if (direction.Value.Contains(Alphabet.EPSILON_LETTER) && !closures.Contains(direction.Key))
                    {
                        queue.Enqueue(direction.Key);
                        closures.Add(direction.Key);
                    }
                }
            }
            return closures;
        }

       public HashSet<string> GetAllPossibleWords()
       {
            return new HashSet<IState>().GatherAllPossibleWords(new HashSet<string>(),"",this);
       }
       public HashSet<IState> GetAllSelfReferencedStates()
       {
            var selfReferenced = new HashSet<IState>();
            var traversed = new HashSet<IState>();
            Queue<IState> queue = new Queue<IState>();
            queue.Enqueue(this);
            while (queue.Count > 0)
            {
                foreach (var direction in queue.Peek().Directions)
                {
                    if (!traversed.Contains(direction.Key))
                    {
                        if (direction.Key.Equals(queue.Peek())){ 
                            foreach (var letter in direction.Value)
                            {
                                if (!letter.IsEpsilon)
                                {
                                    selfReferenced.Add(queue.Peek());
                                    break;
                                }
                            }
                        }
                        traversed.Add(queue.Peek());
                        queue.Enqueue(direction.Key);
                    }
                }
                queue.Dequeue();
            }
            return selfReferenced;
       }
        public bool CheckWord(string word)
        {
            return this.CheckWordWithDirections(new Dictionary<WordCheckerTransitionKey, HashSet<IState>>(), this, word, 0);
        }

        private bool CheckWordWithDirections(Dictionary<WordCheckerTransitionKey, HashSet<IState>> tracker,
            IState state, string word, int fromLetter)
        {
            foreach (var direction in state.Directions)
            {
                var currentTransition = new WordCheckerTransitionKey(state, fromLetter);
                if (fromLetter < word.Length &&
                    (!tracker.ContainsKey(currentTransition) ||
                    !tracker[currentTransition].Contains(direction.Key)) &&
                     direction.Value.Contains(new Letter(word[fromLetter])))
                {
                    if (tracker.ContainsKey(currentTransition))
                    {
                        tracker[currentTransition].Add(direction.Key);
                    }
                    else
                    {
                        tracker.Add(currentTransition, new HashSet<IState>() { direction.Key });
                    }
                    if (CheckWordWithDirections(tracker, direction.Key, word, fromLetter + 1))
                    {
                        return true;
                    }
                }
                else if (
                    direction.Value.Contains(Alphabet.EPSILON_LETTER) &&
                    (!tracker.ContainsKey(currentTransition) ||
                    !tracker[currentTransition].Contains(direction.Key)))
                {
                    if (tracker.ContainsKey(currentTransition))
                    {
                        tracker[currentTransition].Add(direction.Key);
                    }
                    else
                    {
                        tracker.Add(currentTransition, new HashSet<IState>() { direction.Key });
                    }
                    if (CheckWordWithDirections(tracker, direction.Key, word, fromLetter))
                    {
                        return true;
                    }
                }
            }

            return state.Final && fromLetter >= word.Length;
        }

    }
}
