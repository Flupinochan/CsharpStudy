using NlogTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlogTest.Services;
public sealed class DefaultGreetingService(IConsole console) : IGreetingService
{
    public String Greet(String name)
    {
        String greeting = $"Hello, {name}!";
        console.WriteLine(greeting);
        return greeting;
    }
}