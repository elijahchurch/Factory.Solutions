using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Engineer
    {
        public int EngineerId { get; set;}
        [Required(ErrorMessage = "A name must be entered!")]
        public string Name { get; set;}
        [Required(ErrorMessage = "A description must be entered!")]
        public string Description { get; set;}
        public List<EngineerMachine> JoinEntities {get;}
    }
}