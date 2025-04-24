using HostingTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostingTest.Services;
public sealed class ExampleTransientService:IExampleTransientService
{
    public Guid Id { get => Guid.NewGuid(); }
}
