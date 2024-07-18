using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Core.Entities
{
    public class History : BaseEntity
    {
        public string IdTask { get; set; }
        public string IdUser { get; set; }
        public string Name { get; set; }
        public string Changes { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
