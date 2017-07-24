using System;
using System.Collections.Generic;

namespace Reporting
{
    public interface ITrade
    {
        DateTime Date { get; }

        IDictionary<int, double> VolumePerPeriod { get; }
    }
}
