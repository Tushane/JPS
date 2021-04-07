using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JPS.Models
{
    public class UserRoleManagerModel
    {
        [Key]
        public string UserId { get; set; }
        public string username { get; set; }
        public bool IsSelected { get; set; }
    }
}
