using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityExample.DomainClasses
{
    public class PageMetaDetail
    {
        [Key]
        public int Id { get; set; }
        public string PageUrl { get; set; }
        public string Title { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
    }
}
