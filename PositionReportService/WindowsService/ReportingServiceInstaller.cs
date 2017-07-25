using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace WindowsService
{
    [RunInstaller(true)]
    public class ReportingServiceInstaller : Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller installer;

        public ReportingServiceInstaller()
        {
            this.processInstaller = new ServiceProcessInstaller() { Account = ServiceAccount.LocalSystem };
            this.installer = new ServiceInstaller()
            {
                StartType = ServiceStartMode.Manual,
                ServiceName = "Trade Reporting Service"
            };

            Installers.Add(this.processInstaller);
            Installers.Add(this.installer);
        }
    }
}
