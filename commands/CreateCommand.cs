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
           
        });

        return createCommand;
    }
}