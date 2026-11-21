using Octokit;
using System.Net;

namespace Api;

partial class Program
{
    static HttpClient GetLeetCodeClient()
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

        HttpClient client = new(handler)
        {
            BaseAddress = graphqlEndpoint
        };

        client.DefaultRequestHeaders.Referrer = new Uri(baseAddress, LEETCODEUSERNAME);
        client.DefaultRequestHeaders.Add("x-csrftoken", CSRFTOKEN);

        return client;
    }

    static GitHubClient GetGitHubClient()
    {
        GitHubClient gitHubClient = new(new ProductHeaderValue("LeetCode-to-GitHu-API"))
        {
            Credentials = new Credentials(GITHUBTOKEN) 
        };

        return gitHubClient;
    }
}