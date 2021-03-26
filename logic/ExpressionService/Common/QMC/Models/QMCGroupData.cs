using System;
using System.Collections.Generic;
using System.Text;

namespace logic.ExpressionService.Common.QMC.Models
{
    public class QMCGroupData
    {
        public int OriginRow { get; set; }
        public string[] Rows { get; set; }
        public string RowData { get; set; }

        public QMCGroupData()
        {

        }
        public QMCGroupData(int Origin, string RowData)
        {
            this.OriginRow = Origin;
            this.Rows = new string[1] { $@"{Origin}" };
            this.RowData = RowData;
        }
        public QMCGroupData(string[] Rows, string RowData)
        {
            this.OriginRow = Int32.Parse(Rows[0]);
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
            if(this.Rows[this.Rows.Length - 1] == "3" && this.Rows[0] == "0")
            {
                var asd = 0;
            }
            return Int32.Parse(this.Rows[this.Rows.Length-1]) - Int32.Parse(this.Rows[0]);
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
