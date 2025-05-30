﻿//==================================================
// Copyright (c) Coalition of Good-Hearted Engineers
// Free To Use To Bridge Knowledge and Curiosity
//==================================================

using System.Collections.Generic;
using ADotNet.Clients;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks;
using ADotNet.Models.Pipelines.GithubPipelines.DotNets.Tasks.SetupDotNetTaskV1s;

public class Program
{
    public static void Main(string[] args)
    {
        var githubPipeline = new GithubPipeline
        {
            Name = "BookSphere Build Pipeline",

            OnEvents = new Events
            {
                Push = new PushEvent
                {
                    Branches = new[] { "main" }
                },
                PullRequest = new PullRequestEvent
                {
                    Branches = new[] { "main" }
                }
            },

            Jobs = new Dictionary<string, Job>
            {
                {
                    "build",
                    new Job
                    {
                        RunsOn = "windows-latest",

                        Steps = new List<GithubTask>
                        {
                            new CheckoutTaskV2
                            {
                                Name = "Checking Out Code"
                            },

                            new SetupDotNetTaskV1
                            {
                                Name = "Setting Up .Net",
                                TargetDotNetVersion = new TargetDotNetVersion
                                {
                                    DotNetVersion = "9.0.102"
                                }
                            },

                            new RestoreTask
                            {
                                Name = "Restoring NuGet Packages"
                            },

                            new DotNetBuildTask
                            {
                                Name = "Building Project"
                            },

                            new TestTask
                            {
                                Name = "Test"
                            }

                        }
                    }
                }
            }
        };

        var client = new ADotNetClient();

        client.SerializeAndWriteToFile(
            adoPipeline: githubPipeline,
            path: "../../../../.github/workflows/dotnet.yml");
    }
}
