using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Web.Mvc;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Authentication;
using HiringManager.DomainServices.Candidates;
using HiringManager.DomainServices.Positions;
using HiringManager.DomainServices.Sources;
using HiringManager.EntityModel;
using HiringManager.Web.Infrastructure;
using HiringManager.Web.Infrastructure.MVC;
using HiringManager.Web.ViewModels;
using HiringManager.Web.ViewModels.Positions;
using Simple.Validation;

namespace HiringManager.Web.Controllers
{
    [Authorize]
    public partial class PositionsController : Controller
    {
        private readonly IPositionService _positionService;
        private readonly ISourceService _sourceService;
        private readonly ICandidateService _candidateService;
        private readonly IUserSession _userSession;
        private readonly IClock _clock;

        public PositionsController(IPositionService positionService, ISourceService sourceService, ICandidateService candidateService, IUserSession userSession, IClock clock)
        {
            _positionService = positionService;
            _sourceService = sourceService;
            _candidateService = candidateService;
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
                request.Statuses = new[] { status };

            var openPositions = this._positionService.Query(request);
            var viewModel = AutoMapper.Mapper.Map<IndexViewModel<PositionSummaryIndexItem>>(openPositions);

            return View(viewModel);
        }

        public virtual ViewResult Create()
        {
            var viewModel = new CreatePositionViewModel()
                            {
                                OpenDate = this._clock.Today,
                                Openings = 1,
                            };
            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult Create(CreatePositionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var request = AutoMapper.Mapper.Map<CreatePositionRequest>(viewModel);

                request.HiringManagerId = _userSession.ManagerId.GetValueOrDefault();
                var response = this._positionService.CreatePosition(request);
                ModelState.AddModelErrors(response.ValidationResults);

                if (ModelState.IsValid)
                    return RedirectToAction(MVC.Positions.Index("Open"));
            }
            return View(viewModel);
        }

        public virtual ViewResult Candidates(int id)
        {
            var details = this._positionService.Details(id);
            var viewModel = AutoMapper.Mapper.Map<PositionCandidatesViewModel>(details);
            return View(viewModel);
        }

        [HttpGet]
        public virtual ViewResult AddCandidate(int id)
        {
            var sources = _sourceService.Query(null);
            var candidates = _candidateService.Query(null);

            var viewModel = new AddCandidateViewModel()
                            {
                                PositionId = id,
                                Sources = new SelectList(sources.Data, "SourceId", "Name"),
                                Candidates = new SelectList(candidates.Data, "CandidateId", "Name"),
                            };
            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult AddCandidate(AddCandidateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var request = AutoMapper.Mapper.Map<AddCandidateRequest>(viewModel);

                var response = this._positionService.AddCandidate(request);
                this.ModelState.Accept(response);

                if (this.ModelState.IsValid)
                    return RedirectToAction("Candidates", new { id = response.PositionId });
            }
            
            var sources = _sourceService.Query(null);
            var candidates = _candidateService.Query(null);
            
            viewModel.Sources = new SelectList(sources.Data, "SourceId", "Name");
            viewModel.Candidates = new SelectList(candidates.Data, "CandidateId", "Name");
            
            return View(viewModel);
        }

        [HttpGet]
        public virtual ViewResult Pass(int id)
        {
            var details = this._positionService.GetCandidateStatusDetails(id);
            var viewModel = AutoMapper.Mapper.Map<CandidateStatusViewModel>(details);
            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult Pass(CandidateStatusViewModel model)
        {
            if (ModelState.IsValid)
            {
                this._positionService.SetCandidateStatus(model.CandidateStatusId, "Passed");
                return RedirectToAction("Candidates", new { id = model.PositionId });
            }
            return View(model);
        }

        [HttpGet]
        public virtual ViewResult Status(int id)
        {
            var details = this._positionService.GetCandidateStatusDetails(id);
            var viewModel = AutoMapper.Mapper.Map<CandidateStatusViewModel>(details);

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
            var viewModel = AutoMapper.Mapper.Map<CandidateStatusViewModel>(details);
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

        public virtual ActionResult Close(int id)
        {
            var details = this._positionService.Details(id);
            var viewModel = AutoMapper.Mapper.Map<ClosePositionViewModel>(details);
            return View(viewModel);
        }

        [HttpPost]
        public virtual ActionResult Close(ClosePositionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = this._positionService.Close(model.PositionId);
                if (response.ValidationResults.HasErrors())
                {
                    response.WriteValidationErrorsTo(ModelState);
                    return View(model);
                }
                return RedirectToAction(MVC.Positions.Index());
            }
            return View(model);
        }
    }
}