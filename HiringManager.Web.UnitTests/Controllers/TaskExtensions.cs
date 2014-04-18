using System.Threading;
using System.Threading.Tasks;

namespace HiringManager.Web.UnitTests.Controllers
{
    public static class TaskExtensions
    {
        public static T ExecuteSynchronously<T>(this Task<T> task)
        {
            T result = default(T);
            var wait = new ManualResetEvent(initialState: false);
            task.ContinueWith(r =>
            {
                result = r.Result;
                wait.Set();
            })
                ;
            wait.WaitOne();

            return result;
        }
    }
}
