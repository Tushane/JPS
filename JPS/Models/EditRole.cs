using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JPS.Models
{
    public class EditRole
    {
       
        public string id { set; get; }
        public string role_name { set; get; }
        public List<string> users { get; set; }

        public EditRole()
        {
            users = new List<string>();
        }
    }
}
