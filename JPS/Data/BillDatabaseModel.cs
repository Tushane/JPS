using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPS.Data
{
    public class BillDatabaseModel
    {

        [Key]
        public string bill_id { set; get; }


        public DateTime date_generated { set; get; }

        public DateTime due_date { set; get; }

        
        public string cust_id { set; get; }
        public string premise_id { set; get; }
        public string address { set; get; }
        public string amount { get; set; }

        public string status { get; set; }
    }
}
