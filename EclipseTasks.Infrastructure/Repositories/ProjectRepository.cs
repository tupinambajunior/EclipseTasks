using AutoMapper;
using AutoMapper.QueryableExtensions;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using EclipseTasks.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EclipseTasks.Infrastructure.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProjectRepository(AppDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public new async Task<IEnumerable<ProjectNameDTO>> ListAsync()
        {
            return await _context.Projects
                .ProjectTo<ProjectNameDTO>(_mapper.ConfigurationProvider)
                .ToArrayAsync();
        }
    }
}