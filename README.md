RpmApiClient-CSharp
===================

A C# Client for [RPM's API][API].

## Setup

The only dependency for this client is __[RestSharp][RestSharp]__.

You can get it by installing [NuGet][NuGet] on Visual Studio and then using the menu:

> Tools > NuGet Package Manager > Manage NuGet Packages for solution.

## Tests and Usage

The RpmApiTests project contains a suite of tests tha show basic usage of the API Client.

To setup:

1. Rename ApiSettingsSample.cs to ApiSettings.cs (make sure the class name also changes).
2. Fill in the information required on ApiSettings.cs (RPM url and API key).
3. You should be able to run the tests on TestRpmApi.cs

[API]: http://api.rpmsoftware.com/
[RestSharp]: http://restsharp.org/
[NuGet]: http://www.nuget.org/

