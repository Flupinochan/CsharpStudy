using NlogTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NlogTest.Services;
public sealed class DefaultConsole : IConsole
{
    public Boolean IsEnabled { get; set; } = true;

    public void WriteLine(String message)
    {
        if(IsEnabled is false)
        {
            return;
        }

        Debug.WriteLine(message);
    }
}
