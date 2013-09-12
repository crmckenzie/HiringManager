using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager.DomainServices
{
    public interface IHiringService
    {
        HireCandidateResponse Hire(HireCandidateRequest request);
    }
}
