using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.QMC.Models
{
    public class QMCGroups : Dictionary<int, List<QMCGroupData>>
    {
        public QMCGroups() : base() { }
        public QMCGroups(int capacity) : base(capacity) { }
    }
}
