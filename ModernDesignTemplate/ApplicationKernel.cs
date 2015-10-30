using System;
using Ninject;

namespace ModernDesignTemplate
{
    public class ApplicationKernel : IDisposable
    {
        private static ApplicationKernel _instance;

        private ApplicationKernel()
        {
            Kernel = new StandardKernel();
            Kernel.Load<ApplicationModule>();
        }

        public static ApplicationKernel Instance
        {
            get { return _instance ?? (_instance = new ApplicationKernel()); }
        }

        public IKernel Kernel { get; private set; }

        public static T Get<T>()
        {
            return Instance.Kernel.Get<T>();
        }

        public static void ClearInstance()
        {
            if (_instance != null)
            {
                _instance.Dispose();
                _instance = null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Kernel != null)
                {
                    Kernel.Dispose();
                    Kernel = null;
                }
            }
        }
    }
}