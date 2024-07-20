using EclipseTasks.Core.Entities;

namespace EclipseTasks.Core.Repository
{
    public interface ICommentTaskRepository
    {
        public void Create(CommentTask comment);
    }
}