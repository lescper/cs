using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FxProductMonitor.Model
{
    public class MJLDProductRequest
    {
        public string timeStamp { get; set; }
        public string user { get; set; }
        public string password { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string goodsName { get; set; }
        public int SaleType { get; set; }
        public int AreaId { get; set; }
        public string AreaName { get; set; }
        public int ThemeId { get; set; }

    }
}
