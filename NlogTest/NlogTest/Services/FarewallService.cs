using NlogTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlogTest.Services;
public class FarewellService(IConsole console)
{
    public String SayGoodbye(String name)
    {
        String farewell = $"Goodbye, {name}!";
        console.WriteLine(farewell);
        return farewell;
    }
}