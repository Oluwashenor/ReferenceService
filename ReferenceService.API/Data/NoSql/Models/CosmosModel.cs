using System.Text.Json.Serialization;

namespace ReferenceService.API.Data.NoSql.Models
{
    public class CosmosModel
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("partitionKey")]
        public required string PartitionKey { get; set; }
    }
}
