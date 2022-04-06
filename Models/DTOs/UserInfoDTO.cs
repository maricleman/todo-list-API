using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UserInfoDTO
    {
        public string UserActiveDirectoryID { get; set; }
        public string UserDisplayName { get; set; }
        public string Email { get; set; }
        public List<TodoItemDTO> ListOfTodoItems { get; set; }
    }
}
