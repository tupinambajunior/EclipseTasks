using EclipseTasks.Application.Mappers;
using EclipseTasks.Application.UseCases.Projects.Commands.CreateProject;
using EclipseTasks.Application.UseCases.Projects.Queries.ListProjects;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace EclipseTasks.Application.UnitTests.Projects
{
    public class ListProjectsUnitTest
    {
        private ServiceCollection sc;

        public ListProjectsUnitTest()
        {
            sc = new ServiceCollection();
            var domainAssembly = typeof(CreateProjectCommand).Assembly;

            sc.AddMappings();
            sc.AddMediatR(c => c.RegisterServicesFromAssembly(domainAssembly));
            sc.AddFluentValidation(new[] { domainAssembly });
        }

        [Fact]
        public async System.Threading.Tasks.Task ListAllProjectWithSuccessfully()
        {
            var dtos = new ProjectNameDTO[]
            {
                new ProjectNameDTO{ Name = "Project 1" },
                new ProjectNameDTO{ Name = "Project 2" },
                new ProjectNameDTO{ Name = "Project 3" },
            } as IEnumerable<ProjectNameDTO>;

            Mock<IProjectRepository> repoMock = new(MockBehavior.Strict);
            repoMock
                .Setup(s => s.ListAsync())
                .Returns(System.Threading.Tasks.Task.FromResult(dtos));

            sc.AddTransient(impl => repoMock.Object);

            var services = sc.BuildServiceProvider();
            var mediator = services.GetService<IMediator>()!;

            var query = new ListProjectsQuery();

            var projects = await mediator.Send(query);

            Assert.Equal(dtos.Count(), projects.Count());
        }

        [Fact]
        public async System.Threading.Tasks.Task ListZeroProjects()
        {
            var dtos = new ProjectNameDTO[0] as IEnumerable<ProjectNameDTO>;

            Mock<IProjectRepository> repoMock = new(MockBehavior.Strict);
            repoMock
                .Setup(s => s.ListAsync())
                .Returns(System.Threading.Tasks.Task.FromResult(dtos));

            sc.AddTransient(impl => repoMock.Object);

            var services = sc.BuildServiceProvider();
            var mediator = services.GetService<IMediator>()!;

            var query = new ListProjectsQuery();

            var projects = await mediator.Send(query);

            Assert.Equal(dtos.Count(), projects.Count());
        }
    }
}