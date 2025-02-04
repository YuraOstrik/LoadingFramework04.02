using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loading.Framework29._01.Basic_class
{
    internal class Sale
    {
        public int StationeryId { get; set; }
        public int CustomerId { get; set; }
        public int ManagerId {  get; set; }

        public int Id { get; set; }

        public int Sold { get; set; }
        public decimal SalePrice { get; set; }
        public DateTime SaleDate { get; set; }


        public Stationery? Stationery { get; set; }
        public Customer? Customer { get; set; }
        public Manager? Manager { get; set; }
    }
}
