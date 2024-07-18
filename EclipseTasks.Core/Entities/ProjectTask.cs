using EclipseTasks.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Core.Entities
{
    public class ProjectTask : BaseEntity
    {
        public string IdProject { get; set; }
        public string IdUser { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public Enum.TaskStatus Status { get; set; }
        public TaskPriority Priority { get; set; }

        public ProjectTask()
        {
        }

        public ProjectTask(string id, string idProject, string idUser, string title, string description, DateTime? dueDate, TaskPriority priority)
        {
            Id = id;
            IdProject = idProject;
            IdUser = idUser;
            Title = title;
            Description = description;
            DueDate = dueDate;
            Status = Enum.TaskStatus.Pending;
            Priority = priority;
        }
    }
}
