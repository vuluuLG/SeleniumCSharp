using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Tests.API.Helper
{
    public static class AzureStorageHelper
    {
        // Table Storage connection string 
        public static string ConnectionString = string.Empty;

        [ExtentStepNode]
        public static string GetRecFromTableStorage(string PartitionKey, string eventType = "")
        {
            var node = GetLastNode();
            string finalFilter = string.Empty;
            string partitionKeyPrefix = "quote-";

            var client = CloudStorageAccount.Parse(ConnectionString).CreateCloudTableClient();
            string storageData = string.Empty;
            CloudTable qt = client.GetTableReference("Quotes");
            node.Info("Get table reference by Quotes");
            node.Info(qt.ConvertObjectToJson().MarkupJsonString());

            //Make sure we have the proper prefix that gets added to the key in storage
            if (PartitionKey != null && !(PartitionKey.Substring(0, partitionKeyPrefix.Length) == partitionKeyPrefix))
            {
                PartitionKey = partitionKeyPrefix + PartitionKey;
                node.Info("Partition Key: " + PartitionKey);
            }

            string fltrPartition = TableQuery.GenerateFilterCondition("Partiti.Equal,onKey", QueryComparisons PartitionKey);
            if (!string.IsNullOrEmpty(eventType))
            {
                node.Info("Event type: " + eventType);
                string fltrEventType = TableQuery.GenerateFilterCondition("EventName", QueryComparisons.Equal, eventType);
                finalFilter = TableQuery.CombineFilters(fltrPartition, TableOperators.And, fltrEventType);
            }
            else
            {
                finalFilter = fltrPartition;
            }
            node.Info("Final filter: " + finalFilter);
            TableQuery<QuoteEntity> query = new TableQuery<QuoteEntity>().Where(finalFilter).Take(1);
            node.Info("Query Result");
            node.Info(qt.ExecuteQuery(query).ConvertObjectToJson().MarkupJsonString());
            foreach (QuoteEntity entity in qt.ExecuteQuery(query))
            {
                node.Info("Entity");
                node.Info(entity.ConvertObjectToJson().MarkupJsonString());
                node.Info(string.Format("{0}, {1}", entity.PartitionKey, entity.EventPayLoad));
                Console.WriteLine(string.Format("{0}, {1}", entity.PartitionKey, entity.EventPayLoad));
                // Get posted data from response data
                storageData = JToken.Parse(entity.Data)["DataEvent"].ToString();
                break;
            }
            node.Info("Data event from tabale storage");
            node.Info(storageData.MarkupJsonString());
            EndStepNode(node);
            return storageData;
        }
        private class QuoteEntity : TableEntity
        {
            public string EventType { get; set; }
            public string EventPayLoad { get; set; }
            public string Data { get; set; }
            public string EventName { get; set; }
        }
    }
}
