using System.Collections.Generic;

namespace CQRSDemo.DTO
{
    public class PersonDTO
    {
        public string Name { get; set; }

        public string Dob { get; set; }

        public List<GoalDTO> Goals { get; set; }

    }
}
