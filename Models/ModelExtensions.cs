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
                ActiveDirectoryId = userInfo.UserActiveDirectoryID,
                Email = userInfo.Email,
                DisplayName = userInfo.UserDisplayName,
                ListOfTodoItems = JsonConvert.SerializeObject(userInfo.ListOfTodoItems),
                RowKey = userInfo.UserActiveDirectoryID,
                PartitionKey = "1"
            };
            return todoListEntity;
        }

        public static UserInfoDTO ToDto(this TodoListEntity todoListEntity)
        {
            var userInfo = new UserInfoDTO
            {
                UserActiveDirectoryID = todoListEntity.ActiveDirectoryId,
                Email = todoListEntity.Email,
                UserDisplayName = todoListEntity.DisplayName,
                ListOfTodoItems = JsonConvert.DeserializeObject<List<TodoItemDTO>>(todoListEntity.ListOfTodoItems)
            };
            return userInfo;
        }
    }
}
