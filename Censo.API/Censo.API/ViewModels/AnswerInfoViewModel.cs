namespace Censo.API.ViewModels
{
    public class AnswerInfoViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? GenderCode { get; set; }

        public int? RegionCode { get; set; }

        public int? EthnicityCode { get; set; }

        public int? SchoolingCode { get; set; }
    }
}
