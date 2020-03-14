namespace Censo.API
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model;
    using ViewModels;

    public static class Mapping
    {
        public static AnswerModel ToCensusAnswerModel(this CensusAnswerViewModel @this)
        {
            // filling the basic info about this person
            var census = new AnswerModel
            {
                FirstName = @this.Info.FirstName,
                LastName = @this.Info.LastName,
                RegionId = @this.RegionCode,
                GenderId = @this.Info.GenderCode,
                SchoolingId = @this.SchoolingCode,
                EthnicityId = @this.EthnicityCode,
                Parents = new List<AnswerParentChildModel>(),
                Children = new List<AnswerParentChildModel>()
            };
            return census;
        }

        public static IEnumerable<AnswerModel> RetrieveCensusAnswerModelParents(this CensusAnswerViewModel @this)
        {
            return @this.ParentsInfo?.Select(x => new AnswerModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                GenderId = x.GenderCode
            });
        }

        public static IEnumerable<AnswerModel> RetrieveCensusAnswerModelChildren(this CensusAnswerViewModel @this)
        {
            return @this.ChildrenInfo?.Select(x => new AnswerModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                GenderId = x.GenderCode
            });
        }
    }
}
