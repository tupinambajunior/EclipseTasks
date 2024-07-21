using EclipseTasks.Application.Repositories;
using EclipseTasks.Application.UseCases.Projects.Commands.CreateProject;
using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repository;
using FluentValidation;
using MediatR;
using MediatR.Extensions.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace EclipseTasks.Application.UnitTests.Projects
{
    public class CreateProjectUnitTest
    {
        private ServiceCollection sc;

        public CreateProjectUnitTest()
        {
            sc = new ServiceCollection();
            var domainAssembly = typeof(CreateProjectCommand).Assembly;

            sc.AddMediatR(c => c.RegisterServicesFromAssembly(domainAssembly));
            sc.AddFluentValidation(new[] { domainAssembly });
        }

        [Fact]
        public async System.Threading.Tasks.Task CreateProjectWithSuccessfully()
        {
            Mock<IUnitOfWork> uowMock = new(MockBehavior.Strict);
            uowMock
                .Setup(s => s.SaveChangesAsync())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            Mock<IProjectRepository> repoMock = new(MockBehavior.Strict);
            repoMock
                .Setup(s => s.Create(It.IsAny<Project>()))
                .Verifiable();

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            var command = new CreateProjectCommand { Name = "mock" };

            await _mediator.Send(command);

            repoMock.Verify(s => s.Create(It.IsAny<Project>()), Times.Once);
            uowMock.Verify(s => s.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async System.Threading.Tasks.Task ThrowExceptionProjectWithoutName()
        {
            Mock<IUnitOfWork> uowMock = new(MockBehavior.Strict);
            uowMock
                .Setup(s => s.SaveChangesAsync())
                .Returns(System.Threading.Tasks.Task.CompletedTask);

            Mock<IProjectRepository> repoMock = new(MockBehavior.Strict);
            repoMock
                .Setup(s => s.Create(It.IsAny<Project>()))
                .Verifiable();

            sc.AddTransient(impl => uowMock.Object);
            sc.AddTransient(impl => repoMock.Object);

            var _services = sc.BuildServiceProvider();
            var _mediator = _services.GetService<IMediator>()!;

            var command = new CreateProjectCommand { Name = "" };

            await Assert.ThrowsAsync<ValidationException>(() => _mediator.Send(command));
        }
    }
}