using MassTransit;
using Partytime.Party.Contracts;

namespace Partytime.Party.Consumers
{
    public class PartyGetById : IConsumer<PartyGetById>
    {
        private readonly IPublishEndpoint publishEndpoint;
        
        public PartyGetById(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<PartyGetById> context)
        {
            // Recieved message when joined party is created
            var message = context.Message;
        }
    }

}