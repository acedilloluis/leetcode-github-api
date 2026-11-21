using Octokit;

namespace Api;

partial class Program
{
    static async Task UploadFiles(GitHubClient client, string language, DateOnly date, List<File> files)
    {
        Reference branchReference = await client.Git.Reference.Get(GITHUBUSERNAME, GITHUBREPONAME, "heads/main");
        string latestCommitSha = branchReference.Object.Sha;
        Commit latestCommit = await client.Git.Commit.Get(GITHUBUSERNAME, GITHUBREPONAME, latestCommitSha);

        NewTree tree = new()
        {
            BaseTree = latestCommit.Tree.Sha
        };

        //create blob for each file and add to tree
        foreach (File file in files)
        {
            NewBlob blob = new()
            {
                Encoding = EncodingType.Utf8,
                Content = file.Content
            };

            //upload blob to GitHub repo database to get Sha reference
            BlobReference blobReference = await client.Git.Blob.Create(GITHUBUSERNAME, GITHUBREPONAME, blob);

            tree.Tree.Add(new NewTreeItem
            {
               Path = $"{language}/{file.Name}.md",
               Mode = "100644", //non-executable file
               Type = TreeType.Blob,
               Sha = blobReference.Sha  
            });
        }

        //create tree, commit it, update branch pointer in GitHub
        TreeResponse createdTree = await client.Git.Tree.Create(GITHUBUSERNAME, GITHUBREPONAME, tree);
        NewCommit commit = new($"upload problems solved on {date}", createdTree.Sha, latestCommitSha);
        Commit createdCommit = await client.Git.Commit.Create(GITHUBUSERNAME, GITHUBREPONAME, commit);
        await client.Git.Reference.Update(
            GITHUBUSERNAME, 
            GITHUBREPONAME, 
            "heads/main", 
            new ReferenceUpdate(createdCommit.Sha)
        );
    }
}