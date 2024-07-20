using EclipseTasks.Core.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Application.UseCases.Reports.Productivity
{
    public class ReportProductivityQueryHandler : IRequestHandler<ReportProductivityQuery, ReportProductivityDTO>
    {
        private readonly ITaskRepository _taskRepository;

        public ReportProductivityQueryHandler(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<ReportProductivityDTO> Handle(ReportProductivityQuery request, CancellationToken cancellationToken)
        {
            var today = DateTime.Now;
            var daysAgo =  today.AddDays(request.QuantityLastDays * -1);

            var tasks = await _taskRepository.ListAsync(daysAgo, today);

            var tasksGroupedByUser = tasks
                .Where(t => t.Status == Core.Enums.StatusTask.Done)
                .GroupBy(t => t.Actor)
                .Select(g => new ReportProductivityDTO.DataDTO
                {
                    UserEmail = g.Key.Email,
                    Average = g.Count() // / request.QuantityLastDays
                }).ToArray();

            return new ReportProductivityDTO
            {
                Data = tasksGroupedByUser
            };
        }
    }
}
