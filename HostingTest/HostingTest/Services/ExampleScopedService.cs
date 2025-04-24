using HostingTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostingTest.Services;
public class ExampleScopedService : IExampleScopedService
{
    Guid IReportServiceLifetime.Id { get; } = Guid.NewGuid();
}
