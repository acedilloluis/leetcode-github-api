namespace Api;

partial class Program
{
    const string PROBLEMSOLUTION = """
        query problemSolution($titleSlug: String!, $submissionId: Int!) {
            question(titleSlug: $titleSlug) {
                content
                mysqlSchemas
                difficulty
                acRate
            }
            submissionDetails(submissionId: $submissionId) {
                code
                timestamp
                statusCode
                lang {
                    name
                }
            }
        }
    """;
}