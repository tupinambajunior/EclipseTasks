using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Core.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public Project()
        {
        }

        public Project(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
