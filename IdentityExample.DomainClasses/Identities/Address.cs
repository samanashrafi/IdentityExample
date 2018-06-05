﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityExample.DomainClasses
{
    public class Address
    {
        public int Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public virtual ICollection<ApplicationUser> ApplicationUsers { get; set; }
    }
}
