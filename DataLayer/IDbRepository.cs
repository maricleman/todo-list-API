using Microsoft.Azure.Cosmos.Table;
using Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public interface IDbRepository
    {
        TodoListEntity RetrieveTodoListItem(string ActiveDirectoryId);
        Task<TodoListEntity> InsertOrMergeAsync(TodoListEntity entity);
        Task<TodoListEntity> DeleteAsync(TodoListEntity entity);
        TableResult InsertOrUpdateTodoItems(TodoListEntity todoListEntity);
    }
}
