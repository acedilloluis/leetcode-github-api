namespace Api;

partial class Program
{
    //graphql request wrapper
    record Request<T>(
        string Query,
        T Variables
    );

    record RecentSubmissionVariables(
        string Username,
        int Limit
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

    record RecentSubmission(
        int Id,
        string Title
    );

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