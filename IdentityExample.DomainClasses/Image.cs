using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityExample.DomainClasses
{
    public class Image
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
