using System;
using System.Web.Mvc;
using HiringManager.DomainServices;
using HiringManager.Mappers;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Positions;

namespace HiringManager.Web.Controllers
{
    [Authorize]
    public class PositionsController : Controller
    {
        private readonly IPositionService _positionService;
        private readonly IFluentMapper _fluentMapper;
        private readonly IClock _clock;

        public PositionsController(IPositionService positionService, IFluentMapper fluentMapper, IClock clock)
        {
            _positionService = positionService;
            _fluentMapper = fluentMapper;
            _clock = clock;
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

        public ViewResult Create()
        {
            var viewModel = new CreatePositionViewModel()
                            {
                                OpenDate = this._clock.Today,
                            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(CreatePositionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var request = this._fluentMapper
                    .Map<CreatePositionRequest>()
                    .From(viewModel)
                    ;
                this._positionService.CreatePosition(request);
                return RedirectToAction("Index", "Positions");
            }
            return View(viewModel);
        }

        public ViewResult Candidates(int id)
        {
            var details = this._positionService.Details(id);
            var viewModel = this._fluentMapper
                .Map<PositionCandidatesViewModel>()
                .From(details)
                ;

            return View(viewModel);
        }

        [HttpGet]
        public ViewResult AddCandidate(int id)
        {
            var viewModel = new AddCandidateViewModel()
                            {
                                PositionId = id,
                            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddCandidate(AddCandidateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var request = this._fluentMapper
                    .Map<AddCandidateRequest>()
                    .From(viewModel);

                var response = this._positionService.AddCandidate(request);

                return RedirectToRoute("Candidates", new {id = response.PositionId});
            }
            return View(viewModel);
        }

        public ActionResult Pass(int id)
        {
            return View(id);
        }

        public ActionResult Status(int id)
        {
            return View(id);
        }

        public ActionResult Hire(int id)
        {
            return View(id);
        }
    }
}