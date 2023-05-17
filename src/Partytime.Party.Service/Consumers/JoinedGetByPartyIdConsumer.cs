using MassTransit;
using MassTransit.Transports;
using Partytime.Joined.Contracts;
using Partytime.Party.Contracts;

namespace Partytime.Party.Consumers
{
    public class JoinedGetByPartyIdConsumer : IConsumer<JoinedGetByPartyId>
    {
        private readonly IPublishEndpoint publishEndpoint;
        
        public JoinedGetByPartyIdConsumer(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<JoinedGetByPartyId> context)
        {
            // 3. Recieved message when joined party is created
            var message = context.Message;

            // 4. Respond hard coded reply
            await context.RespondAsync(new CommandMessage("Harcoded reply"));
        }
    }

}