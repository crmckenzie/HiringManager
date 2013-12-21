using System;
using System.Web.Mvc;
using HiringManager.DomainServices;
using HiringManager.Mappers;
using HiringManager.Web.Infrastructure;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Positions;
using Simple.Validation;

namespace HiringManager.Web.Controllers
{
    [Authorize]
    public partial class PositionsController : Controller
    {
        private readonly IPositionService _positionService;
        private readonly IFluentMapper _fluentMapper;
        private readonly IUserSession _userSession;
        private readonly IClock _clock;

        public PositionsController(IPositionService positionService, IFluentMapper fluentMapper, IUserSession userSession, IClock clock)
        {
            _positionService = positionService;
            _fluentMapper = fluentMapper;
            _userSession = userSession;
            _clock = clock;
        }

        public virtual ViewResult Index(string status)
        {
            var request = new QueryPositionSummariesRequest()
                          {
                              ManagerIds = new[] { this._userSession.ManagerId.Value }
                          };
            if (!string.IsNullOrWhiteSpace(status))
                request.Statuses = new[] {status};

            var openPositions = this._positionService.Query(request);
            var viewModel = this._fluentMapper
                .Map<IndexViewModel<PositionSummaryIndexItem>>()
                .From(openPositions)
                ;

            return View(viewModel);
        }

        public virtual ViewResult Create()
        {
            var viewModel = new CreatePositionViewModel()
                            {
                                OpenDate = this._clock.Today,
                            };
            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(CreatePositionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var request = this._fluentMapper
                    .Map<CreatePositionRequest>()
                    .From(viewModel)
                    ;
                this._positionService.CreatePosition(request);
                return RedirectToAction(MVC.Positions.Index("Open"));
            }
            return View(viewModel);
        }

        public virtual ViewResult Candidates(int id)
        {
            var details = this._positionService.Details(id);
            var viewModel = this._fluentMapper
                .Map<PositionCandidatesViewModel>()
                .From(details)
                ;

            return View(viewModel);
        }

        [HttpGet]
        public virtual ViewResult AddCandidate(int id)
        {
            var viewModel = new AddCandidateViewModel()
                            {
                                PositionId = id,
                            };
            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult AddCandidate(AddCandidateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var request = this._fluentMapper
                    .Map<AddCandidateRequest>()
                    .From(viewModel);

                var response = this._positionService.AddCandidate(request);
                if (response.ValidationResults.HasErrors())
                {
                    response.WriteValidationErrorsTo(ModelState);
                    return View(viewModel);
                }

                return RedirectToAction("Candidates", new {id = response.PositionId});
            }
            return View(viewModel);
        }

        [HttpGet]
        public virtual ViewResult Pass(int id)
        {
            var details = this._positionService.GetCandidateStatusDetails(id);
            var viewModel = this._fluentMapper
                .Map<CandidateStatusViewModel>()
                .From(details)
                ;

            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult Pass(CandidateStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                this._positionService.SetCandidateStatus(model.CandidateStatusId, "Passed");
                return RedirectToAction("Candidates", new {id = model.PositionId});
            }
            return View(model);
        }

        [HttpGet]
        public virtual ViewResult Status(int id)
        {
            var details = this._positionService.GetCandidateStatusDetails(id);
            var viewModel = this._fluentMapper
                .Map<CandidateStatusViewModel>()
                .From(details)
                ;

            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult Status(CandidateStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                this._positionService.SetCandidateStatus(model.CandidateStatusId, model.Status);
                return RedirectToAction("Candidates", new { id = model.PositionId });
            }
            return View(model);
        }

        [HttpGet]
        public virtual ViewResult Hire(int id)
        {
            var details = this._positionService.GetCandidateStatusDetails(id);
            var viewModel = this._fluentMapper
                .Map<CandidateStatusViewModel>()
                .From(details)
                ;

            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult Hire(CandidateStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = this._positionService.Hire(model.CandidateStatusId);
                if (response.ValidationResults.HasErrors())
                {
                    response.WriteValidationErrorsTo(ModelState);
                    return View(model);
                }

                return RedirectToAction("Candidates", new { id = model.PositionId });
            }
            return View(model);
        }

        public ActionResult Close(int id)
        {
            var details = this._positionService.GetCandidateStatusDetails(id);
            var viewModel = this._fluentMapper
                .Map<CandidateStatusViewModel>()
                .From(details)
                ;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Close(CandidateStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = this._positionService.Close(model.CandidateStatusId);
                if (response.ValidationResults.HasErrors())
                {
                    response.WriteValidationErrorsTo(ModelState);
                    return View(model);
                }
                return RedirectToAction("Candidates", new { id = model.PositionId });
            }
            return View(model);
        }
    }
}