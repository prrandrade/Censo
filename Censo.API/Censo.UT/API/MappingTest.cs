namespace Censo.UT.API
{
    using System.Collections.Generic;
    using System.Linq;
    using Censo.API;
    using Censo.API.ViewModels;
    using Domain.Model;
    using Microsoft.CodeAnalysis;
    using Xunit;

    public class MappingTest
    {
        [Fact]
        public void FromAnswerInfoViewModel_ToAnswerModel()
        {
            // arrange
            var from = new AnswerInfoViewModel
            {
                EthnicityCode = 1,
                FirstName = "First Name",
                GenderCode = 2,
                LastName = "Last Name",
                RegionCode = 4,
                SchoolingCode = 5
            };

            // act
            var result = from.ToAnswerModel();

            // assert
            Assert.Equal(from.EthnicityCode, result.EthnicityId);
            Assert.Equal(from.FirstName, result.FirstName);
            Assert.Equal(from.GenderCode, result.GenderId);
            Assert.Equal(from.LastName, result.LastName);
            Assert.Equal(from.RegionCode, result.RegionId);
            Assert.Equal(from.SchoolingCode, result.SchoolingId);
        }

        [Fact]
        public void FromAnswerViewModel_ToAnswerModelTest()
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
            Assert.Empty(result.Parents);
            Assert.Empty(result.Children);
        }

        [Fact]
        public void FromIEnumerableAnswerViewModel_RetrieveAnswerModelParents_WithParents()
        {
            // arrange
            var from = new AnswerViewModel
            {
                ParentsInfo = new List<AnswerInfoViewModel>()
                {
                    new AnswerInfoViewModel
                    {
                        EthnicityCode = 1,
                        FirstName = "First Name Parent",
                        GenderCode = 2,
                        LastName = "Last Name Parent",
                        RegionCode = 4,
                        SchoolingCode = 5
                    },
                    new AnswerInfoViewModel
                    {
                        EthnicityCode = 6,
                        FirstName = "First Name Parent 2",
                        GenderCode = 7,
                        LastName = "Last Name Parent 2",
                        RegionCode = 8,
                        SchoolingCode = 9
                    }
                }
            };

            // act
            var result = from.RetrieveAnswerModelParents();

            // assert
            var parentsInfoOrigin = from.ParentsInfo.ToList();
            var parentsInfoDestiny = result.ToList();

            for (var i = 0; i < from.ParentsInfo.Count(); i++)
            {
                Assert.Equal(parentsInfoOrigin[i].EthnicityCode, parentsInfoDestiny[i].EthnicityId); ;
                Assert.Equal(parentsInfoOrigin[i].FirstName, parentsInfoDestiny[i].FirstName);
                Assert.Equal(parentsInfoOrigin[i].GenderCode, parentsInfoDestiny[i].GenderId);
                Assert.Equal(parentsInfoOrigin[i].LastName, parentsInfoDestiny[i].LastName);
                Assert.Equal(parentsInfoOrigin[i].RegionCode, parentsInfoDestiny[i].RegionId);
                Assert.Equal(parentsInfoOrigin[i].SchoolingCode, parentsInfoDestiny[i].SchoolingId);
            }
        }

        [Fact]
        public void FromIEnumerableAnswerViewModel_RetrieveAnswerModelParents_WithoutParents()
        {
            // arrange
            var from = new AnswerViewModel();

            // act
            var result = from.RetrieveAnswerModelChildren();

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void FromIEnumerableAnswerViewModel_RetrieveAnswerModelChildren_WithChildren()
        {
            // arrange
            var from = new AnswerViewModel
            {
                ChildrenInfo = new List<AnswerInfoViewModel>()
                {
                    new AnswerInfoViewModel
                    {
                        EthnicityCode = 1,
                        FirstName = "First Name Child",
                        GenderCode = 2,
                        LastName = "Last Name Child",
                        RegionCode = 4,
                        SchoolingCode = 5
                    },
                    new AnswerInfoViewModel
                    {
                        EthnicityCode = 6,
                        FirstName = "First Name Child 2",
                        GenderCode = 7,
                        LastName = "Last Name Child 2",
                        RegionCode = 8,
                        SchoolingCode = 9
                    }
                }
            };

            // act
            var result = from.RetrieveAnswerModelChildren();

            // assert
            var childrenInfoOrigin = from.ChildrenInfo.ToList();
            var childrenInfoDestiny = result.ToList();

            for (var i = 0; i < from.ChildrenInfo.Count(); i++)
            {
                Assert.Equal(childrenInfoOrigin[i].EthnicityCode, childrenInfoDestiny[i].EthnicityId); ;
                Assert.Equal(childrenInfoOrigin[i].FirstName, childrenInfoDestiny[i].FirstName);
                Assert.Equal(childrenInfoOrigin[i].GenderCode, childrenInfoDestiny[i].GenderId);
                Assert.Equal(childrenInfoOrigin[i].LastName, childrenInfoDestiny[i].LastName);
                Assert.Equal(childrenInfoOrigin[i].RegionCode, childrenInfoDestiny[i].RegionId);
                Assert.Equal(childrenInfoOrigin[i].SchoolingCode, childrenInfoDestiny[i].SchoolingId);
            }
        }

        [Fact]
        public void FromIEnumerableAnswerViewModel_RetrieveAnswerModelParents_WithoutChildren()
        {
            // arrange
            var from = new AnswerViewModel();

            // act
            var result = from.RetrieveAnswerModelChildren();

            // assert
            Assert.Null(result);
        }

        [Fact]
        public void FromAnwserModel_ToAnswerViewModel_WithParentsAndChildren()
        {
            // arrange
            var from = new AnswerModel
            {
                Id = 1,
                RegionId = 2,
                GenderId = 3,
                SchoolingId = 4,
                EthnicityId = 5,
                FirstName = "First Name",
                LastName = "Last Name",
                Parents = new List<AnswerParentChildModel>()
                {
                    new AnswerParentChildModel
                    {
                        Parent = new AnswerModel()
                        {
                            Id = 6,
                            RegionId = 7,
                            GenderId = 8,
                            SchoolingId = 9,
                            EthnicityId = 10,
                            FirstName = "First Name Parent",
                            LastName = "Last Name Parent",
                        }
                    }
                },
                Children = new List<AnswerParentChildModel>()
                {
                    new AnswerParentChildModel
                    {
                        Child = new AnswerModel()
                        {
                            Id = 11,
                            RegionId = 12,
                            GenderId = 13,
                            SchoolingId = 14,
                            EthnicityId = 15,
                            FirstName = "First Name Children",
                            LastName = "Last Name Children",
                        }
                    }
                }
            };

            // act
            var result = from.ToAnswerViewModel();

            // assert
            var childrenInfo = result.ChildrenInfo.ToList();
            var parentInfo = result.ParentsInfo.ToList();

            Assert.Equal(result.Info.Id, from.Id);
            Assert.Equal(result.Info.EthnicityCode, from.EthnicityId);
            Assert.Equal(result.Info.GenderCode, from.GenderId);
            Assert.Equal(result.Info.SchoolingCode, from.SchoolingId);
            Assert.Equal(result.Info.RegionCode, from.RegionId);
            Assert.Equal(result.Info.FirstName, from.FirstName);
            Assert.Equal(result.Info.LastName, from.LastName);

            Assert.Equal(childrenInfo[0].Id, from.Children[0].Child.Id);
            Assert.Equal(childrenInfo[0].EthnicityCode, from.Children[0].Child.EthnicityId);
            Assert.Equal(childrenInfo[0].GenderCode, from.Children[0].Child.GenderId);
            Assert.Equal(childrenInfo[0].SchoolingCode, from.Children[0].Child.SchoolingId);
            Assert.Equal(childrenInfo[0].RegionCode, from.Children[0].Child.RegionId);
            Assert.Equal(childrenInfo[0].FirstName, from.Children[0].Child.FirstName);
            Assert.Equal(childrenInfo[0].LastName, from.Children[0].Child.LastName);

            Assert.Equal(parentInfo[0].Id, from.Parents[0].Parent.Id);
            Assert.Equal(parentInfo[0].EthnicityCode, from.Parents[0].Parent.EthnicityId);
            Assert.Equal(parentInfo[0].GenderCode, from.Parents[0].Parent.GenderId);
            Assert.Equal(parentInfo[0].SchoolingCode, from.Parents[0].Parent.SchoolingId);
            Assert.Equal(parentInfo[0].RegionCode, from.Parents[0].Parent.RegionId);
            Assert.Equal(parentInfo[0].FirstName, from.Parents[0].Parent.FirstName);
            Assert.Equal(parentInfo[0].LastName, from.Parents[0].Parent.LastName);
        }

        [Fact]
        public void FromAnswerModel_ToAnswerViewModel_WithoutParentsAndChildren()
        {
            // arrange
            var from = new AnswerModel
            {
                Id = 1,
                RegionId = 2,
                GenderId = 3,
                SchoolingId = 4,
                EthnicityId = 5,
                FirstName = "First Name",
                LastName = "Last Name",
                Parents = new List<AnswerParentChildModel>()
                {
                    new AnswerParentChildModel
                    {
                        Parent = new AnswerModel()
                        {
                            Id = 6,
                            RegionId = 7,
                            GenderId = 8,
                            SchoolingId = 9,
                            EthnicityId = 10,
                            FirstName = "First Name Parent",
                            LastName = "Last Name Parent",
                        }
                    }
                },
                Children = new List<AnswerParentChildModel>()
                {
                    new AnswerParentChildModel
                    {
                        Child = new AnswerModel()
                        {
                            Id = 11,
                            RegionId = 12,
                            GenderId = 13,
                            SchoolingId = 14,
                            EthnicityId = 15,
                            FirstName = "First Name Children",
                            LastName = "Last Name Children",
                        }
                    }
                }
            };

            // act
            var result = from.ToAnswerViewModel(false);

            // assert
            Assert.Equal(result.Info.Id, from.Id);
            Assert.Equal(result.Info.EthnicityCode, from.EthnicityId);
            Assert.Equal(result.Info.GenderCode, from.GenderId);
            Assert.Equal(result.Info.SchoolingCode, from.SchoolingId);
            Assert.Equal(result.Info.RegionCode, from.RegionId);
            Assert.Equal(result.Info.FirstName, from.FirstName);
            Assert.Equal(result.Info.LastName, from.LastName);

            Assert.Null(result.ChildrenInfo);
            Assert.Null(result.ParentsInfo);
        }

        [Fact]
        public void FromIEnumerableAnswserModel_ToAnswerViewModel_WithParentsAndChildren()
        {
            // arrange
            var from = new List<AnswerModel>()
            {
                new AnswerModel
                {
                    Id = 1,
                    RegionId = 2,
                    GenderId = 3,
                    SchoolingId = 4,
                    EthnicityId = 5,
                    FirstName = "First Name",
                    LastName = "Last Name",
                    Parents = new List<AnswerParentChildModel>()
                    {
                        new AnswerParentChildModel
                        {
                            Parent = new AnswerModel()
                            {
                                Id = 6,
                                RegionId = 7,
                                GenderId = 8,
                                SchoolingId = 9,
                                EthnicityId = 10,
                                FirstName = "First Name Parent",
                                LastName = "Last Name Parent",
                            }
                        }
                    },
                    Children = new List<AnswerParentChildModel>()
                    {
                        new AnswerParentChildModel
                        {
                            Child = new AnswerModel()
                            {
                                Id = 11,
                                RegionId = 12,
                                GenderId = 13,
                                SchoolingId = 14,
                                EthnicityId = 15,
                                FirstName = "First Name Children",
                                LastName = "Last Name Children",
                            }
                        }
                    }
                },
                new AnswerModel
                {
                    Id = 21,
                    RegionId = 22,
                    GenderId = 23,
                    SchoolingId = 24,
                    EthnicityId = 25,
                    FirstName = "2 First Name",
                    LastName = "2 Last Name",
                    Parents = new List<AnswerParentChildModel>()
                    {
                        new AnswerParentChildModel
                        {
                            Parent = new AnswerModel()
                            {
                                Id = 26,
                                RegionId = 27,
                                GenderId = 28,
                                SchoolingId = 29,
                                EthnicityId = 210,
                                FirstName = "2 First Name Parent",
                                LastName = "2 Last Name Parent",
                            }
                        }
                    },
                    Children = new List<AnswerParentChildModel>()
                    {
                        new AnswerParentChildModel
                        {
                            Child = new AnswerModel()
                            {
                                Id = 211,
                                RegionId = 212,
                                GenderId = 213,
                                SchoolingId = 214,
                                EthnicityId = 215,
                                FirstName = "2 First Name Children",
                                LastName = "2 Last Name Children",
                            }
                        }
                    }
                }
            };

            // act
            var results = from.ToAnswerViewModel().ToList();

            // assert
            for (var i = 0; i < from.Count; i++)
            {
                var f = from[i];
                var r = results[i];

                var childrenInfo = r.ChildrenInfo.ToList();
                var parentInfo = r.ParentsInfo.ToList();

                Assert.Equal(r.Info.Id, f.Id);
                Assert.Equal(r.Info.EthnicityCode, f.EthnicityId);
                Assert.Equal(r.Info.GenderCode, f.GenderId);
                Assert.Equal(r.Info.SchoolingCode, f.SchoolingId);
                Assert.Equal(r.Info.RegionCode, f.RegionId);
                Assert.Equal(r.Info.FirstName, f.FirstName);
                Assert.Equal(r.Info.LastName, f.LastName);

                Assert.Equal(childrenInfo[0].Id, f.Children[0].Child.Id);
                Assert.Equal(childrenInfo[0].EthnicityCode, f.Children[0].Child.EthnicityId);
                Assert.Equal(childrenInfo[0].GenderCode, f.Children[0].Child.GenderId);
                Assert.Equal(childrenInfo[0].SchoolingCode, f.Children[0].Child.SchoolingId);
                Assert.Equal(childrenInfo[0].RegionCode, f.Children[0].Child.RegionId);
                Assert.Equal(childrenInfo[0].FirstName, f.Children[0].Child.FirstName);
                Assert.Equal(childrenInfo[0].LastName, f.Children[0].Child.LastName);

                Assert.Equal(parentInfo[0].Id, f.Parents[0].Parent.Id);
                Assert.Equal(parentInfo[0].EthnicityCode, f.Parents[0].Parent.EthnicityId);
                Assert.Equal(parentInfo[0].GenderCode, f.Parents[0].Parent.GenderId);
                Assert.Equal(parentInfo[0].SchoolingCode, f.Parents[0].Parent.SchoolingId);
                Assert.Equal(parentInfo[0].RegionCode, f.Parents[0].Parent.RegionId);
                Assert.Equal(parentInfo[0].FirstName, f.Parents[0].Parent.FirstName);
                Assert.Equal(parentInfo[0].LastName, f.Parents[0].Parent.LastName);

            }
        }

        [Fact]
        public void FromIEnumerableAnswserModel_ToAnswerViewModel_WithoutParentsAndChildren()
        {
            // arrange
            var from = new List<AnswerModel>()
            {
                new AnswerModel
                {
                    Id = 1,
                    RegionId = 2,
                    GenderId = 3,
                    SchoolingId = 4,
                    EthnicityId = 5,
                    FirstName = "First Name",
                    LastName = "Last Name",
                    Parents = new List<AnswerParentChildModel>()
                    {
                        new AnswerParentChildModel
                        {
                            Parent = new AnswerModel()
                            {
                                Id = 6,
                                RegionId = 7,
                                GenderId = 8,
                                SchoolingId = 9,
                                EthnicityId = 10,
                                FirstName = "First Name Parent",
                                LastName = "Last Name Parent",
                            }
                        }
                    },
                    Children = new List<AnswerParentChildModel>()
                    {
                        new AnswerParentChildModel
                        {
                            Child = new AnswerModel()
                            {
                                Id = 11,
                                RegionId = 12,
                                GenderId = 13,
                                SchoolingId = 14,
                                EthnicityId = 15,
                                FirstName = "First Name Children",
                                LastName = "Last Name Children",
                            }
                        }
                    }
                },
                new AnswerModel
                {
                    Id = 21,
                    RegionId = 22,
                    GenderId = 23,
                    SchoolingId = 24,
                    EthnicityId = 25,
                    FirstName = "2 First Name",
                    LastName = "2 Last Name",
                    Parents = new List<AnswerParentChildModel>()
                    {
                        new AnswerParentChildModel
                        {
                            Parent = new AnswerModel()
                            {
                                Id = 26,
                                RegionId = 27,
                                GenderId = 28,
                                SchoolingId = 29,
                                EthnicityId = 210,
                                FirstName = "2 First Name Parent",
                                LastName = "2 Last Name Parent",
                            }
                        }
                    },
                    Children = new List<AnswerParentChildModel>()
                    {
                        new AnswerParentChildModel
                        {
                            Child = new AnswerModel()
                            {
                                Id = 211,
                                RegionId = 212,
                                GenderId = 213,
                                SchoolingId = 214,
                                EthnicityId = 215,
                                FirstName = "2 First Name Children",
                                LastName = "2 Last Name Children",
                            }
                        }
                    }
                }
            };

            // act
            var results = from.ToAnswerViewModel(false).ToList();

            // assert
            for (var i = 0; i < from.Count; i++)
            {
                var f = from[i];
                var r = results[i];

                Assert.Equal(r.Info.Id, f.Id);
                Assert.Equal(r.Info.EthnicityCode, f.EthnicityId);
                Assert.Equal(r.Info.GenderCode, f.GenderId);
                Assert.Equal(r.Info.SchoolingCode, f.SchoolingId);
                Assert.Equal(r.Info.RegionCode, f.RegionId);
                Assert.Equal(r.Info.FirstName, f.FirstName);
                Assert.Equal(r.Info.LastName, f.LastName);

                Assert.Null(r.ChildrenInfo);
                Assert.Null(r.ParentsInfo);
            }
        }

        [Fact]
        public void FromIEnumerableIEnumerableAnswserModel_ToAnswerViewModel()
        {
            // arrange
            var from = new List<List<AnswerModel>>()
            {
                new List<AnswerModel>
                {
                    new AnswerModel
                    {
                        Id = 1, FirstName = "FirstName", LastName = "LastName", RegionId = 2, GenderId = 3,
                        SchoolingId = 4, EthnicityId = 5
                    }
                },
                new List<AnswerModel>
                {
                    new AnswerModel
                    {
                        Id = 11, FirstName = "1FirstName", LastName = "1LastName", RegionId = 12, GenderId = 13,
                        SchoolingId = 14, EthnicityId = 15
                    },
                    new AnswerModel
                    {
                        Id = 21, FirstName = "2FirstName", LastName = "2LastName", RegionId = 22, GenderId = 23,
                        SchoolingId = 24, EthnicityId = 25
                    }
                },
            };

            // act
            var result = from.ToAnswerViewModel().ToList();

            // assert
            Assert.Single(result[0]);
            Assert.Equal(2, result[1].Count());

            var firstResult = result[0].ToList()[0];
            Assert.Equal(firstResult.Info.Id, from[0][0].Id);
            Assert.Equal(firstResult.Info.EthnicityCode, from[0][0].EthnicityId);
            Assert.Equal(firstResult.Info.GenderCode, from[0][0].GenderId);
            Assert.Equal(firstResult.Info.SchoolingCode, from[0][0].SchoolingId);
            Assert.Equal(firstResult.Info.RegionCode, from[0][0].RegionId);
            Assert.Equal(firstResult.Info.FirstName, from[0][0].FirstName);
            Assert.Equal(firstResult.Info.LastName, from[0][0].LastName);

            var secondResults = result[1].ToList();

            for (var i = 0; i < secondResults.Count; i++)
            {
                Assert.Equal(secondResults[i].Info.Id, from[1][i].Id);
                Assert.Equal(secondResults[i].Info.EthnicityCode, from[1][i].EthnicityId);
                Assert.Equal(secondResults[i].Info.GenderCode, from[1][i].GenderId);
                Assert.Equal(secondResults[i].Info.SchoolingCode, from[1][i].SchoolingId);
                Assert.Equal(secondResults[i].Info.RegionCode, from[1][i].RegionId);
                Assert.Equal(secondResults[i].Info.FirstName, from[1][i].FirstName);
                Assert.Equal(secondResults[i].Info.LastName, from[1][i].LastName);
            }

        }
    }
}
