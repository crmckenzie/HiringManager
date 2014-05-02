using System.Linq;
using System.Web.Mvc;
using HiringManager.DomainServices.Validators.UnitTests;
using HiringManager.Web.Controllers;
using HiringManager.Web.ViewModels.Positions;
using NUnit.Framework;
using Simple.Validation;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestHelpers;

namespace HiringManager.Web.Integration.Tests.Steps.Positions
{
    [Binding]
    public class AddCandidateSteps
    {
        [Given(@"I have the following candidate")]
        public void GivenIHaveTheFollowingCandidate(Table table)
        {
            var positionId = ScenarioContext.Current.Get<int>("PositionId");

            var viewModel = table.CreateInstance<NewCandidateViewModel>();
            viewModel.PositionId = positionId;
   
            ScenarioContext.Current.Set(viewModel);
        }

        [When(@"I submit the the candidate to AddCandidate")]
        public void WhenISubmitTheTheCandidateToAddCandidate()
        {
            var viewModel = ScenarioContext.Current.Get<NewCandidateViewModel>();
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var response = controller.NewCandidate(viewModel);
            ScenarioContext.Current.Set(response);
        }

        [Then(@"I should be redirected to the Position Candidates Page")]
        public void ThenIShouldBeRedirectedToThePositionCandidatesPage()
        {
            var positionId = ScenarioContext.Current.Get<int>("PositionId");

            var response = ScenarioContext.Current.Get<ActionResult>();
            var redirect = response as RedirectToRouteResult;
            Assert.That(redirect, Is.Not.Null);

            Assert.That(redirect.RouteValues.ContainsKey("id"));
            Assert.That(redirect.RouteValues["id"], Is.EqualTo(positionId));
            Assert.That(redirect.RouteValues.ContainsKey("action"));
            Assert.That(redirect.RouteValues["action"], Is.EqualTo("Candidates"));
        }

        [Then(@"'(.*)' should be listed as a candidate with a status of '(.*)'")]
        public void ThenShouldBeListedAsACandidateWithAStatusOf(string candidateName, string status)
        {
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var positionId = ScenarioContext.Current.Get<int>("PositionId");
            var details = controller.Candidates(positionId).Model as PositionCandidatesViewModel;

            var candidate = details.Candidates.SingleOrDefault(row => row.CandidateName == candidateName);
            Assert.That(candidate.Status, Is.EqualTo(status));
        }


        [Given(@"I have added the following candidate")]
        [When(@"I add the following candidate")]
        public void GivenIHaveAddedTheFollowingCandidate(Table table)
        {
            var positionId = ScenarioContext.Current.Get<int>("PositionId");

            var viewModel = table.CreateInstance<NewCandidateViewModel>();
            viewModel.PositionId = positionId;
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var response = controller.NewCandidate(viewModel);
            ScenarioContext.Current.Set(response);
            ScenarioContext.Current.Set(viewModel);
        }

        [Given(@"I have hired the candidate '(.*)'")]
        public void GivenIHaveHiredTheCandidate(string candidateName)
        {
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var positionId = ScenarioContext.Current.Get<int>("PositionId");
            var details = controller.Candidates(positionId).Model as PositionCandidatesViewModel;

            var candidate = details.Candidates.SingleOrDefault(row => row.CandidateName == candidateName);

            var response = controller.Hire(new CandidateStatusViewModel()
                                           {
                                               CandidateId = candidate.CandidateId,
                                               CandidateStatusId = candidate.CandidateStatusId,
                                               PositionId = positionId,
                                           });
   
            ScenarioContext.Current.Set(response);
        }

        [Then(@"I should be returned to the Add Candidate page")]
        public void ThenIShouldBeReturnedToTheAddCandidatePage()
        {
            var response = ScenarioContext.Current.Get<ActionResult>() as ViewResult;
            Assert.That(response, Is.Not.Null);

            var addCandidateViewModel = ScenarioContext.Current.Get<NewCandidateViewModel>();

            Assert.That(response.Model, Is.SameAs(addCandidateViewModel));
        }

        [Then(@"the page should report the following errors")]
        public void ThenThePageShouldReportTheFollowingErrors(Table table)
        {
            var validationResults = table.CreateSet<ValidationResult>();
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            controller.OutputModelState();

            var actualResults = controller.GetModelStateValidationResults();

            foreach (var expectedResult in validationResults)
            {
                actualResults.AssertInvalidFor(expectedResult);
            }
        }
    }
}
