using Microsoft.Azure.Cosmos.Table;
using Models.DTOs;
using Models.EntityModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class ModelExtensions
    {
        public static TodoListEntity ToEntityModel(this UserInfoDTO userInfo)
        {
            var todoListEntity = new TodoListEntity
            {
                ActiveDirectoryId = userInfo.user_active_directory_id,
                Email = userInfo.email,
                DisplayName = userInfo.user_display_name,
                ListOfTodoItems = JsonConvert.SerializeObject(userInfo.list_of_todo_items),
                RowKey = userInfo.user_active_directory_id,
                PartitionKey = "1",
                Timestamp = DateTime.UtcNow
            };
            return todoListEntity;
        }

        public static UserInfoDTO ToDto(this TodoListEntity todoListEntity)
        {
            var userInfo = new UserInfoDTO
            {
                user_active_directory_id = todoListEntity.ActiveDirectoryId,
                email = todoListEntity.Email,
                user_display_name = todoListEntity.DisplayName,
                list_of_todo_items = JsonConvert.DeserializeObject<List<TodoItemDTO>>(todoListEntity.ListOfTodoItems)
            };
            return userInfo;
        }
    }
}
