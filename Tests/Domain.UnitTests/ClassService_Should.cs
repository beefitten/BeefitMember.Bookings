using System;
using System.Net;
using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Domain.Services;
using Domain.Services.Class;
using Moq;
using Persistence.Repositories.Classes;
using Persistence.Repositories.Classes.Models;
using Persistence.Repositories.FireStore;
using Tests.Helpers;
using Xunit;

namespace Tests.Domain.UnitTests
{
    public class ClassService_Should
    {
        [Theory, AutoMoqData]
        public async Task Call_AddClass_And_Invoke_Repositories_When_Adding_A_Class_Successfully(
            [Frozen] Mock<IClassesRepository> classesRepositoryMock,
            [Frozen] Mock<IFireStore> fireStoreRepositoryMock,
            [Frozen] Mock<IMessageBus> messageBusMock,
            ClassModel model)
        {
            var sut = new ClassService(messageBusMock.Object,
                classesRepositoryMock.Object,
                fireStoreRepositoryMock.Object);

            var response = await sut.AddClass(model);
            
            Assert.Equal(HttpStatusCode.OK, response);
            
            classesRepositoryMock
                .Verify(x => x.AddClass(
                        It.Is<ClassModel>(y => 
                            y.Location == model.Location &&
                            y.ClassImage == model.ClassImage &&
                            y.ClassName == model.ClassName),
                        It.IsAny<Guid>()),
                    Times.Once);
            
            fireStoreRepositoryMock
                .Verify(x => x.AddClass(
                        It.IsAny<Guid>()),
                    Times.Once);
        }
        
        [Theory, AutoMoqData]
        public async Task Call_AddClass_And_Throw_Exception_When_Classes_Cannot_Be_Added(
            [Frozen] Mock<IClassesRepository> classesRepositoryMock,
            [Frozen] Mock<IFireStore> fireStoreRepositoryMock,
            [Frozen] Mock<IMessageBus> messageBusMock,
            ClassModel model)
        {
            classesRepositoryMock
                .Setup(x => x.AddClass(It.IsAny<ClassModel>(), It.IsAny<Guid>()))
                .ThrowsAsync(new Exception("Some message here"));

            var sut = new ClassService(messageBusMock.Object,
                classesRepositoryMock.Object,
                fireStoreRepositoryMock.Object);

            var expectedException = "Something went wrong adding class: " + model.ClassName;

            var exception = await Assert.ThrowsAsync<Exception>(() => sut.AddClass(model));
            
            Assert.Equal(expectedException, exception.Message); 
        }
    }
}