using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Models.DTOs;
using Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TodoListManager.API.Controllers
{
    /// <summary>
    /// Thanks to the following link for helping me out!
    /// 
    /// https://gowthamcbe.com/2021/07/18/azure-table-storage-with-asp-net-core-web-api/
    /// </summary>
    [Route("api/[controller]")]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoListManager todoListManager;

        public TodoItemsController(ITodoListManager todoListManager)
        {
            this.todoListManager = todoListManager;
        }

        [HttpGet]
        [ActionName(nameof(RetrieveTodoListItem))]
        public IActionResult RetrieveTodoListItem([FromQuery] string ActiveDirectoryId)
        {
            try
            {
                var results = todoListManager.RetrieveTodoListItem(ActiveDirectoryId);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// This will either modify or
        /// insert the user's profile info
        /// and their associated todo list.
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult AddOrModifyTodoItems([FromBody] UserInfoDTO userInfo)
        {
            try
            {
                var results = todoListManager.InsertOrModifyUsersTodoItems(userInfo);
                if (results.HttpStatusCode == 204)
                {
                    return Ok(results.Etag);
                } else
                {
                    return BadRequest(results.Result);
                }
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
