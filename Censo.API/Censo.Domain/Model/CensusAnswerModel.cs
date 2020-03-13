namespace Censo.Domain.Model
{
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces;

    [Table("Census")]
    public class CensusAnswerModel : IModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [ForeignKey("Census_Region")]
        public int RegionRefId { get; set; }
        public virtual RegionModel Region { get; set; }

        [ForeignKey("Census_Ethnicity")]
        public int EthnicityRefId { get; set; }
        public virtual EthnicityModel Ethnicity { get; set; }

        [ForeignKey("Census_Gender")]
        public int GenderRefId { get; set; }
        public virtual GenderModel Gender { get; set; }

        [ForeignKey("Census_Schooling")]
        public int SchoolingRefId { get; set; }
        public virtual SchoolingModel Schooling { get; set; }


        [ForeignKey("CensusAnswer")]
        public int? FatherRefId { get; set; }
        public virtual CensusAnswerModel Father { get; set; }

        [ForeignKey("CensusAnswer")]
        public int? MotherRefId { get; set; }
        public virtual CensusAnswerModel Mother { get; set; }
    }
}
