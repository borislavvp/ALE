using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.QMC.Models
{
    public class QMCGroupData
    {
        public int OriginRow { get; set; }
        public int[] Rows { get; set; }
        public string RowData { get; set; }

        public QMCGroupData()
        {
            this.Rows = new int[0];
        }
        public QMCGroupData(int Origin, string RowData)
        {
            this.OriginRow = Origin;
            this.Rows = new int[1] { Origin };
            this.RowData = RowData;
        }
        public QMCGroupData(int[] Rows, string RowData)
        {
            this.OriginRow = Rows[0];
            this.Rows = Rows;
            this.RowData = RowData;
        }

        public override bool Equals(object obj)
        {
            return obj is QMCGroupData data &&
                   RowData == data.RowData;
        }

        public int GetAbsoluteRowComparableIndex()
        {
            return this.Rows[this.Rows.Length-1] - this.Rows[0];
        }

        public int NumberOfRows()
        {
            return this.Rows.Length;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(RowData);
        }

        public static bool operator ==(QMCGroupData left, QMCGroupData right)
        {
            return EqualityComparer<QMCGroupData>.Default.Equals(left, right);
        }

        public static bool operator !=(QMCGroupData left, QMCGroupData right)
        {
            return !(left == right);
        }
    }
}
