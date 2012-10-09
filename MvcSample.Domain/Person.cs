using System;
using System.ComponentModel.DataAnnotations;
using MvcSample.Resources.Models;
using MvcSample.Resources.Shared;

namespace MvcSample.Domain {
    public class Person : Entity {

        [Required(ErrorMessageResourceName = "Error_Field_Required", ErrorMessageResourceType = typeof(Messages))]
        [DataType(DataType.Text)]
        [Display(Name = "FirstName", ResourceType = typeof(PersonResource))]
        [StringLength(50, ErrorMessageResourceName = "Error_Field_Size", ErrorMessageResourceType = typeof(Messages))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "Error_Field_Required", ErrorMessageResourceType = typeof(Messages))]
        [DataType(DataType.Text)]
        [Display(Name = "LastName", ResourceType = typeof(PersonResource))]
        [StringLength(150, ErrorMessageResourceName = "Error_Field_Size", ErrorMessageResourceType = typeof(Messages))]
        public string LastName { get; set; }

        protected Person (string firstName, string lastName) {
            LastName = lastName;
            FirstName = firstName;
        }

        protected Person() {}
    }
}