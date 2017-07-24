using System.ServiceProcess;

namespace WindowsService
{
    static class Runner
    {
        static void Main()
        {
            ServiceBase[] services = new ServiceBase[]
            {
                new ReportingService()
            };

            ServiceBase.Run(services);
        }
    }
}
