namespace Censo.Domain.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces;

    [Table("Census_Schooling")]
    public class SchoolingModel : IModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
