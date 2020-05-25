using System;
using CQRSDemo.Requests;

namespace CQRSDemo.Commands
{
    public class AddPersonCommand
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string NIN { get; set; }

        public DateTime Dob { get; set; }

        public static AddPersonCommand ConvertFromRequest(AddPersonRequest request)
        {
            return new AddPersonCommand {
                FirstName = request.FirstName,
                MiddleName = request.MiddleName,
                LastName = request.LastName,
                NIN = request.NIN,
                Dob = DateTime.Parse(request.Dob)
            };
        }
    }
}
