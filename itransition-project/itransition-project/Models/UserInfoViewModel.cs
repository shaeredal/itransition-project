﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace itransition_project.Models
{
    public class UserInfoViewModel
    {
       public ApplicationUser Profile { get; set; }
        public  IEnumerable<Comment> Comments { get; set; }
    }
    
}