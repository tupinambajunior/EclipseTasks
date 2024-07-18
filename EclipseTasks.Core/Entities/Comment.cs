using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Core.Entities
{
    public class Comment : BaseEntity
    {
        public string IdTask { get; set; }
        public string IdUser { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }

        public Comment()
        {

        }

        public Comment(string id, string idTask, string idUser, string name, string role, string content)
        {
            Id = id;
            IdTask = idTask;
            IdUser = idUser;
            Name = name;
            Role = role;
            Content = content;
            CreatedAt = DateTime.Now;
        }
    }
}
