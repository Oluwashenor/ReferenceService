using Microsoft.Azure.Cosmos;
using ReferenceService.API.Data.NoSql.Models;

namespace ReferenceService.API.Data.NoSql
{
    public abstract class CosmosRepository<T> where T : CosmosModel
    {
        protected readonly Container Container;
        protected CosmosRepository(CosmosClient client, string databaseId, string containerId)
        {
            Container = client.GetContainer(databaseId, containerId);
        }
    }
}
