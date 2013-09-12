using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager.DomainServices
{
    public interface IHiringService
    {
        CreatePositionResponse CreatePosition(CreatePositionRequest request);
        HireCandidateResponse Hire(HireCandidateRequest request);
    }
}
