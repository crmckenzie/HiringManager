using System;
using System.Linq;
using System.Web.Mvc;
using HiringManager.Web.Controllers;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Positions;
using NUnit.Framework;
using TechTalk.SpecFlow;

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
            ScenarioContext.Current.Pending();
        }

    }
}