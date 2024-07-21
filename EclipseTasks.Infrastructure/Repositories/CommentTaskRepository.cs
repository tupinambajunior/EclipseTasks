using AutoMapper;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using EclipseTasks.Infrastructure.Persistence;

namespace EclipseTasks.Infrastructure.Repositories
{
    public class CommentTaskRepository : GenericRepository<CommentTask>, ICommentTaskRepository
    {
        public CommentTaskRepository(AppDbContext context) : base(context) { }
    }
}