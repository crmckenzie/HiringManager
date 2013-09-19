using System.Collections;
using System.Collections.Generic;

namespace HiringManager.Web.Models
{
    public class IndexViewModel<T>
    {
        public IndexViewModel()
        {
            this.Data = new List<T>();
        }
        public List<T> Data { get; set; }
    }
}