
namespace CleverenceTasks.Task3
{
    public class Disposable : IDisposable
    {
        protected virtual void Dispose(bool disposing)
        {
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
            this.Dispose(true);
        }

        ~Disposable() 
        {
            this.Dispose(false);
        }
    }
}
