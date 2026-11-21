#!/usr/bin/env dotnet
#:package System.CommandLine@3.0.0
#:package Octokit@14.0.0
#:include commands/CreateCommand.cs
#:include queries/*.cs
#:include httprequests/*.cs
#:include json/*.cs
#:include env/EnvironmentVariables.cs

global using System.CommandLine;
using Octokit;

namespace Api;

partial class Program
{
    public static int Main(string[] args)
    {
        //create httpclinet to make requests to leetcode api
        using HttpClient client = GetLeetCodeClient();

        //create github client
        GitHubClient gitHubClient = GetGitHubClient();

        Command createCommand = CreateCommand(client, gitHubClient);

        RootCommand rootCommand = new("Simple API to save solved LeetCode exercises to GitHub")
        {
            Subcommands = {createCommand}
        };

        return rootCommand.Parse(args).Invoke();
    }
}