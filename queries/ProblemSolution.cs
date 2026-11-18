namespace Api;

partial class Program
{
    const string PROBLEMSOLUTION = """
        query problemSolution($titleslug: String!, $submissionId: Int!) {
            question(titleslug: $titleslug) {
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