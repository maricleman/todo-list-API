using Microsoft.Azure.Cosmos.Table;
using Microsoft.Extensions.Configuration;
using Models.Config;
using Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DbRepository : IDbRepository
    {
        private const string TableName = "TodoList";
        private readonly ConnectionStrings connectionStrings;

        public DbRepository(ConnectionStrings connectionStrings)
        {
            this.connectionStrings = connectionStrings;
        }

        /// <summary>
        /// For performing any operation on Azure Tables, first we need to define
        /// a TableOperation and then execute that on the CloudTable object.
        /// 
        /// Every table operation will return a TableResult object which will have the execution
        /// details such as the status, the result object, etc. The ExecuteTableOperation()
        /// method handles all these.
        /// </summary>
        /// <param name="ActiveDirectoryId"></param>
        /// <param name="DisplayName"></param>
        /// <returns></returns>
        public TodoListEntity RetrieveTodoListItem(string ActiveDirectoryId)
        {

            var condition1 = TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "1");
            var condition2 = TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, ActiveDirectoryId);
            var query = new TableQuery<TodoListEntity>().Where(condition1)
                                                        .Where(condition2);
            var dbTable = GetCloudTable(TableName);
            var results = dbTable.ExecuteQuery(query).FirstOrDefault();

            return results;
        }

        public TableResult InsertOrUpdateTodoItems(TodoListEntity todoListEntity)
        {
            var insertOrUpdateOperation = TableOperation.InsertOrReplace(todoListEntity);
            var dbTable = GetCloudTable(TableName);
            var result = dbTable.Execute(insertOrUpdateOperation);
            return result;
        }

        public async Task<TodoListEntity> DeleteAsync(TodoListEntity entity)
        {
            var deleteOperation = TableOperation.Delete(entity);
            return await ExecuteTableOperation(deleteOperation) as TodoListEntity;
        }

        public async Task<TodoListEntity> InsertOrMergeAsync(TodoListEntity entity)
        {
            try
            {
                var table = GetCloudTable(TableName);
                TableOperation insertOperation = TableOperation.InsertOrMerge(entity);
                await table.ExecuteAsync(insertOperation);

                var query = new TableQuery<TodoListEntity>();
                var lst = table.ExecuteQuery(query);
                return lst.FirstOrDefault();
            }
            catch (Exception ex)
            {
                Debug.Write($"Error: {ex.Message}, Stack trace: {ex.StackTrace}");
                throw;
            }
            
        }

        private async Task<object> ExecuteTableOperation(TableOperation tableOperation)
        {
            try
            {
                var table = GetCloudTable(TableName);
                var tableResult = await table.ExecuteAsync(tableOperation);
                var result = tableResult.Result;
                return tableResult.Result;
            }
            catch (Exception ex)
            {
                Debug.Write($"Error: {ex.Message}, Stack trace: {ex.StackTrace}");
                throw;
            }
            
        }

        /// <summary>
        /// Connect to the Azure storage account.
        /// If the table does not exist, create it and return it.
        /// </summary>
        /// <returns></returns>
        private CloudTable GetCloudTable(string tableName)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionStrings.AzureStorageConnectionString);
            var tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            var table = tableClient.GetTableReference(tableName);
            table.CreateIfNotExists();
            return table;
        }
    }
}
