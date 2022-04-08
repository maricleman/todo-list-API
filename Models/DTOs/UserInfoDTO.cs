using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class UserInfoDTO
    {
        [JsonProperty("user_active_directory_id")]
        public string user_active_directory_id { get; set; }
        [JsonProperty("user_display_name")]
        public string user_display_name { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("list_of_todo_items")]
        public List<TodoItemDTO> list_of_todo_items { get; set; }
    }
}
