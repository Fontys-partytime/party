using MassTransit;
using Partytime.Joined.Contracts;
using Partytime.Party.Contracts;
using Partytime.Party.Service.Repositories;

namespace Partytime.Party.Consumers
{
    public class JoinedDeletedConsumer : IConsumer<JoinedDeleted>
    {
        private readonly IPartyRepository partyRepository;
        
        public JoinedDeletedConsumer()
        {
            
        }

        public async Task Consume(ConsumeContext<JoinedDeleted> context)
        {
            // Recieved message when joined party is created
            var message = context.Message;
        }
    }

}