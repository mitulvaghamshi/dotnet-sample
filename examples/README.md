# Walkthrough Examples

Examples are created with Visual Studio 2022, SQL Server 2022, and .NET v6.0 and
v7.0. All code remains same with slight less code compared to walkthrough which
are written in 2021.

## Changes:

- These examples does not contain `Startup.cs` file, code from this file is now
  combined into `Program.cs`.
- Project usage new implicite globals, means the `Program.cs` file does not
  contain any `class` or `Main` method definition, as per new style.
- All example were removed `Bootstrap and jQuery lib`s which you can download
  [Bootstrap with jQuery (non-blazor apps)](../resources/bootstrap-jquery.zip) and
  place inside `wwwroot/` folder (e.g. `wwwroot/lib/`) or...
- If working with Blazor apps, download
  [Bootstrap with Open-Iconic (blazor apps)](../resources/bootstrap-iconic.zip)
  and place inside `wwwroot/css/` folder (e.g. `wwwroot/css/bootsrap`
  and `wwwroot/css/open-iconic`).
