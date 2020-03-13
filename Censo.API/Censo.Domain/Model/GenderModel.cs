namespace Censo.Domain.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces;

    [Table("Census_Gender")]
    public class GenderModel : IModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }
    }
}
