using System;
using System.Threading.Tasks;

namespace Reporting
{
    public interface IReportCreator
    {
        Task CreateTradeVolumeReportAsync(DateTime date, string savePath);
    }
}
