using MassTransit;
using Partytime.Joined.Contracts;
using Partytime.Party.Contracts;
using Partytime.Party.Service.Repositories;
using Entities = Partytime.Party.Service.Entities;

namespace Partytime.Party.Consumers
{
    public class JoinedCreatedConsumer : IConsumer<JoinedCreated>
    {
        private readonly IPartyRepository _partyRepository;
        
        public JoinedCreatedConsumer(IPartyRepository partyRepository)
        {
            this._partyRepository = partyRepository;
        }

        public async Task Consume(ConsumeContext<JoinedCreated> context)
        {
            // Recieved message when joined party is created
            // Save the userid and username so that we don't have to search for that back in the joined service
            var message = context.Message;

            // Proves walking skeleton
            var party = await _partyRepository.CheckIfJoinedExistsInParty(message.partyId, message.UserId);
            
            if(party != null) // If joined is already in
            {
                return;
            }

            Entities.Joined joined = new Entities.Joined
            {
                Partyid = message.partyId,
                Userid = message.UserId,
                Username = message.Username,
                Accepted = null // Now the party person can look for requests
            };
            
            await _partyRepository.AddJoinedToParty(message.partyId, joined); // Final step of walking skeleton
        }
    }

}