var target = Argument("target", "Test");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .WithCriteria(c => HasArgument("rebuild"))
    .Does(() =>
{
    CleanDirectory($"./src/TestGithubPipeline/bin/{configuration}");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() =>
{
    DotNetBuild("./src/TestGithubPipeline.sln", new DotNetBuildSettings
    {
        Configuration = configuration,
    });
});

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
{
    DotNetTest("./src/TestGithubPipeline.sln", new DotNetTestSettings
    {
        Configuration = configuration,
        NoBuild = true,
    });
});

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);