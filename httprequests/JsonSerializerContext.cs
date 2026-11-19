using System.Text.Json.Serialization;

namespace Api;

partial class Program
{
    [JsonSourceGenerationOptions(PropertyNamingPolicy = JsonKnownNamingPolicy.CamelCase)]
    [JsonSerializable(typeof(Request))]
    [JsonSerializable(typeof(RecentSubmission))]
    [JsonSerializable(typeof(ProblemSolution))]
    [JsonSerializable(typeof(Response<ProblemSolution>))]
    [JsonSerializable(typeof(Response<RecentSubmissionsData<List<RecentSubmission>>>))]
    partial class RequestsJsonContext : JsonSerializerContext;
}