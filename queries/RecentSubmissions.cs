namespace Api;

partial class Program
{
    const string RECENTSUBMISSIONS = """
        query recentSubmissions($username: String!, $limit: Int!) {
            recentSubmissionList(username: $username, limit: $limit) {
                id
                titleslug
            }
        }
    """;
}