using System.Collections.Generic;

namespace HiringManager.Web.ViewModels
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