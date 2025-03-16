namespace BusinessReportingMVC.ViewModels
{
    public class PersonalInfoViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public bool? Approved { get; set; }

        public string Position { get; set; }
        public string Role { get; set; }
    }
}
