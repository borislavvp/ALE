using System.Collections.Generic;

namespace logic.FiniteAutomataService.Interfaces
{
    public interface IDerections : IDictionary<IState, IList<ILetter>>
    {

    }
}
