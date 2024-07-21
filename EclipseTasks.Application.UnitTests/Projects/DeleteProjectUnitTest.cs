using EclipseTasks.Application.Repositories;
using EclipseTasks.Application.UseCases.Projects.Commands.DeleteProject;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace EclipseTasks.Application.UnitTests.Projects
{
    public class DeleteProjectUnitTest
    {
        private ServiceCollection sc;

        public DeleteProjectUnitTest()
        {
            sc = new ServiceCollection();
            var domainAssembly = typeof(DeleteProjectCommand).Assembly;

            sc.AddMediatR(c => c.RegisterServicesFromAssembly(domainAssembly));
            sc.AddFluentValidation(new[] { domainAssembly });
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteProjectWithSuccessfully()
        {
            var project = new Project { Id = Guid.NewGuid() };

            Mock<IUnitOfWork> uowMock = GetUnitOfWork();
            Mock<IProjectRepository> repoMock = GetProjectRepository(project);
            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(includePendingTask: false);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var services = sc.BuildServiceProvider();
            var mediator = services.GetService<IMediator>()!;

            var command = new DeleteProjectCommand(project.Id);

            await mediator.Send(command);

            repoMock.Verify(s => s.Delete(It.IsAny<Project>()), Times.Once);
            uowMock.Verify(s => s.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowValidationProjectWithPendingTasks()
        {
            var project = new Project { Id = Guid.NewGuid() };

            Mock<IUnitOfWork> uowMock = GetUnitOfWork();
            Mock<IProjectRepository> repoMock = GetProjectRepository(project);
            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(includePendingTask: true);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var services = sc.BuildServiceProvider();
            var mediator = services.GetService<IMediator>()!;

            var command = new DeleteProjectCommand(project.Id);

            await Assert.ThrowsAsync<ValidationException>(async () => await mediator.Send(command));
        }

        private static Mock<ITaskRepository> GetTaskRepository(bool includePendingTask)
        {
            var tasks = new List<Core.Entities.Task> {
                new Core.Entities.Task { Id = Guid.NewGuid(), Status = Core.Enums.StatusTask.Done },
            };

            if (includePendingTask)
                tasks.Add(new Core.Entities.Task { Id = Guid.NewGuid(), Status = Core.Enums.StatusTask.Todo });

            Mock<ITaskRepository> repoTaskMock = new(MockBehavior.Strict);
            repoTaskMock
                .Setup(s => s.ListAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult(tasks as IEnumerable<Core.Entities.Task>));

            return repoTaskMock;
        }

        private static Mock<IProjectRepository> GetProjectRepository(Project project)
        {
            Mock<IProjectRepository> repoMock = new(MockBehavior.Strict);

            repoMock
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult((Project)null));

            repoMock
                .Setup(s => s.GetByIdAsync(project.Id))
                .Returns(System.Threading.Tasks.Task.FromResult(project));

            repoMock
                .Setup(s => s.Delete(It.IsAny<Project>()))
                .Verifiable();

            return repoMock;
        }

        private static Mock<IUnitOfWork> GetUnitOfWork()
        {
            Mock<IUnitOfWork> uowMock = new(MockBehavior.Strict);
            uowMock
                .Setup(s => s.SaveChangesAsync())
                .Returns(System.Threading.Tasks.Task.CompletedTask);
            return uowMock;
        }
    }
}