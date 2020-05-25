namespace CQRSDemo.Requests
{
    public class AddPersonRequest
    {
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string NIN { get; set; }

        public string Dob { get; set; }
    }
}
