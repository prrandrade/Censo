namespace Censo.API.ViewModels
{
    using System.Collections.Generic;

    public class AnswerViewModel
    {
        public AnswerInfoViewModel Info { get; set; }

        public IEnumerable<AnswerInfoViewModel> ParentsInfo { get; set; }

        public IEnumerable<AnswerInfoViewModel> ChildrenInfo { get; set; }
    }
}
