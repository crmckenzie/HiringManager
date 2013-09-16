using System.Collections;
using System.Collections.Generic;

namespace HiringManager.Web.Models
{
    public class IndexViewModel<T>
    {
        public IList<T> Data { get; set; }
    }
}