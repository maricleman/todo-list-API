using DataLayer;
using Microsoft.Azure.Cosmos.Table;
using Models;
using Models.DTOs;
using Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class TodoListManager : ITodoListManager
    {
        private readonly IDbRepository dbRepository;

        public TodoListManager(IDbRepository dbRepository)
        {
            this.dbRepository = dbRepository;
        }

        public UserInfoDTO RetrieveTodoListItem(string ActiveDirectoryId)
        {
            try
            {
                var result = dbRepository.RetrieveTodoListItem(ActiveDirectoryId);
                return result.ToDto();
            }
            catch (Exception ex)
            {
                Debug.Write($"Error: {ex.Message}, Stack trace: {ex.StackTrace}");
                throw;
            }
            
        }

        public TableResult InsertOrModifyUsersTodoItems(UserInfoDTO userInfoDTO)
        {
            try
            {
                var result = dbRepository.InsertOrUpdateTodoItems(userInfoDTO.ToEntityModel());
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}, Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        //public async Task<TodoListEntity> DeleteAsync(string ActiveDirectoryId, string DisplayName)
        //{
        //    var entity = await dbRepository.RetrieveAsync(ActiveDirectoryId, DisplayName);
        //    var result = await dbRepository.DeleteAsync(entity);

        //    return result;
        //}

        //public async Task<TodoListEntity> InsertOrMergeAsync(TodoListEntity entity)
        //{
        //    entity.PartitionKey = entity.Email;
        //    entity.RowKey = entity.ActiveDirectoryId;
        //    var result = await dbRepository.InsertOrMergeAsync(entity);
        //    return result;
        //}
    }
}
