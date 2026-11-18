using System.ComponentModel;

namespace Api;

partial class Program
{

    static async Task<List<Response<RecentSubmission>>> GetRecentSubmissions(HttpClient client)
    {
        Request<RecentSubmissionVariables> query = new Request<RecentSubmissionVariables>(
            RECENTSUBMISSIONS,
            new RecentSubmissionVariables(
                USERNAME,
                RECENTSUBMISSIONSLIMIT
            )
        );

        try
        {
            using HttpResponseMessage response = await client.PostAsJsonAsync<Request<RecentSubmissionVariables>>(
                "",
                query,
                RequestsJsonContext.Default.RequestRecentSubmissionVariables
            );

            List<Response<RecentSubmission>> recentSubmissions = 
                await response.Content.ReadFromJsonAsync<List<Response<RecentSubmission>>>(
                    RequestsJsonContext.Default.ListResponseRecentSubmission)
                    ?? new List<Response<RecentSubmission>>();
            
            return recentSubmissions;
        }
        catch (HttpRequestException e)
        {
            Console.Error.WriteLine($"Exception caught! Message : {e.Message}");
            return new List<Response<RecentSubmission>>();
        }
    }
}