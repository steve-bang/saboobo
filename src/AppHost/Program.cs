

var builder = DistributedApplication.CreateBuilder(args);

var postgreDB = builder.AddPostgres("postgres")
                .WithPgAdmin()
                .WithLifetime(ContainerLifetime.Persistent);

var productDb = postgreDB.AddDatabase("Product");

builder.AddProject<Projects.Product_Api>("productservice")
        .WithReference(productDb)
        .WaitFor(productDb);

builder.Build().Run();
