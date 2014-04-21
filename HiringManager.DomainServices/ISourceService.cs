using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiringManager.DomainServices
{
    public interface ISourceService
    {
        QueryResponse<SourceSummary> Query(QuerySourcesRequest request);
    }
}
