namespace MentorshipWebApplication.Models
{
    public class UserValidationModel
    {
        //private readonly DigitalBooksContext _context=new DigitalBooksContext();
        public string userName { get; set; }
        public string password { get; set; }
        public bool validateCredential(string userName, string password)
        {
            
            if(userName != null && password !=null)
            {
                if (userName == "abc" && password == "123")
                {
                    return true;
                }
            }

            return false;
        }

    }
}
