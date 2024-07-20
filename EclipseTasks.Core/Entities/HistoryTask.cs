namespace EclipseTasks.Core.Entities
{
    public class HistoryTask
    {
        public Guid Id { get; set; }
        public User Actor { get; set; }
        public DateTime GeneratedAt { get; set; }
        public string Content { get; set; }
        public Task Task { get; set; }
    }
}