using System.Text.Json.Nodes;

namespace Api;

partial class Program
{
    static async Task<Response<ProblemSolution>> GetProblemSolution(string id, string title, HttpClient client)
    {
        Request query = new(
            PROBLEMSOLUTION,
            new JsonObject
            {
                ["submissionId"] = Convert.ToUInt64(id),
                ["titleSlug"] = title
            }
        );

        using HttpResponseMessage response = await client.PostAsJsonAsync<Request>(
            "",
            query,
            RequestsJsonContext.Default.Request
        );

        Response<ProblemSolution> problemSolution = await response.Content.ReadFromJsonAsync(
            RequestsJsonContext.Default.ResponseProblemSolution
        ) ?? throw new InvalidOperationException("The problem solution response was empty.");

        return problemSolution;
    }
}