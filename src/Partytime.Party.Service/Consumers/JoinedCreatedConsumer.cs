using MassTransit;
using Partytime.Joined.Contracts;
using Partytime.Party.Contracts;

namespace Partytime.Party.Consumers
{
    public class JoinedCreatedConsumer : IConsumer<JoinedCreated>
    {
        private readonly IPublishEndpoint publishEndpoint;
        
        public JoinedCreatedConsumer(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<JoinedCreated> context)
        {
            // Recieved message when joined party is created
            var message = context.Message;
        }
    }

}