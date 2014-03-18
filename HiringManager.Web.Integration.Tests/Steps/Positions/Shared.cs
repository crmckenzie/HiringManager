using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using HiringManager.EntityModel;
using HiringManager.Web.Controllers;
using HiringManager.Web.Models;
using HiringManager.Web.ViewModels;
using HiringManager.Web.ViewModels.Positions;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace HiringManager.Web.Integration.Tests.Steps.Positions
{
    [Binding]
    public class Shared
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

        [Given(@"I submit the create position request")]
        [When(@"I submit the create position request")]
        public void WhenISubmitTheCreatePositionRequest()
        {
            var viewModel = ScenarioContext.Current.Get<CreatePositionViewModel>();
            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var response = controller.Create(viewModel);
            ScenarioContext.Current.Set(response);

            if (response.GetType() == typeof (RedirectToRouteResult))
            {
                var view = controller.Index("Open");
                var model = view.Model as IndexViewModel<PositionSummaryIndexItem>;
                var positionSummaryItem = model.Data.Single(row => row.Title == viewModel.Title);
                ScenarioContext.Current.Set(positionSummaryItem.PositionId, "PositionId");
                ScenarioContext.Current.Set(positionSummaryItem.PositionId, positionSummaryItem.Title);
            }
        }

        [Then(@"the requested position should have a status of '(.*)'")]
        public void ThenTheRequestedPositionShouldHaveAStatusOf(string status)
        {
            var positionId = ScenarioContext.Current.Get<int>("PositionId");

            var controller = ScenarioContext.Current.GetFromNinject<PositionsController>();
            var view = controller.Index("Open") as ViewResult;
            var model = view.Model as IndexViewModel<PositionSummaryIndexItem>;

            var targetRecord = model.Data.SingleOrDefault(row => row.PositionId == positionId);

            Assert.That(targetRecord.Status, Is.EqualTo("Open"));
        }
    }
}
