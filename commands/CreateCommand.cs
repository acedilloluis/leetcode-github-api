using System.Text;
using System.Xml.Linq;

namespace Api;

partial class Program
{
    static Argument<string> languageArg = new Argument<string>("language")
    {
        Description = "Programming language that requested solutions should be written in"    
    };

    static Argument<DateOnly> dateArg = new Argument<DateOnly>("date")
    {
        Description = "Date when requested solutions were submitted"
    };

    static Command createCommand = new Command("create", "Make MarkDown files for requested solutions")
    {
        Arguments = { languageArg, dateArg }
    };

    static Command CreateCommand(HttpClient client)
    {
        createCommand.SetAction(async parseResult =>
        {
            string language = parseResult.GetValue(languageArg)!;
            DateOnly date = parseResult.GetValue(dateArg);

            Response<RecentSubmissionsData<List<RecentSubmission>>> recentSubmissions = 
                await GetRecentSubmissions(client);

            List<Response<ProblemSolution>> problemSolutions = new();
            //get problem statements and solutions by submissionId
            foreach (RecentSubmission recentSubmission in recentSubmissions.Data!.RecentAcSubmissionList!)
            {
                Response<ProblemSolution> problemSolution = await GetProblemSolution(
                    recentSubmission.Id,
                    recentSubmission.TitleSlug, 
                    client
                );
                problemSolutions.Add(problemSolution);
            }

            List<ProblemSolution> filteredProblemSolutions = FilterProblemSolutions(language, date, problemSolutions);
            Console.WriteLine(BuildMarkDownFile(filteredProblemSolutions[0]));
            //allow garbage collector to reclaim memory; problemsolutions no longer needed
            problemSolutions = null!;
        });
        return createCommand;
    }

    static List<ProblemSolution> FilterProblemSolutions(string language, DateOnly date, List<Response<ProblemSolution>> problemSolutions)
    {
        List<ProblemSolution> unwrappedAndFiltered = new();

        foreach (Response<ProblemSolution> problemSolution in problemSolutions)
        {
            //convert timestamp in problemSolution to dateonly
            DateOnly submissionDate = DateOnly.FromDateTime(
                DateTimeOffset.FromUnixTimeSeconds(problemSolution.Data!.SubmissionDetails.Timestamp).LocalDateTime
            );
            if (problemSolution.Data!.SubmissionDetails.Lang.Name == language && submissionDate == date)
                unwrappedAndFiltered.Add(problemSolution.Data!);
        }
        return unwrappedAndFiltered;
    }

    static string BuildMarkDownFile(ProblemSolution problemSolution)
    {
        StringBuilder sb = new();
        //remove html tags from problem statement
        string problemStatement = XElement.Parse(
            "<root>" + problemSolution.Question.Content.Replace("&nbsp;", " ") + "</root>").Value;

        sb.Append($"## {problemSolution.Question.Title}\n");
        sb.Append("### Problem Statement\n");
        sb.Append(problemStatement + "\n");
        sb.Append("### Solution\n");
        sb.Append($"```{problemSolution.SubmissionDetails.Lang.Name}\n");
        sb.Append(problemSolution.SubmissionDetails.Code + "\n");
        sb.Append("```\n");
        sb.Append("### Notes\n");
        sb.Append(problemSolution.Question.Note);

        return sb.ToString();
    }
}