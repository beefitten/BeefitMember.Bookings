using System.Threading.Tasks;
using AutoFixture.Xunit2;
using Consumer.Service.Handlers;
using Domain.Events;
using Xunit;
using Moq;
using Persistence.Repositories.Classes;
using Persistence.Repositories.Classes.Models;
using Persistence.Repositories.FireStore;

namespace Tests.Consumer.Service.UnitTests
{
    public class BookingHandler_Should
    {
        [Theory, AutoData]
        public async Task Not_Invoke_Repositories_When_Not_Classes_Exist(
            [Frozen] Mock<IClassesRepository> classesRepositoryMock,
            [Frozen] Mock<IFireStore> fireStoreRepositoryMock,
            BookClassEvent bookClassEvent)
        {
            classesRepositoryMock
                .Setup(x => x.GetClassInformation(bookClassEvent.ClassId))
                .ReturnsAsync(() => null);

            var sut = new BookingsHandler(classesRepositoryMock.Object, fireStoreRepositoryMock.Object);
            
            await sut.HandleClassBooking(bookClassEvent);
            
            classesRepositoryMock
                .Verify(x => x.AddBookingOnClass(
                    It.IsAny<string>(),
                    It.IsAny<bool>(),
                    It.IsAny<int>(),
                    It.IsAny<string>()),
                    Times.Never);
            
            fireStoreRepositoryMock
                .Verify(x => x.AddUserToClass(
                        It.IsAny<string>(),
                        It.IsAny<string>()),
                    Times.Never);
        }
        
        [Theory, AutoData]
        public async Task Not_Invoke_Repositories_When_Class_Is_Full(
            [Frozen] Mock<IClassesRepository> classesRepositoryMock,
            [Frozen] Mock<IFireStore> fireStoreRepositoryMock,
            BookClassEvent bookClassEvent,
            ClassReturnModel propertyModel)
        {
            var model = new ClassReturnModel(
                bookClassEvent.ClassId,
                propertyModel.FitnessName,
                propertyModel.ClassName,
                propertyModel.ClassType,
                propertyModel.ClassImage,
                true,
                propertyModel.MaxParticipants,
                propertyModel.NumberOfParticipants,
                propertyModel.TimeStart,
                propertyModel.TimeEnd,
                propertyModel.Participants,
                propertyModel.Location);
            
            classesRepositoryMock
                .Setup(x => x.GetClassInformation(bookClassEvent.ClassId))
                .ReturnsAsync(model);

            var sut = new BookingsHandler(classesRepositoryMock.Object, fireStoreRepositoryMock.Object);
            
            await sut.HandleClassBooking(bookClassEvent);
            
            classesRepositoryMock
                .Verify(x => x.AddBookingOnClass(
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<int>(),
                        It.IsAny<string>()),
                    Times.Never);
            
            fireStoreRepositoryMock
                .Verify(x => x.AddUserToClass(
                        It.IsAny<string>(),
                        It.IsAny<string>()),
                    Times.Never);
        }
        
        [Theory, AutoData]
        public async Task Invoke_Repositories_When_Class_Is_Not_Full(
            [Frozen] Mock<IClassesRepository> classesRepositoryMock,
            [Frozen] Mock<IFireStore> fireStoreRepositoryMock,
            BookClassEvent bookClassEvent,
            ClassReturnModel propertyModel)
        {
            var model = new ClassReturnModel(
                bookClassEvent.ClassId,
                propertyModel.FitnessName,
                propertyModel.ClassName,
                propertyModel.ClassType,
                propertyModel.ClassImage,
                false,
                propertyModel.MaxParticipants,
                propertyModel.NumberOfParticipants,
                propertyModel.TimeStart,
                propertyModel.TimeEnd,
                propertyModel.Participants,
                propertyModel.Location);
            
            classesRepositoryMock
                .Setup(x => x.GetClassInformation(bookClassEvent.ClassId))
                .ReturnsAsync(model);

            var sut = new BookingsHandler(classesRepositoryMock.Object, fireStoreRepositoryMock.Object);
            
            await sut.HandleClassBooking(bookClassEvent);
            
            classesRepositoryMock
                .Verify(x => x.AddBookingOnClass(
                        It.IsAny<string>(),
                        It.IsAny<bool>(),
                        It.IsAny<int>(),
                        It.IsAny<string>()),
                    Times.Once);
            
            fireStoreRepositoryMock
                .Verify(x => x.AddUserToClass(
                        It.IsAny<string>(),
                        It.IsAny<string>()),
                    Times.Once);
        }
    }
}