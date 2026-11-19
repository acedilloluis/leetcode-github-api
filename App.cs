#!/usr/bin/env dotnet
#:package System.CommandLine@3.0.0
#:include commands/CreateCommand.cs
#:include queries/*.cs
#:include httprequests/*.cs
#:include env/EnvironmentVariables.cs

global using System.CommandLine;
using System.Net;

namespace Api;

partial class Program
{
    public static int Main(string[] args)
    {
        Uri baseAddress = new(BASEADDRESS);
        Uri graphqlEndpoint = new(baseAddress, "graphql");

        CookieContainer cookieContainer = new();
        cookieContainer.Add(graphqlEndpoint, new Cookie("csrftoken", CSRFTOKEN));
        cookieContainer.Add(graphqlEndpoint, new Cookie("LEETCODE_SESSION", LEETCODESESSION));

        HttpClientHandler handler = new()
        {
            CookieContainer = cookieContainer
        };

        using HttpClient client = new(handler)
        {
            BaseAddress = graphqlEndpoint
        };

        client.DefaultRequestHeaders.Referrer = new Uri(baseAddress, USERNAME);
        client.DefaultRequestHeaders.Add("x-csrftoken", CSRFTOKEN);

        Command createCommand = CreateCommand(client);

        RootCommand rootCommand = new("Simple API to save solved LeetCode exercises to GitHub")
        {
            Subcommands = {createCommand}
        };

        return rootCommand.Parse(args).Invoke();
    }
}