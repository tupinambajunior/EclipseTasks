namespace EclipseTasks.Core.Entities
{
    public class CommentTask
    {
        public Guid Id { get; set; }
        public Task Task { get; set; }
        public User Actor { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}