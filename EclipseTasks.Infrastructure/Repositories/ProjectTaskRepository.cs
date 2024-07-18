using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Infrastructure.Repositories
{
    public class ProjectTaskRepository : Repository<ProjectTask>, IProjectTaskRepository
    {
        private readonly PgContext _dbContext;
        public ProjectTaskRepository(PgContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}