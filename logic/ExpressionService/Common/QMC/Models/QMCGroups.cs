using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.QMC.Models
{
    public class QMCGroups : SortedDictionary<int, List<QMCGroupData>>
    {
        public QMCGroups() : base() { }
        public QMCGroups(IComparer<int> capacity) : base(capacity) { }
    }
}
