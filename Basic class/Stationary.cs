using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loading.Framework29._01.Basic_class
{
    internal class Stationery
    {
        public int Id {  get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; }
        public int Num { get; set; }
        public decimal Price { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
