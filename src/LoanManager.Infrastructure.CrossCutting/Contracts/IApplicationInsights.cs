using Microsoft.ApplicationInsights.DataContracts;
using System;
using System.Collections.Generic;

namespace LoanManager.Infrastructure.CrossCutting.Contracts
{
    public interface IApplicationInsights
    {
        void TrackEvent(EventTelemetry telemetry);
        void TrackException(Exception exception);
        void TrackException(Exception exception, IDictionary<string, string> properties);
    }
}
