using System.Text.Json.Nodes;

namespace Api;

partial class Program
{
    static async Task<Response<RecentSubmissionsData<List<RecentSubmission>>>> GetRecentSubmissions(HttpClient client)
    {
        Request query = new(
            RECENTSUBMISSIONS,
            new JsonObject 
            {
                ["username"] = USERNAME,
                ["limit"] = RECENTSUBMISSIONSLIMIT
            }
        );

        using HttpResponseMessage response = await client.PostAsJsonAsync<Request>(
            "",
            query,
            RequestsJsonContext.Default.Request
        );

        Response<RecentSubmissionsData<List<RecentSubmission>>> recentSubmissions = await response.Content.ReadFromJsonAsync(
            RequestsJsonContext.Default.ResponseRecentSubmissionsDataListRecentSubmission
        ) ?? throw new InvalidOperationException("The recent submissions response was empty.");

        return recentSubmissions;
    }
}