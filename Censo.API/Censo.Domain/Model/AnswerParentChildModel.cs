namespace Censo.Domain.Model
{
    public class AnswerParentChildModel
    {
        public int ParentId { get; set; }
        public AnswerModel Parent { get; set; }

        public int ChildId { get; set; }
        public AnswerModel Child { get; set; }
    }
}
