using EclipseTasks.Application.Exceptions;
using EclipseTasks.Application.Repositories;
using EclipseTasks.Application.UseCases.Tasks.Commands.DeleteTask;
using EclipseTasks.Application.UseCases.Tasks.Commands.UpdateTask;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace EclipseTasks.Application.UnitTests.Projects
{
    public class DeleteTaskUnitTest
    {
        private ServiceCollection sc;

        public DeleteTaskUnitTest()
        {
            sc = new ServiceCollection();
            var domainAssembly = typeof(UpdateTaskCommand).Assembly;

            sc.AddMediatR(c => c.RegisterServicesFromAssembly(domainAssembly));
            sc.AddFluentValidation(new[] { domainAssembly });


            var histories = new HistoryTask[] { new HistoryTask() } as IEnumerable<HistoryTask>;
            Mock<IHistoryTaskRepository> histMock = new(MockBehavior.Strict);
            histMock
                .Setup(t => t.ListAsync(It.IsAny<Core.Entities.Task>()))
                .Returns(System.Threading.Tasks.Task.FromResult(histories));

            histMock
                .Setup(t => t.Delete(It.IsAny<HistoryTask>()))
                .Verifiable();

            sc.AddTransient(impl => histMock.Object);
        }

        [Fact]
        public async System.Threading.Tasks.Task DeleteTaskWithSuccessfully()
        {
            Core.Entities.Task task = GetTaskToDelete();

            var command = new DeleteTaskCommand(task.Id);

            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await _mediator.Send(command);

            repoTaskMock.Verify(s => s.Delete(It.IsAny<Core.Entities.Task>()), Times.Once);
            uowMock.Verify(s => s.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowValidationExceptionWithoutId()
        {
            Core.Entities.Task task = GetTaskToDelete();

            var command = new DeleteTaskCommand(Guid.Empty);

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
            Core.Entities.Task task = GetTaskToDelete();

            var command = new DeleteTaskCommand(Guid.NewGuid());

            Mock<IUnitOfWork> uowMock = GetUnitOfWork();

            Mock<ITaskRepository> repoTaskMock = GetTaskRepository(task);

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoTaskMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            await Assert.ThrowsAsync<NotFoundException>(async () => await _mediator.Send(command));
        }

        private Core.Entities.Task GetTaskToDelete()
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
                .Setup(s => s.Delete(It.IsAny<Core.Entities.Task>()))
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