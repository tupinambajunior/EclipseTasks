using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EclipseTasks.Core.Enum;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EclipseTasks.Core.Entities
{
    public class User: BaseEntity
    {
        public string Name { get; set; }
        public UserRole Role { get; set; }

        public User()
        {

        }

        public User(string id, string name, UserRole role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
    }
}
