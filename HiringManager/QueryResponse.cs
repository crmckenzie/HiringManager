using System.Collections.Generic;

namespace HiringManager
{
    public class QueryResponse<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public IList<T> Data { get; set; }
    }
}