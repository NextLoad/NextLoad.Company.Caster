using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caster.Model
{
    public partial class MemberInfo
    {
        public string Mtitle { get; set; }
    }

    public partial class DishInfo
    {
        public string DTypeTitle { get; set; }
    }

    public partial class TableInfo
    {
        public string THTitle { get; set; }
    }

    public partial class OrderDetailInfo
    {
        public string ODTitle { get; set; }
        public decimal ODPrice { get; set; }
        public int ODishId { get; set; }
    }
}
