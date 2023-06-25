using FakeItEasy;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Partytime.Party.Service.Controllers;
using Partytime.Party.Service.Dtos;
using Partytime.Party.Service.Repositories;

namespace Partytime.Party.tests
{
    public class UnitTest1
    {
        [Fact]
        public async void GetJoinedByPartyId_ReturnsCorrectJoined_ByPartyId()
        {
            // Arrange
            Service.Entities.Party fakeParty = A.Dummy<Service.Entities.Party>();

            var dataStore = A.Fake<IPartyRepository>();
            var publishEndPoint = A.Fake<IPublishEndpoint>();

            A.CallTo(() => dataStore.GetPartyById(fakeParty.Id)).Returns(fakeParty);
            var controller = new PartyController(dataStore);

            // Act
            var actionResult = await controller.GetByIdAsync(fakeParty.Id);

            // Assert
            var result = actionResult as OkObjectResult;
            var returnParty = result.Value as Service.Entities.Party;
            Assert.Equal(fakeParty, returnParty);
        }

        [Fact]
        public async void Updated_UpdatesJoinedCorrectly()
        {
            // Arrange
            Service.Entities.Party fakeParty = A.Dummy<Service.Entities.Party>();

            var dataStore = A.Fake<IPartyRepository>();
            var publishEndPoint = A.Fake<IPublishEndpoint>();

            CreatePartyDto createdPartyFromDto = new CreatePartyDto(fakeParty.Userid, fakeParty.Title, fakeParty.Description, fakeParty.Starts, fakeParty.Ends, fakeParty.Amount, fakeParty.Paymentlink, fakeParty.Linkexperation);
            var result2 = A.CallTo(() => dataStore.CreateParty(fakeParty)).Returns(fakeParty);

            UpdatePartyDto updateParty = new UpdatePartyDto(fakeParty.Title, fakeParty.Description, new DateTimeOffset(), new DateTimeOffset().AddDays(1), 678, "test.com", new DateTimeOffset());

            var test = Task.FromResult(result2).Result;

            A.CallTo(() => dataStore.UpdateParty(new Guid(), fakeParty)).Returns(fakeParty);
            var controller = new PartyController(dataStore);

            // Act
            var actionResult = await controller.Put(new Guid(), updateParty);

            // Assert
            var result = actionResult.Result as OkObjectResult;

            var returnParty = result.Value as Service.Entities.Party;
            Assert.NotEqual(fakeParty, returnParty);
        }

       
    }
}