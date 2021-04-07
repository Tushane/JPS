using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPS.Models
{
    public class SeletedCustomer
    {
        public string cust_id { set; get; }
        public bool selected { set; get; }

        public SeletedCustomer()
        {
            selected = false;
        }
    }
}
