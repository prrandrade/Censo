namespace Censo.Domain.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces;

    [Table("Census_Ethnicity")]
    public class EthnicityModel : IModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
