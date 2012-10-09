using System.ComponentModel.DataAnnotations;

namespace MvcSample.Domain {
    public class Entity {

        [Key]
        public int Id { get; set; }
    }
}