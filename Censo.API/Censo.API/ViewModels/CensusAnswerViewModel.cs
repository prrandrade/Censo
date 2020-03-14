namespace Censo.API.ViewModels
{
    using System.Collections.Generic;

    public class CensusAnswerViewModel
    {
        public CensusAnswerInfoViewModel Info { get; set; }

        public int RegionCode { get; set; }

        public int EthnicityCode { get; set; }

        public int SchoolingCode { get; set; }

        public IEnumerable<CensusAnswerInfoViewModel> ParentsInfo { get; set; }

        public IEnumerable<CensusAnswerInfoViewModel> ChildrenInfo { get; set; }
    }
}
