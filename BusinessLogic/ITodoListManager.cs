using Microsoft.Azure.Cosmos.Table;
using Models.DTOs;
using Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface ITodoListManager
    {
        UserInfoDTO RetrieveTodoListItem(string ActiveDirectoryId);
        TableResult InsertOrModifyUsersTodoItems(UserInfoDTO userInfoDTO);
    }
}
