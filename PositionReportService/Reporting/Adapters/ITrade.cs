using System;
using System.Collections.Generic;

namespace Reporting
{
    internal interface ITrade
    {
        DateTime Date { get; }

        IDictionary<int, double> VolumePerPeriod { get; }
    }
}
