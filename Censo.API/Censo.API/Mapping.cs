namespace Censo.API
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model;
    using ViewModels;

    public static class Mapping
    {
        public static AnswerModel ToAnswerModel(this AnswerViewModel @this)
        {
            // filling the basic info about this person
            var census = new AnswerModel
            {
                FirstName = @this.Info.FirstName,
                LastName = @this.Info.LastName,
                RegionId = @this.Info.RegionCode,
                GenderId = @this.Info.GenderCode,
                SchoolingId = @this.Info.SchoolingCode,
                EthnicityId = @this.Info.EthnicityCode,
                Parents = new List<AnswerParentChildModel>(),
                Children = new List<AnswerParentChildModel>()
            };
            return census;
        }

        public static IEnumerable<AnswerModel> RetrieveAnswerModelParents(this AnswerViewModel @this)
        {
            return @this.ParentsInfo?.Select(x => new AnswerModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                GenderId = x.GenderCode
            });
        }

        public static IEnumerable<AnswerModel> RetrieveAnswerModelChildren(this AnswerViewModel @this)
        {
            return @this.ChildrenInfo?.Select(x => new AnswerModel
            {
                FirstName = x.FirstName,
                LastName = x.LastName,
                GenderId = x.GenderCode
            });
        }

        public static AnswerViewModel ToAnswerViewModel(this AnswerModel @this)
        {
            var vm = new AnswerViewModel
            {
                Info = new AnswerInfoViewModel
                {
                    Id = @this.Id,
                    FirstName = @this.FirstName,
                    LastName = @this.LastName,
                    GenderCode = @this.GenderId,
                    SchoolingCode = @this.SchoolingId,
                    EthnicityCode = @this.EthnicityId,
                    RegionCode = @this.RegionId
                },
                ParentsInfo = @this.Parents?.Select(x => new AnswerInfoViewModel
                {
                    Id = x.Parent.Id,
                    FirstName = x.Parent.FirstName,
                    LastName = x.Parent.LastName,
                    GenderCode = x.Parent.GenderId,
                    SchoolingCode = x.Parent.SchoolingId,
                    EthnicityCode = x.Parent.EthnicityId,
                    RegionCode = x.Parent.RegionId
                }),
                ChildrenInfo = @this.Children?.Select(x => new AnswerInfoViewModel
                {
                    Id = x.Child.Id,
                    FirstName = x.Child.FirstName,
                    LastName = x.Child.LastName,
                    GenderCode = x.Child.GenderId,
                    SchoolingCode = x.Child.SchoolingId,
                    EthnicityCode = x.Child.EthnicityId,
                    RegionCode = x.Child.RegionId
                })
            };

            return vm;
        }

        public static IEnumerable<AnswerViewModel> ToAnswerViewModel(this IEnumerable<AnswerModel> @this)
        {
            return @this.Select(x => x.ToAnswerViewModel());
        }
    }
}
