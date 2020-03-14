namespace Censo.Domain.Model
{
    using Interfaces;

    public class RegionModel : IModel
    {
        public int Id { get; set; }

        public string Value { get; set; }
    }
}
