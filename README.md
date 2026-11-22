# A LeetCode to GitHub API

A simple API connecting LeetCode's GraphQL endpoint to GitHub's REST API via the command line. It queries LeetCode for recently accepted submissions, creates a markdown file for each submission containing the problem statement and solution, and uploads those files to a GitHub repository. 

## Usage

- Upload new markdown files for submissions in C# and submitted on 11-21-2026
```
create csharp -d 11-21-2026
```