namespace Censo.UT.API
{
    using System.Collections.Generic;
    using Censo.API;
    using Censo.API.ViewModels;
    using Xunit;

    public class MappingTest
    {
        [Fact]
        public void ToAnswerModelTest()
        {
            // arrange
            var from = new AnswerViewModel
            {
                Info = new AnswerInfoViewModel
                {
                    EthnicityCode = 1,
                    FirstName = "First Name",
                    GenderCode = 2,
                    LastName = "Last Name",
                    RegionCode = 4,
                    SchoolingCode = 5
                }
            };

            // act
            var result = from.ToAnswerModel();

            // assert
            Assert.Equal(from.Info.EthnicityCode, result.EthnicityId);
            Assert.Equal(from.Info.FirstName, result.FirstName);
            Assert.Equal(from.Info.GenderCode, result.GenderId);
            Assert.Equal(from.Info.LastName, result.LastName);
            Assert.Equal(from.Info.RegionCode, result.RegionId);
            Assert.Equal(from.Info.SchoolingCode, result.SchoolingId);
        }
    }
}
