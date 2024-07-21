using EclipseTasks.Application.Configuration;
using EclipseTasks.Application.Exceptions;
using EclipseTasks.Application.Repositories;
using EclipseTasks.Application.Security;
using EclipseTasks.Application.UseCases.Tasks.Commands.UpdateTask;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;

namespace EclipseTasks.Application.UnitTests.Projects
{
    public class UpdateTaskUnitTest
    {
        private ServiceCollection sc;

        public UpdateTaskUnitTest()
        {
            sc = new ServiceCollection();
            var domainAssembly = typeof(UpdateTaskCommand).Assembly;

            sc.AddMediatR(c => c.RegisterServicesFromAssembly(domainAssembly));
            sc.AddFluentValidation(new[] { domainAssembly });

            Mock<IOptions<AppOptions>> configMock = new(MockBehavior.Strict);
            configMock
                .SetupGet(s => s.Value)
                .Returns(new AppOptions { MaxTaskInProject = 3 });

            Mock<IHistoryTaskRepository> histMock = new(MockBehavior.Strict);
            histMock.Setup(t => t.Create(It.IsAny<HistoryTask>())).Verifiable();

            Mock<IAuthentication> authMock = new(MockBehavior.Strict);
            authMock.Setup(t => t.GetUser()).Returns(new User { Email = "teste@teste.com.br" });

            sc.AddTransient(impl => configMock.Object);
            sc.AddTransient(impl => histMock.Object);
            sc.AddTransient(impl => authMock.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task UpdateTaskWithSuccessfully()
        {
            Core.Entities.Task task = GetTaskToUpdate();

            var command = new UpdateTaskCommand
            {
                Id = task.Id,
                Title = "Test Updated",
                Description = "Test Updated",
                Deadline = DateTime.Now.AddDays(1),
                Status = Core.Enums.StatusTask.Done
            };
            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await _mediator.Send(command);

            repoTaskMock.Verify(s => s.Update(It.IsAny<Core.Entities.Task>()), Times.Once);
            uowMock.Verify(s => s.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowValidatinoExceptionWithoutId()
        {
            Core.Entities.Task task = GetTaskToUpdate();

            var command = new UpdateTaskCommand
            {
                Id = Guid.Empty,
                Title = "Test Updated",
                Description = "Test Updated",
                Deadline = DateTime.Now.AddDays(1),
                Status = Core.Enums.StatusTask.Done
            };
            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<ValidationException>(async () => await _mediator.Send(command));
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowValidatinoExceptionWithoutTitle()
        {
            Core.Entities.Task task = GetTaskToUpdate();

            var command = new UpdateTaskCommand
            {
                Id = task.Id,
                Title = "",
                Description = "Test Updated",
                Deadline = DateTime.Now.AddDays(1),
                Status = Core.Enums.StatusTask.Done
            };
            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<ValidationException>(async () => await _mediator.Send(command));
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowNotFoundTask()
        {
            Core.Entities.Task task = GetTaskToUpdate();

            var command = new UpdateTaskCommand
            {
                Id = Guid.NewGuid(),
                Title = "Ttitle 1",
                Description = "Test Updated",
                Deadline = DateTime.Now.AddDays(1),
                Status = Core.Enums.StatusTask.Done
            };
            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<NotFoundException>(async () => await _mediator.Send(command));
        }

        private Core.Entities.Task GetTaskToUpdate()
        {
            return new Core.Entities.Task
            {
                Id = Guid.NewGuid(),
                Title = "Title",
                Description = "Description",
                Deadline = DateTime.Now,
                Priority = Core.Enums.PriorityTask.Medium,
                Status = Core.Enums.StatusTask.Doing,
                Project = new Project()
            };
        }

        private Mock<ITaskRepository> GetTaskRepository(Core.Entities.Task task)
        {
            Mock<ITaskRepository> repoTaskMock = new(MockBehavior.Strict);

            repoTaskMock
                .Setup(s => s.GetByIdAsync(It.IsAny<Guid>()))
                .Returns(System.Threading.Tasks.Task.FromResult((Core.Entities.Task)null));

            repoTaskMock
                .Setup(s => s.GetByIdAsync(task.Id))
                .Returns(System.Threading.Tasks.Task.FromResult(task));

            repoTaskMock
                .Setup(s => s.Update(It.IsAny<Core.Entities.Task>()))
                .Verifiable();

            return repoTaskMock;
        }

        private Mock<IUnitOfWork> GetUnitOfWork()
        {
            Mock<IUnitOfWork> uowMock = new(MockBehavior.Strict);
            uowMock
                .Setup(s => s.SaveChangesAsync())
                .Returns(System.Threading.Tasks.Task.CompletedTask);
            return uowMock;
        }
    }
}