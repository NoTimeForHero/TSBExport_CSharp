using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TSBExport_CSharp.Grid
{
    public class GridDataTable : DataTable
    {
        public IEnumerable<string> Headers => Columns.Cast<DataColumn>().Select(x => x.Caption == "0" ? "#" : "Head_" + x.Caption);
        public IEnumerable<string> Footers => Columns.Cast<DataColumn>().Select(x => x.Caption == "0" ? "#" : "Foot_" + x.Caption);
    }
}
