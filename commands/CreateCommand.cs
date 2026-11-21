using System.Text;
using System.Xml.Linq;
using Octokit;

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

    static Command CreateCommand(HttpClient client, GitHubClient gitHubClient)
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

            List<ProblemSolution> filteredProblemSolutions = problemSolutions
                .Where(problemSolution => MatchesLangDate(language, date, problemSolution))
                .Select(problemSolution => problemSolution.Data!)
                .ToList();

            List<File> files = filteredProblemSolutions
                .Select(problemSolution => new File(problemSolution.Question.TitleSlug, BuildMarkDownFile(problemSolution)))
                .ToList();

            await UploadFiles(gitHubClient, language, date, files);
            Console.WriteLine("Successfully uploaded files!");
        });
        return createCommand;
    }

    static bool MatchesLangDate(string language, DateOnly date, Response<ProblemSolution> problemSolution)
    {
        //convert timestamp in problemSolution to dateonly
        DateOnly submissionDate = DateOnly.FromDateTime(
            DateTimeOffset.FromUnixTimeSeconds(problemSolution.Data!.SubmissionDetails.Timestamp).LocalDateTime
        );
        
        if (problemSolution.Data!.SubmissionDetails.Lang.Name == language && submissionDate == date)
            return true;
        
        return false;
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