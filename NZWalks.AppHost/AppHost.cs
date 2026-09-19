var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.NZWalks_API>("nzwalks-api")
    .WithExternalHttpEndpoints();

builder.Build().Run();
