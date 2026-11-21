namespace Api;

partial class Program
{
    const string PROBLEMSOLUTION = """
        query problemSolution($titleSlug: String!, $submissionId: Int!) {
            question(titleSlug: $titleSlug) {
                title
                titleSlug
                content
                mysqlSchemas
                note
                difficulty
                acRate
            }
            submissionDetails(submissionId: $submissionId) {
                code
                timestamp
                lang {
                    name
                }
            }
        }
    """;
}