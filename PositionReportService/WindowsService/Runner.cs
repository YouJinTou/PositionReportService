using System.ServiceProcess;

namespace WindowsService
{
    public static class Runner
    {
        public static void Main()
        {
            ServiceBase[] services = new ServiceBase[]
            {
                new ReportingService()
            };

            ServiceBase.Run(services);
        }
    }
}
