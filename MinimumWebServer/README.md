# Walkthrough 2 - Minimum Web Server

## Setup

This part of the walkthrough will set up a minimum web server.

- Start Visual Studio, and click `Create a new project`.
- Set language to `C#`, and project type to `Console`.
- Select the `Console Application` template, click `Next`.
- Set Project name to `MinimumWebServer`, and click `Next`.
- Ensure `Target Framework` is `.NET 5.0`, click `Create`.

## Program.cs (role of the `Main` method)

- Press **Ctrl+F5** to run the program. The output is as expected.
- Change the name of the class to `App`.

```cs
class App
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
    }
}
```

- Run the program. The output is unchanged.
- In the **Solution Explorer**, change the name of the file to `App.cs`.
- Run the program. The output is unchanged.
- Remove the `Main` method's parameters.

```cs
class App
{
    static void Main()
    {
        Console.WriteLine("Hello World!");
    }
}
```

- Run the program. The output is unchanged.
- Change the return type to `string` and return a string.

```cs
class App
{
    static string Main()
    {
        Console.WriteLine("Hello World!");
        return "hello";
    }
}
```

- Run the program. It doesn't compile; inspect the error.
- Change the return type to `int` and update the return statement.

```cs
class App
{
    static int Main()
    {
        Console.WriteLine("Hello World!");
        return 0;
    }
}
```

- Run the program. The output is unchanged.
- Change the return type back to `void` and remove the return statement.

```cs
class App
{
    static void Main()
    {
        Console.WriteLine("Hello World!");
    }
}
```

- Run the program. The output is unchanged,
- But notice that it still returns a `0`.
- Add a second `Main` method with the original method signature.

```cs
class App
{
    static void Main()
    {
        Console.WriteLine("Hello World!");
    }

    static void Main(string[] args)
    {
        Console.WriteLine("Main");
    }
}
```

- Run the program. It doesn't compile; inspect the error.
- Remove the second `Main` method.

```cs
class App
{
    static void Main()
    {
        Console.WriteLine("Hello World!");
    }
}
```

- Run the program. The output is unchanged.
- Change the name of the `Main` method.

```cs
class App
{
    static void First()
    {
        Console.WriteLine("Hello World!");
    }
}
```

- Run the program. It doesn't compile; inspect the error.
- Change the name back to `Main`.

```cs
class App
{
    static void Main()
    {
        Console.WriteLine("Hello World!");
    }
}
```

- Run the program. The output is unchanged.

## Getting started

- In **Solution Explorer**, right-click the project and select `Manage NuGet Packages...`
- Click the **Browse** tab and search for `kestrel`.
- Install `Microsoft.AspNetCore.Server.Kestrel`.
- Delete the `Console.Write` and begin creating a new web server.
- Add the `using Microsoft.AspNetCore.Hosting;` directive.

```cs
using Microsoft.AspNetCore.Hosting;

namespace MinimumWebServer
{
    class App
    {
        static void Main()
        {
            // Compilation error...
            new WebHostBuilder()
        }
   }
}
```

- Continue, use quick actions to add the `using Microsoft.AspNetCore.Builder;` directive when you get to `Run()`.

```cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace MinimumWebServer
{
    class App
    {
        static void Main()
        {
            new WebHostBuilder()
                .UseKestrel()
                // Compilation error...
                .Configure(app => app.Run(context => context.Response))
        }
    }
}
```

- Continue, use quick actions to add the `using Microsoft.AspNetCore.Http;` directive when you get to `WriteAsync`.

```cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace MinimumWebServer
{
    class App
    {
        static void Main()
        {
            new WebHostBuilder()
                .UseKestrel()
                // Compilation error...
                .Configure(app => app.Run(context => context.Response.WriteAsync()))
        }
    }
}
```

- Finish the statement.

```cs
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;

namespace MinimumWebServer
{
    class App
    {
        static void Main()
        {
            new WebHostBuilder()
                .UseKestrel()
                .Configure(app => app.Run(context => context
                    .Response
                    .WriteAsync("<h1>Minimum Web Server</h1>")))
                .Build()
                .Run();
        }
    }
}
```

- Run the program.
- From the console window that appears, **copy one of the URLs and paste it into a browser**.
- In the console window, shut down the application.
- From the **File** menu, close the solution.
