using MassTransit;
using MassTransit.Transports;
using Partytime.Joined.Contracts;
using Partytime.Party.Contracts;

namespace Partytime.Party.Consumers
{
    public class JoinedGetByPartyId : IConsumer<JoinedGetByPartyId>
    {
        private readonly IPublishEndpoint publishEndpoint;
        
        public JoinedGetByPartyId(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<JoinedGetByPartyId> context)
        {
            // Recieved message when joined party is created
            var message = context.Message;

            // Send reply to messaging container
            await publishEndpoint.Publish(new CommandMessage("Harcoded reply"));
        }
    }

}