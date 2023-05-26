using MassTransit;
using Partytime.Joined.Contracts;
using Partytime.Party.Contracts;
using Partytime.Party.Service.Repositories;

namespace Partytime.Party.Consumers
{
    public class JoinedUpdatedConsumer : IConsumer<JoinedCreated>
    {
        private readonly IPartyRepository partyRepository;
        
        public JoinedUpdatedConsumer()
        {
            
        }

        public async Task Consume(ConsumeContext<JoinedCreated> context)
        {
            // Recieved message when joined party is created
            // Save the userid and username so that we don't have to search for that back in the joined service
            var message = context.Message;
        }
    }

}