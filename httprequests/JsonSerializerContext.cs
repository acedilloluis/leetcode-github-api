using System.Text.Json;

namespace Api;

partial class Program
{
    [System.Text.Json.Serialization.JsonSerializable(typeof(Request<RecentSubmissionVariables>))]
    [System.Text.Json.Serialization.JsonSerializable(typeof(RecentSubmission))]
    [System.Text.Json.Serialization.JsonSerializable(typeof(ProblemSolution))]
    [System.Text.Json.Serialization.JsonSerializable(typeof(List<Response<ProblemSolution>>))]
    [System.Text.Json.Serialization.JsonSerializable(typeof(List<Response<RecentSubmission>>))]
    partial class RequestsJsonContext : System.Text.Json.Serialization.JsonSerializerContext;
}