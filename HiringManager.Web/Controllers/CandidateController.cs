using System.IO;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Services.Description;
using HiringManager.DomainServices;
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
        private readonly IUploadService _uploadService;
        private readonly HiringManagerDbContext _db;

        public CandidateController(ICandidateService candidateService, ISourceService sourceService, IUploadService uploadService, HiringManagerDbContext dbContext)
        {
            _candidateService = candidateService;
            _sourceService = sourceService;
            _uploadService = uploadService;
            _db = dbContext;
        }

        // GET: /Candidate/
        public virtual ActionResult Index()
        {
            return View(_db.Candidates.ToList());
        }

        // GET: /Candidate/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = _db.Candidates.Find(id);
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
            Candidate candidate = _db.Candidates.Find(id);
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
            Candidate candidate = _db.Candidates.Find(id);
            _db.Candidates.Remove(candidate);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        public FileResult Download(int id)
        {
            var download = _uploadService.Download(id);

            var mimeType = System.Web.MimeMapping.GetMimeMapping(download.FileName);

            return File(download.Stream, mimeType, download.FileName);
        }
    }
}
