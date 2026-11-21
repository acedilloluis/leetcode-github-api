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
        string Title,
        string TitleSlug,
        string Content,
        string[] MySqlSchemas,
        string Note,
        string Difficulty,
        float AcRate
    );

    record SubmissionDetails(
        string Code,
        int Timestamp,
        LanguageName Lang
    );

    record LanguageName(
        string Name
    );

    record ProblemSolution(
        Question Question,
        SubmissionDetails SubmissionDetails
    );

    record File(
        string Name,
        string Content
    );
}