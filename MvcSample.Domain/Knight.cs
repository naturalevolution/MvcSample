using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MvcSample.Resources.Models;
using MvcSample.Resources.Shared;

namespace MvcSample.Domain {
    public class Knight : Person {

        public Knight() { }

        public Knight(string firstName, string lastName, string email) : base(firstName, lastName) {
            Email = email;
            DateOfKnighthood = DateTime.Now;
        }

        [DataType(DataType.Date)]
        [Display(Name = "DateOfKnighthood", ResourceType = typeof(PersonResource))]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfKnighthood { get; set; }


        [Required(ErrorMessageResourceName = "Error_Field_Required", ErrorMessageResourceType = typeof(Messages))]
        [DataType(DataType.EmailAddress, ErrorMessageResourceName = "Error_Field_Email", ErrorMessageResourceType = typeof(Messages))]
        [Display(Name = "Email", ResourceType = typeof(PersonResource))]
        [StringLength(20, ErrorMessageResourceName = "Error_Field_Size", ErrorMessageResourceType = typeof(Messages))]
        public string Email { get; set; }

        public virtual ICollection<Princess> PrincessesToResque { get; set; }

        public string DisplayDateOfKnighthood() {
            return DateOfKnighthood.ToString("dd/MM/yyyy");
        }
    }
}