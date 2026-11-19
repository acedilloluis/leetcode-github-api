namespace Api;

partial class Program
{
    const string PROBLEMSOLUTION = """
        query problemSolution($titleSlug: String!, $submissionId: Int!) {
            question(titleSlug: $titleSlug) {
                title
                content
                mysqlSchemas
                note
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