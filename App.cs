#!/usr/bin/env dotnet
#:package System.CommandLine@3.0.0
#:include commands/CreateCommand.cs
#:include queries/*.cs
#:include httprequests/*.cs
#:include env/EnvironmentVariables.cs

global using System.CommandLine;

namespace Api;

partial class Program
{
    public static int Main(string[] args)
    {
        using HttpClient client = new()
        {
            BaseAddress = new Uri("https://leetcode.com/graphql")
        };

        Command createCommand = CreateCommand(client);

        RootCommand rootCommand = new RootCommand("Simple API to save solved LeetCode exercises to GitHub")
        {
            Subcommands = {createCommand}
        };

        return rootCommand.Parse(args).Invoke();
    }
}