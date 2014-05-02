// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace HiringManager.Web.Controllers
{
    public partial class PositionsController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected PositionsController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Index()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Index);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Candidates()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Candidates);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult NewCandidate()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.NewCandidate);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Pass()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Pass);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Status()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Status);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ViewResult Hire()
        {
            return new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Hire);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult Close()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Close);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public PositionsController Actions { get { return MVC.Positions; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Positions";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Positions";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string Index = "Index";
            public readonly string Create = "Create";
            public readonly string Candidates = "Candidates";
            public readonly string NewCandidate = "NewCandidate";
            public readonly string Pass = "Pass";
            public readonly string Status = "Status";
            public readonly string Hire = "Hire";
            public readonly string Close = "Close";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string Index = "Index";
            public const string Create = "Create";
            public const string Candidates = "Candidates";
            public const string NewCandidate = "NewCandidate";
            public const string Pass = "Pass";
            public const string Status = "Status";
            public const string Hire = "Hire";
            public const string Close = "Close";
        }


        static readonly ActionParamsClass_Index s_params_Index = new ActionParamsClass_Index();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Index IndexParams { get { return s_params_Index; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Index
        {
            public readonly string status = "status";
        }
        static readonly ActionParamsClass_Create s_params_Create = new ActionParamsClass_Create();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Create CreateParams { get { return s_params_Create; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Create
        {
            public readonly string viewModel = "viewModel";
        }
        static readonly ActionParamsClass_Candidates s_params_Candidates = new ActionParamsClass_Candidates();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Candidates CandidatesParams { get { return s_params_Candidates; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Candidates
        {
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_NewCandidate s_params_NewCandidate = new ActionParamsClass_NewCandidate();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_NewCandidate NewCandidateParams { get { return s_params_NewCandidate; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_NewCandidate
        {
            public readonly string id = "id";
            public readonly string viewModel = "viewModel";
        }
        static readonly ActionParamsClass_Pass s_params_Pass = new ActionParamsClass_Pass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Pass PassParams { get { return s_params_Pass; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Pass
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Status s_params_Status = new ActionParamsClass_Status();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Status StatusParams { get { return s_params_Status; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Status
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Hire s_params_Hire = new ActionParamsClass_Hire();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Hire HireParams { get { return s_params_Hire; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Hire
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_Close s_params_Close = new ActionParamsClass_Close();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_Close CloseParams { get { return s_params_Close; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_Close
        {
            public readonly string id = "id";
            public readonly string model = "model";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string Candidates = "Candidates";
                public readonly string Close = "Close";
                public readonly string Create = "Create";
                public readonly string Hire = "Hire";
                public readonly string Index = "Index";
                public readonly string NewCandidate = "NewCandidate";
                public readonly string Pass = "Pass";
                public readonly string Status = "Status";
            }
            public readonly string Candidates = "~/Views/Positions/Candidates.cshtml";
            public readonly string Close = "~/Views/Positions/Close.cshtml";
            public readonly string Create = "~/Views/Positions/Create.cshtml";
            public readonly string Hire = "~/Views/Positions/Hire.cshtml";
            public readonly string Index = "~/Views/Positions/Index.cshtml";
            public readonly string NewCandidate = "~/Views/Positions/NewCandidate.cshtml";
            public readonly string Pass = "~/Views/Positions/Pass.cshtml";
            public readonly string Status = "~/Views/Positions/Status.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_PositionsController : HiringManager.Web.Controllers.PositionsController
    {
        public T4MVC_PositionsController() : base(Dummy.Instance) { }

        partial void IndexOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, string status);

        public override System.Web.Mvc.ViewResult Index(string status)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Index);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "status", status);
            IndexOverride(callInfo, status);
            return callInfo;
        }

        partial void CreateOverride(T4MVC_System_Web_Mvc_ViewResult callInfo);

        public override System.Web.Mvc.ViewResult Create()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Create);
            CreateOverride(callInfo);
            return callInfo;
        }

        partial void CreateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, HiringManager.Web.ViewModels.Positions.CreatePositionViewModel viewModel);

        public override System.Web.Mvc.ActionResult Create(HiringManager.Web.ViewModels.Positions.CreatePositionViewModel viewModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Create);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "viewModel", viewModel);
            CreateOverride(callInfo, viewModel);
            return callInfo;
        }

        partial void CandidatesOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, int id);

        public override System.Web.Mvc.ViewResult Candidates(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Candidates);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            CandidatesOverride(callInfo, id);
            return callInfo;
        }

        partial void NewCandidateOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, int id);

        public override System.Web.Mvc.ViewResult NewCandidate(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.NewCandidate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            NewCandidateOverride(callInfo, id);
            return callInfo;
        }

        partial void NewCandidateOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, HiringManager.Web.ViewModels.Positions.NewCandidateViewModel viewModel);

        public override System.Web.Mvc.ActionResult NewCandidate(HiringManager.Web.ViewModels.Positions.NewCandidateViewModel viewModel)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.NewCandidate);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "viewModel", viewModel);
            NewCandidateOverride(callInfo, viewModel);
            return callInfo;
        }

        partial void PassOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, int id);

        public override System.Web.Mvc.ViewResult Pass(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Pass);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            PassOverride(callInfo, id);
            return callInfo;
        }

        partial void PassOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, HiringManager.Web.ViewModels.Positions.CandidateStatusViewModel model);

        public override System.Web.Mvc.ActionResult Pass(HiringManager.Web.ViewModels.Positions.CandidateStatusViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Pass);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            PassOverride(callInfo, model);
            return callInfo;
        }

        partial void StatusOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, int id);

        public override System.Web.Mvc.ViewResult Status(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Status);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            StatusOverride(callInfo, id);
            return callInfo;
        }

        partial void StatusOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, HiringManager.Web.ViewModels.Positions.CandidateStatusViewModel model);

        public override System.Web.Mvc.ActionResult Status(HiringManager.Web.ViewModels.Positions.CandidateStatusViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Status);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            StatusOverride(callInfo, model);
            return callInfo;
        }

        partial void HireOverride(T4MVC_System_Web_Mvc_ViewResult callInfo, int id);

        public override System.Web.Mvc.ViewResult Hire(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ViewResult(Area, Name, ActionNames.Hire);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            HireOverride(callInfo, id);
            return callInfo;
        }

        partial void HireOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, HiringManager.Web.ViewModels.Positions.CandidateStatusViewModel model);

        public override System.Web.Mvc.ActionResult Hire(HiringManager.Web.ViewModels.Positions.CandidateStatusViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Hire);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            HireOverride(callInfo, model);
            return callInfo;
        }

        partial void CloseOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        public override System.Web.Mvc.ActionResult Close(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Close);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            CloseOverride(callInfo, id);
            return callInfo;
        }

        partial void CloseOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, HiringManager.Web.ViewModels.Positions.ClosePositionViewModel model);

        public override System.Web.Mvc.ActionResult Close(HiringManager.Web.ViewModels.Positions.ClosePositionViewModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.Close);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            CloseOverride(callInfo, model);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
