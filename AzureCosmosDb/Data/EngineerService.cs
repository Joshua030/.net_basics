using AzureCosmosDb.Interfaces;
using Microsoft.Azure.Cosmos;
namespace AzureCosmosDb.Data;

public class EngineerService : IEngineerService
{
    //NOTE - cantake for the overview od the resource in azure or key option in the sidebar
    private readonly string CosmoDbConnectionString = "<change with your Cosmos DB connection string>";

    private readonly string CosmoDbName = "Contractors";
    private readonly string CosmoDbContainerName = "Engineers";

    private Container GetContainerClient()
    {
        CosmosClient cosmosDbClient = new(CosmoDbConnectionString);
        var container = cosmosDbClient.GetContainer(CosmoDbName, CosmoDbContainerName);
        return container;
    }

    public async Task UpsertEngineer(Engineer engineer)
    {
        try
        {
            if (string.IsNullOrEmpty(engineer.id))
            {
                engineer.id = Guid.NewGuid().ToString();
            }
            engineer.Id = engineer.id;
            var container = GetContainerClient();
            var updateRes = await container.UpsertItemAsync(engineer, new PartitionKey(engineer.Id));
            Console.Write(updateRes.StatusCode);
        }
        catch (Exception ex)
        {
            throw new Exception("Exception", ex);
        }
    }


    public async Task DeleteEngineer(string? id, string? partitionKey)
    {
        try
        {
            var container = GetContainerClient();
            var response = await container.DeleteItemAsync<Engineer>(id, new PartitionKey(partitionKey));
        }
        catch (Exception ex)
        {
            throw new Exception("Exception", ex);
        }
    }

    public async Task<List<Engineer>> GetEngineerDetails()
    {
        List<Engineer> engineers = new List<Engineer>();
        try
        {
            var container = GetContainerClient();
            var sqlQuery = "SELECT * FROM c";
            QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
            FeedIterator<Engineer> queryResultSetIterator = container.GetItemQueryIterator<Engineer>(queryDefinition);

            while (queryResultSetIterator.HasMoreResults)
            {
                FeedResponse<Engineer> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                foreach (Engineer engineer in currentResultSet)
                {
                    engineers.Add(engineer);
                }
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
        return engineers;
    }

    public async Task<Engineer> GetEngineerDetailsById(string? id, string? partitionKey)
    {
        try
        {
            var container = GetContainerClient();
            ItemResponse<Engineer> response = await container.ReadItemAsync<Engineer>(id, new PartitionKey(partitionKey));
            return response.Resource;
        }
        catch (Exception ex)
        {
            throw new Exception("Exception ", ex);
        }
    }

}
