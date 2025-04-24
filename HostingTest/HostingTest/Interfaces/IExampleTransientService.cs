using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostingTest.Interfaces;
public interface IExampleTransientService : IReportServiceLifetime
{
    ServiceLifetime IReportServiceLifetime.Lifetime
    {
        get => ServiceLifetime.Transient;
    }
}
