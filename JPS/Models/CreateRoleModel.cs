using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPS.Models
{
    public class CreateRoleModel
    {

        [Key]
        public string id { set; get; }

        [Required]
        [Display(Name ="Enter Role Name: ")]
        [DataType(DataType.Text)]
        public string role_name { set; get; }
    }
}
