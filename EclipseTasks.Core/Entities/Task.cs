using EclipseTasks.Core.Enums;

namespace EclipseTasks.Core.Entities
{
    public class Task : ICloneable
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Project Project { get; set; }
        public DateTime Deadline { get; set; }
        public StatusTask Status { get; set; }
        public PriorityTask Priority { get; set; }
        public User Actor { get; set; }
        public DateTime CreatedAt { get; set; }


        public Task()
        {
            this.Status = StatusTask.Todo;
            this.Priority = PriorityTask.Low;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}