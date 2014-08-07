using System.Web.Mvc;
using HiringManager.Web.Controllers;
using HiringManager.Web.ViewModels.Positions;
using IntegrationTestHelpers;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace HiringManager.Web.Integration.Tests.Steps.Positions
{
    [Binding]
    class ClosePositionSteps
    {
        [When(@"I navigate to the Close Position page for '(.*)'")]
        public void WhenINavigateToTheClosePositionPageFor(string positionName)
        {
            var positionId = ScenarioContext.Current.Get<int>(positionName);
            var controller = ScenarioContextExtensions.GetFromNinject<PositionsController>(ScenarioContext.Current);
            var page = controller.Close(positionId);
            var key = string.Format("Positions/Close/{0}", positionId);
            ScenarioContext.Current.Set(page, key);
            ScenarioContext.Current.Set(page);

        }

        [Then(@"the Close Position page should display")]
        public void ThenTheClosePositionPageShouldDisplay(Table table)
        {
            var page = ScenarioContext.Current.Get<ActionResult>();
            var viewResult = page as ViewResult;
            Assert.That(viewResult, Is.Not.Null);

            var actual = viewResult.Model as ClosePositionViewModel;
            Assert.That(actual, Is.Not.Null);

            table.CompareToInstance(actual);
        }

        [When(@"I close the position for '(.*)'")]
        public void WhenICloseThePositionFor(string positionName)
        {
            var positionId = ScenarioContext.Current.Get<int>(positionName);
            var controller = ScenarioContextExtensions.GetFromNinject<PositionsController>(ScenarioContext.Current);
            var page = controller.Close(new ClosePositionViewModel()
                                        {
                                            PositionId = positionId,
                                        });
            ScenarioContext.Current.Set(page);
        }

        [Then(@"I should be redirected to the Position Index page")]
        public void ThenIShouldBeRedirectedToThePositionIndexPage()
        {
            var page = ScenarioContext.Current.Get<ActionResult>();
            var redirect = page as RedirectToRouteResult;
            Assert.That(redirect, Is.Not.Null);

            Assert.That(redirect.RouteValues["action"], Is.EqualTo("Index"));
        }

    }
}
