﻿namespace Censo.API
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Model;
    using ViewModels;

    public static class Mapping
    {
        public static AnswerModel ToAnswerModel(this AnswerInfoViewModel @this)
        {
            return new AnswerModel
            {
                FirstName = @this.FirstName,
                LastName = @this.LastName,
                RegionId = @this.RegionCode,
                GenderId = @this.GenderCode,
                SchoolingId = @this.SchoolingCode,
                EthnicityId = @this.EthnicityCode
            };
        }

        public static AnswerModel ToAnswerModel(this AnswerViewModel @this)
        {
            // filling the basic info about this person
            var census = @this.Info.ToAnswerModel();
            census.Parents = new List<AnswerParentChildModel>();
            census.Children = new List<AnswerParentChildModel>();
            return census;
        }

        public static IEnumerable<AnswerModel> RetrieveAnswerModelParents(this AnswerViewModel @this)
        {
            return @this.ParentsInfo?.Select(x => x.ToAnswerModel());
        }

        public static IEnumerable<AnswerModel> RetrieveAnswerModelChildren(this AnswerViewModel @this)
        {
            return @this.ChildrenInfo?.Select(x => x.ToAnswerModel());
        }

        public static AnswerViewModel ToAnswerViewModel(this AnswerModel @this, bool withParentChildrenInfo = true)
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
            };

            if (withParentChildrenInfo)
            {
                vm.ParentsInfo = @this.Parents?.Select(x => new AnswerInfoViewModel
                {
                    Id = x.Parent.Id,
                    FirstName = x.Parent.FirstName,
                    LastName = x.Parent.LastName,
                    GenderCode = x.Parent.GenderId,
                    SchoolingCode = x.Parent.SchoolingId,
                    EthnicityCode = x.Parent.EthnicityId,
                    RegionCode = x.Parent.RegionId
                });
                vm.ChildrenInfo = @this.Children?.Select(x => new AnswerInfoViewModel
                {
                    Id = x.Child.Id,
                    FirstName = x.Child.FirstName,
                    LastName = x.Child.LastName,
                    GenderCode = x.Child.GenderId,
                    SchoolingCode = x.Child.SchoolingId,
                    EthnicityCode = x.Child.EthnicityId,
                    RegionCode = x.Child.RegionId
                });
            }

            return vm;
        }

        public static IEnumerable<AnswerViewModel> ToAnswerViewModel(this IEnumerable<AnswerModel> @this, bool withParentChildrenInfo = true)
        {
            return @this.Select(x => x.ToAnswerViewModel(withParentChildrenInfo));
        }

        public static IEnumerable<IEnumerable<AnswerViewModel>> ToAnswerViewModel(this IEnumerable<IEnumerable<AnswerModel>> @this)
        {
            return @this.Select(x => x.ToAnswerViewModel(false));
        }
    }
}
