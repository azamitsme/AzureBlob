using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration;
using Azure.Data.Tables;
using CosmoDB.Common;


var builder = new ConfigurationBuilder();
builder.SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
 
IConfiguration config = builder.Build();

var storageCNSTR = config["ConnectionStrings:Storage"];

var tableServiceClient = new TableServiceClient(storageCNSTR);

var tableClient = tableServiceClient.GetTableClient(
       tableName: "Universities"
);

await tableClient.CreateIfNotExistsAsync();

var iMumbaiUniversity = new University(){
       RowKey = "i-mumbai-university-mi-ind",
       PartitionKey = "i-mumbai-university",
       Name = "Mumbai University",
       Location = "Mumbai India",
       YearFounded = 1984
};

await tableClient.AddEntityAsync<University>(iMumbaiUniversity);