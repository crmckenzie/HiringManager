using System;
using System.Linq;
using System.Web.Mvc;
using HiringManager.Web.Controllers;
using HiringManager.Web.Integration.Tests.Models.Positions;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Positions;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HiringManager.Web.Integration.Tests.Steps.Positions
{
    [Binding]
    public class CreatePositionSteps
    {
        [Given(@"I want to create the position '(.*)' to start on '(.*)'")]
        public void GivenIWantToCreateThePositionToStartOn(string positionName, DateTime startDate)
        {
            var viewModel = new CreatePositionViewModel()
                            {
                                Title = positionName,
                                OpenDate = startDate,
                            };


            ScenarioContext.Current.Set(viewModel);
        }

        [When(@"I submit the create position request")]
        public void WhenISubmitTheCreatePositionRequest()
        {
            var viewModel = ScenarioContext.Current.Get<CreatePositionViewModel>();
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var response = controller.Create(viewModel);
            ScenarioContext.Current.Set(response);
        }

        [Then(@"the I should be redirected to the Position Index page")]
        public void ThenTheIShouldBeRedirectedToThePositionIndexPage()
        {
            var response = ScenarioContext.Current.Get<ActionResult>();
            var redirect = response as RedirectToRouteResult;

            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(redirect.RouteValues["controller"], Is.EqualTo("Positions"));
            Assert.That(redirect.RouteValues["status"], Is.EqualTo("Open"));
        }

        [Then(@"the requested position should be listed on the Position Index Page")]
        public void ThenTheRequestedPositionShouldBeListedOnThePositionIndexPage()
        {
            var viewModel = ScenarioContext.Current.Get<CreatePositionViewModel>();

            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();

            var view = controller.Index("Open") as ViewResult;

            var model = view.Model as IndexViewModel<PositionSummaryIndexItem>;
            Assert.That(model, Is.Not.Null);

            var targetRecord =
                model.Data.SingleOrDefault(row => row.Title == viewModel.Title && row.OpenDate == viewModel.OpenDate);
            Assert.That(targetRecord, Is.Not.Null);

        }

        [Then(@"the requested position should have a status of '(.*)'")]
        public void ThenTheRequestedPositionShouldHaveAStatusOf(string p0)
        {
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var view = controller.Index("Open") as ViewResult;
            var model = view.Model as IndexViewModel<PositionSummaryIndexItem>;
            var viewModel = ScenarioContext.Current.Get<CreatePositionViewModel>();

            var targetRecord =
                model.Data.SingleOrDefault(row => row.Title == viewModel.Title && row.OpenDate == viewModel.OpenDate);

            Assert.That(targetRecord.Status, Is.EqualTo("Open"));
        }

        [Given(@"I have created the position '(.*)' to start on '(.*)'")]
        public void GivenIHaveCreatedThePositionToStartOn(string positionName, DateTime startDate)
        {
            var viewModel = new CreatePositionViewModel()
            {
                Title = positionName,
                OpenDate = startDate,
            };

            ScenarioContext.Current.Set(viewModel);

            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();

            var response = controller.Create(viewModel) as RedirectToRouteResult;            
        }


        [When(@"I receive resumes from the following candidates")]
        [Given(@"I have received resumes from the following candidates")]
        public void WhenIReceiveResumesFromTheFollowingCandidates(Table table)
        {
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var view = controller.Index("Open") as ViewResult;
            var model = view.Model as IndexViewModel<PositionSummaryIndexItem>;
            var createPositionViewModel = ScenarioContext.Current.Get<CreatePositionViewModel>();
            var positionSummaryItem = model.Data.Single(row => row.Title == createPositionViewModel.Title);

            var candidates = table.CreateSet<AddCandidateViewModel>();

            foreach (var addCandidateViewModel in candidates)
            {
                addCandidateViewModel.PositionId = positionSummaryItem.PositionId;
                controller.AddCandidate(addCandidateViewModel);
            }
        }

        [When(@"I pass on the candidate '(.*)'")]
        public void WhenIPassOnTheCandidate(string candidateName)
        {
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var view = controller.Index("Open") as ViewResult;
            var model = view.Model as IndexViewModel<PositionSummaryIndexItem>;
            var createPositionViewModel = ScenarioContext.Current.Get<CreatePositionViewModel>();
            var positionSummaryItem = model.Data.Single(row => row.Title == createPositionViewModel.Title);
            var positionDetailsView = controller.Candidates(positionSummaryItem.PositionId) as ViewResult;
            var positionCandidatesViewModel = positionDetailsView.Model as PositionCandidatesViewModel;

            var candidateViewModel =
                positionCandidatesViewModel.Candidates.Single(row => row.CandidateName == candidateName);

            var response = controller.Pass(new CandidateStatusViewModel()
                            {
                                CandidateId = candidateViewModel.CandidateId,
                                CandidateName = candidateViewModel.CandidateName,
                                CandidateStatusId = candidateViewModel.CandidateStatusId,
                                PositionId = positionCandidatesViewModel.PositionId,
                                PositionTitle = positionCandidatesViewModel.Title,
                                Status = positionCandidatesViewModel.Status,
                            });



        }

        [When(@"I set the candidate status for '(.*)' to '(.*)'")]
        public void WhenISetTheCandidateStatusForTo(string candidateName, string status)
        {
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var view = controller.Index("Open") as ViewResult;
            var model = view.Model as IndexViewModel<PositionSummaryIndexItem>;
            var createPositionViewModel = ScenarioContext.Current.Get<CreatePositionViewModel>();
            var positionSummaryItem = model.Data.Single(row => row.Title == createPositionViewModel.Title);
            var positionDetailsView = controller.Candidates(positionSummaryItem.PositionId) as ViewResult;
            var positionCandidatesViewModel = positionDetailsView.Model as PositionCandidatesViewModel;

            var candidateViewModel =
                positionCandidatesViewModel.Candidates.Single(row => row.CandidateName == candidateName);

            var response = controller.Status(new CandidateStatusViewModel()
            {
                CandidateId = candidateViewModel.CandidateId,
                CandidateName = candidateViewModel.CandidateName,
                CandidateStatusId = candidateViewModel.CandidateStatusId,
                PositionId = positionCandidatesViewModel.PositionId,
                PositionTitle = positionCandidatesViewModel.Title,
                Status = status,
            });
        }

        [Then(@"the requested position should have a (.*) candidate\(s\) awaiting review count")]
        public void ThenTheRequestedPositionShouldHaveACandidateSAwaitingReviewCountOf(int candidatesAwaitingReview)
        {
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var view = controller.Index("Open") as ViewResult;
            var model = view.Model as IndexViewModel<PositionSummaryIndexItem>;
            var createPositionViewModel = ScenarioContext.Current.Get<CreatePositionViewModel>();
            var positionSummaryItem = model.Data.Single(row => row.Title == createPositionViewModel.Title);
            Assert.That(positionSummaryItem.CandidatesAwaitingReview, Is.EqualTo(candidatesAwaitingReview));
        }


        [Then(@"the position details should contain the following candidates")]
        public void ThenThePositionDetailsShouldContainTheFollowingCandidates(Table table)
        {
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var indexView = controller.Index("Open") as ViewResult;
            var model = indexView.Model as IndexViewModel<PositionSummaryIndexItem>;
            var createPositionViewModel = ScenarioContext.Current.Get<CreatePositionViewModel>();
            var positionSummaryItem = model.Data.Single(row => row.Title == createPositionViewModel.Title);
            var positionDetailsView = controller.Candidates(positionSummaryItem.PositionId) as ViewResult;
            var positionCandidatesViewModel = positionDetailsView.Model as PositionCandidatesViewModel;

            var expectedCandidates = table.CreateSet<CandidateStatusDetailsTestModel>();

            foreach (var expected in expectedCandidates)
            {
                var actual = positionCandidatesViewModel.Candidates
                    .SingleOrDefault(row => row.CandidateName == expected.Name);

                Assert.That(actual, Is.Not.Null);
                Assert.That(actual.Status, Is.EqualTo(expected.Status));
                
                var emailAddress = actual.ContactInfo.Single(row => row.Type == "Email").Value;
                Assert.That(emailAddress, Is.EqualTo(expected.EmailAddress));

                var phoneNumber = actual.ContactInfo.Single(row => row.Type == "Phone").Value;
                Assert.That(phoneNumber, Is.EqualTo(expected.PhoneNumber));
            }

        }

    }
}