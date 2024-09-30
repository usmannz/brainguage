using System;
using System.Collections.Generic;
using System.Text;

namespace SampleProject.Common.Entities
{
    public class Pager
    {
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; }
        public int FilterBy { get; set; }
        public int SortBy { get; set; }
        public int SortDirection { get; set; }
        public string FilterText { get; set; } = "";

        public string SortByField => ((SortFields)SortBy).ToString();
        public string SortDirectionField => ((SortDirection)SortDirection).ToString();
        public int SkipBy
        {
            get
            {
                return this.PageSize * (this.PageIndex - 1);
            }
        }
    }

}
