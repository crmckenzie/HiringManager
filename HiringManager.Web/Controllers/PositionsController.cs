using System.Web.Mvc;
using HiringManager.Web.ApplicationServices.Positions;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Positions;

namespace HiringManager.Web.Controllers
{
    public class PositionsController : Controller
    {
        private readonly IPositionApplicationService _positionApplicationService;
        private readonly IFluentMapper _fluentMapper;

        public PositionsController(IPositionApplicationService positionApplicationService, IFluentMapper fluentMapper)
        {
            _positionApplicationService = positionApplicationService;
            _fluentMapper = fluentMapper;
        }

        public ViewResult Index()
        {
            var openPositions = this._positionApplicationService.GetOpenPositions();
            var viewModel = this._fluentMapper
                .Map<IndexViewModel<PositionSummaryIndexItem>>()
                .From(openPositions)
                ;

            return View(viewModel);
        }
    }
}