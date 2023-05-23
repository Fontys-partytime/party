using MassTransit;
using Partytime.Joined.Contracts;
using Partytime.Party.Contracts;
using Partytime.Party.Service.Repositories;

namespace Partytime.Party.Consumers
{
    public class JoinedCreatedConsumer : IConsumer<JoinedCreated>
    {
        private readonly IPartyRepository partyRepository;
        
        public JoinedCreatedConsumer()
        {
            
        }

        public async Task Consume(ConsumeContext<JoinedCreated> context)
        {
            // Recieved message when joined party is created
            var message = context.Message;
        }
    }

}