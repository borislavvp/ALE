using System;
using System.Collections.Generic;
using System.Text;

namespace logic.FiniteAutomataService.Extensions
{
    public static class TompsonConstructionExtensions
    {
        public static bool IsTompsonRule(this char value)
        {
            switch (value)
            {
                case '*':
                    return true;
                case '.':
                    return true;
                case '|':
                    return true;
                default:
                    return false;
            }
        }
    }
}
