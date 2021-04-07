﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace JPS.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the JPSUser class
    public class JPSUser : IdentityUser
    {

        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }


        [PersonalData]
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [PersonalData]
        [Required]
        [Column(TypeName ="nvarchar(100)")]
        public string premise_number { set; get; }

    }
}
