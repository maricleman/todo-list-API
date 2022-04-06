using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.EntityModels
{
    public class TodoListEntity : TableEntity
    {
        public string ActiveDirectoryId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string ListOfTodoItems { get; set; }

    }
}
