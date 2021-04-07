using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPS.Models
{
    public class Payment_Details
    {
        //    [Required]
        //    public Bill_Information product_info { get; set; }

        public string prod_id { set; get; }

        [Required]
        [Display(Name ="Enter Card Number")]
        public string card_no { set; get; }

        [Required]
        [Display(Name = "Payment Description")]
        public string desc { set; get; }

        [Required]
        [Display(Name = "Bill Amount")]
        public string prod_amount { set; get; }


        [Required]
        [Display(Name = "Enter Card Holder Name")]
        public string card_holder { set; get; }

        [Required]
        [Display(Name = "Select Card Expiration Date")]
        public string exp_date { set; get; }

        [Required]
        [Display(Name = "Enter CSV")]
        public string csv { set; get; }

        [Required]
        [Display(Name = "Select Card Type")]
        public string card_type { set; get; }
    }
}
