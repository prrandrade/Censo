namespace Censo.Domain.Model
{
    using System.Collections.Generic;
    using Interfaces;

    public class AnswerModel : IModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? RegionId { get; set; }

        public virtual RegionModel Region { get; set; }

        public int? EthnicityId { get; set; }

        public virtual EthnicityModel Ethnicity { get; set; }

        public int GenderId { get; set; }

        public virtual GenderModel Gender { get; set; }

        public int? SchoolingId { get; set; }

        public virtual SchoolingModel Schooling { get; set; }

        public List<AnswerParentChildModel> Parents { get; set; }

        public List<AnswerParentChildModel> Children { get; set; }
    }
}
