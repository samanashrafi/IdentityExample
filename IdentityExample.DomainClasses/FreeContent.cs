using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace IdentityExample.DomainClasses
{
    public class FreeContent
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام را وارد نمایید")]
        [MaxLength(20, ErrorMessage ="نام باید کمتر از 20 کاراکتر باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage = "نام انگلیسی را وارد نمایید")]
        [MaxLength(20, ErrorMessage = "نام باید کمتر از 20 کاراکتر باشد")]
        public string TitleEn { get; set; }

        [Required(ErrorMessage ="نوع را تعیین نمایید")]
        public string Type { get; set; }

        public string Icon { get; set; } 
               
        public string PageTitle { get; set; }

        [MaxLength(65)]
        public string MetaKeyword { get; set; }

        [MaxLength(256)]
        public string MetaDescription { get; set; }

        [MaxLength(256)]
        public string ShortDescription { get; set; }

        public string LastCommentDate { get; set; }

        public string LastDisLikeDate { get; set; }

        public string LastLikeDate { get; set; }

        public string LastEdit { get; set; }

        public string LastView { get; set; }

       public bool Condition { get; set; }

        public virtual IEnumerable<SubFreeContent> SubFreeContents { get; set; }
    }
}
