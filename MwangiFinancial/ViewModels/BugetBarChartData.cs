using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MwangiFinancial.ViewModels
{
    public class BugetBarChartData
    {
        public string Budget { get; set; }
        public decimal Target { get; set; }
        public decimal Actual { get; set; }
    }
}