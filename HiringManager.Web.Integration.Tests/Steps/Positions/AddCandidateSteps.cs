using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HiringManager.EntityModel;
using HiringManager.Web.Controllers;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Positions;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HiringManager.Web.Integration.Tests.Steps.Positions
{
    [Binding]
    public class AddCandidateSteps
    {
        [Given(@"I have the following candidate")]
        public void GivenIHaveTheFollowingCandidate(Table table)
        {
            var positionId = ScenarioContext.Current.Get<int>("PositionId");

            var viewModel = table.CreateInstance<AddCandidateViewModel>();
            viewModel.PositionId = positionId;
   
            ScenarioContext.Current.Set(viewModel);
        }

        [When(@"I submit the the candidate to AddCandidate")]
        public void WhenISubmitTheTheCandidateToAddCandidate()
        {
            var viewModel = ScenarioContext.Current.Get<AddCandidateViewModel>();
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var response = controller.AddCandidate(viewModel);
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

    }
}
