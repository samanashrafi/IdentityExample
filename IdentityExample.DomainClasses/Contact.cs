using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace IdentityExample.DomainClasses
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نام و نام خانوادگی را وارد نمایید")]
        [MaxLength(20, ErrorMessage = "نام و نام خانوادگی باید کمتر از 50 کاراکتر باشد")]
        public string Name { get; set; }

        [Required(ErrorMessage = "عنوان را وارد نمایید")]
        [MaxLength(20, ErrorMessage = "عنوان باید کمتر از 20 کاراکتر باشد")]
        public string Title { get; set; }

        [Required(ErrorMessage ="لطفا ایمیل را وارد نمایید")]
        public string Email { get; set; }
        
        [MaxLength(300)]
        public string Message { get; set; }
    }
}
