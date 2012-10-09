using System;
using System.ComponentModel.DataAnnotations;

namespace MvcSample.Domain {
    public class Castle : Entity {

        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfConstruction { get; set; }
    }
}