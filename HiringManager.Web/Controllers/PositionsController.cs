using System.Web.Mvc;
using HiringManager.DomainServices;
using HiringManager.Mappers;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Positions;

namespace HiringManager.Web.Controllers
{
    public class PositionsController : Controller
    {
        private readonly IPositionService _positionService;
        private readonly IFluentMapper _fluentMapper;

        public PositionsController(IPositionService positionService, IFluentMapper fluentMapper)
        {
            _positionService = positionService;
            _fluentMapper = fluentMapper;
        }

        public ViewResult Index()
        {
            var openPositions = this._positionService.Query(new QueryPositionSummariesRequest()
                                                            {
                                                                Statuses = new []{"Open"},
                                                            });
            var viewModel = this._fluentMapper
                .Map<IndexViewModel<PositionSummaryIndexItem>>()
                .From(openPositions)
                ;

            return View(viewModel);
        }
    }
}