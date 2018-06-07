using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityExample.DomainClasses
{
    [Table("SubFreeContent")]
    public class SubFreeContent
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(256)]
        public string Title { get; set; }
        
        [MaxLength(256)]
        public string EnTitle { get; set; }

        [MaxLength(512)]
        public string Icon { get; set; }
        public string LastEdit { get; set; }
        //public DateTime RegisterDate { get; set; } = DateTime.Now;
        public int LikeCount { get; set; }
        public int DisLikeCount { get; set; }
        public bool Condition { get; set; }
        public string PageTitle { get; set; }
        [MaxLength(65)]
        public string MetaKeyword { get; set; }
        [MaxLength(256)]
        public string MetaDescription { get; set; }
        [MaxLength(256)]
        public string ShortDescription { get; set; }
        public string ShortDescription2 { get; set; }
        public string LongDescription { get; set; }
        public string LastCommentDate { get; set; }
        public string LastDisLikeDate { get; set; }
        public string LastLikeDate { get; set; }
        public string LastView { get; set; }

        public Image Image { get; set; }

        [Display(Name = "FreeContent")]
        public int FreeContentId { get; set; }

        [ForeignKey("FreeContentId")]
        public virtual FreeContent FreeContent { get; set; }
    }
}
