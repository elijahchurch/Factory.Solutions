using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Factory.Models
{
    public class Machine
    {
        public int MachineId {get; set;}
        [Required(ErrorMessage = "A name must be entered!")]
        public string Name { get; set;}
        [Required(ErrorMessage = "A description must be entered!")]
        public string Description { get; set;}
        [Required(ErrorMessage= "Select the current condition of this machine from the radio button options.")]
        public string Condition {get; set;}
        public List<EngineerMachine> JoinEntities {get;}
    }
}