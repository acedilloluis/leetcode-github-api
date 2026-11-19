using System.Text.Json.Nodes;

namespace Api;

partial class Program
{
    //graphql request wrapper
    record Request(
        string Query,
        JsonNode Variables
    );

    //graphql response wrapper
    record Response<T>(
        T? Data,
        List<Error>? Errors
    );

    //graphql error wrapper
    record Error(
        string Message
    );

    //recent submission query types
    record RecentSubmission(
        string Id,
        string TitleSlug
    );

    record RecentSubmissionsData<T>(
        T RecentAcSubmissionList
    );

    //problem solution query types
    record Question(
        string Content,
        string[] MySqlSchemas,
        string Difficulty,
        float AcRate
    );

    record Submission(
        string Code,
        int Timestamp,
        int StatusCode,
        LanguageName Lang
    );

    record LanguageName(
        string Name
    );

    record ProblemSolution(
        Question Question,
        Submission SubmissionDetails
    );
}