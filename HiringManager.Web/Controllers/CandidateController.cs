using System.Linq;
using System.Net;
using System.Web.Mvc;
using HiringManager.DomainServices.Candidates;
using HiringManager.DomainServices.Sources;
using HiringManager.EntityModel;
using HiringManager.EntityFramework;
using HiringManager.Web.Infrastructure.MVC;
using HiringManager.Web.ViewModels.Candidates;

namespace HiringManager.Web.Controllers
{
    public partial class CandidateController : Controller
    {
        private readonly ICandidateService _candidateService;
        private readonly ISourceService _sourceService;
        private readonly HiringManagerDbContext db;

        public CandidateController(ICandidateService candidateService, ISourceService sourceService, HiringManagerDbContext dbContext)
        {
            _candidateService = candidateService;
            _sourceService = sourceService;
            db = dbContext;
        }

        // GET: /Candidate/
        public virtual ActionResult Index()
        {
            return View(db.Candidates.ToList());
        }

        // GET: /Candidate/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // GET: /Candidate/Create
        public virtual ActionResult Create()
        {
            var viewModel = new EditCandidateViewModel()
                            {
                                Sources = GetSourcesAsSelectList(),
                            };
            return View(viewModel);
        }

        private SelectList GetSourcesAsSelectList(int? sourceId = null)
        {
            var sources = _sourceService.Query(null).Data;
            var selectList = new SelectList(sources, "SourceId", "Name", sourceId);
            return selectList;
        }

        // POST: /Candidate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Create(EditCandidateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var request = AutoMapper.Mapper.Map<SaveCandidateRequest>(viewModel);
                var response = this._candidateService.Save(request);
                this.ModelState.Accept(response);

                if (this.ModelState.IsValid)
                    return RedirectToAction("Index");
            }

            viewModel.Sources = GetSourcesAsSelectList(viewModel.SourceId);

            return View(viewModel);
        }

        // GET: /Candidate/Edit/5
        public virtual ActionResult Edit(int id)
        {
            var details = _candidateService.Get(id);
            var viewModel = AutoMapper.Mapper.Map<EditCandidateViewModel>(details);
            viewModel.Sources = GetSourcesAsSelectList(details.SourceId);
            return View(viewModel);
        }

        // POST: /Candidate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Edit(EditCandidateViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var request = AutoMapper.Mapper.Map<SaveCandidateRequest>(viewModel);
                var response = this._candidateService.Save(request);
                this.ModelState.Accept(response);

                if (this.ModelState.IsValid)
                    return RedirectToAction("Index");
            }

            viewModel.Sources = GetSourcesAsSelectList(viewModel.SourceId);

            return View(viewModel);
        }

        // GET: /Candidate/Delete/5
        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: /Candidate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = db.Candidates.Find(id);
            db.Candidates.Remove(candidate);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
