using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPS.Models
{
    public class Bill_Information
    {
      

        [Required]
        [Display(Name ="Bill ID")]
        [Key]
        public string bill_id { set; get; }

        [Required]
        [Display(Name = "Date Generated")]
        public DateTime date_generated { set; get; }

        [Required]
        [Display(Name ="Due Date")]
        public DateTime due_date { set; get; }

        
        public List<SeletedCustomer> cust_ids { set; get; }

        [Required]
        [Display(Name = "Customer ID")]
        public string cust_id { set; get; }

        //[Required]
        [Display(Name = "Premise ID")]
        public string premise_id { set; get; }

        //[Required]
        [Display(Name = "Customer Address")]
        public string address { set; get; }

        [Required]
        [Display(Name ="Due Amount")]
        public string amount { get; set; }

        [Required]
        [Display(Name = "Bill Status")]
        public string status { get; set; }


        public Bill_Information()
        {
            cust_ids = new List<SeletedCustomer>();
            address = "empty";
            premise_id = "00000";
        }
    }
}
