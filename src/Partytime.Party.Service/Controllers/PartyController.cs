using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Partytime.Party.Contracts;
using Partytime.Party.Service.Clients;
using Partytime.Party.Service.Dtos;

namespace Partytime.Party.Service.Controllers
{
    [ApiController]
    [Route("parties")]
    public class PartyController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;
        private readonly JoinedClient joinedClient;

        // These Guid's are purely for testing purposes between services and to show the teacher that the
        // walking skeleton works
        private static Guid defaultPartyGuid = Guid.NewGuid();
        private static Guid defaultUserGuid = Guid.NewGuid();
        
        public PartyController(IPublishEndpoint publishEndpoint, JoinedClient joinedClient)
        {
            this.publishEndpoint = publishEndpoint;
            this.joinedClient = joinedClient;
        }

        private static readonly List<PartyDto> parties = new()
        {
            new PartyDto(defaultPartyGuid, defaultUserGuid, "Party 1", "Description of party 1", DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, "ExampleStreet 1", 123456),
            new PartyDto(Guid.NewGuid(), Guid.NewGuid(), "Party 2", "Description of party 2", DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, "ExampleStreet 3", 123456),
            new PartyDto(Guid.NewGuid(), Guid.NewGuid(), "Party 3", "Description of party 3", DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, "ExampleStreet 5", 123456)
        };

        [HttpGet]
        public async Task<IEnumerable<PartyDto>> GetAsync()
        {
            return parties;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartyDto>> GetByIdAsync(Guid id)
        {
            // Used for demo purposes walking skeleton
            var hardcodedReply = new CommandMessage("Hardcoded reply");
            var party = parties.Where(party => party.Id == id).SingleOrDefault();
            
            if(party == null)
            {
                return NotFound();
            }

            // Internal communication between microservices Party & Joined
            var joinedParty = await joinedClient.GetPartyJoinedByPartyAsync(id);

            // Need extensions to 'extend' the normal Party entity together with joined
            if(joinedParty != null)
            {
                party = party.AsDto(joinedParty); // Add joined to party when found
            }

            // For now limited to these variables to make the base for my walking skeleton
            // Guid id, string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location
            await publishEndpoint.Publish(new PartyGetById(party.Id, party.Title, party.Description, party.Starts, party.Ends, party.Location));
            
            // Send hardcoded reply on consuming of the message
            await publishEndpoint.Publish(hardcodedReply);
            
            return Ok(hardcodedReply);
            //return Ok(party);
        }

        [HttpPost]
        public ActionResult<PartyDto> Post(CreatePartyDto createPartyDto)
        {
            // Need to add a function in Program.cs that automatically converts to ISODateTime for starts and ends
            // https://stackoverflow.com/questions/68539924/c-wrong-datetime-format-passed-to-front-end
            var party = new PartyDto(Guid.NewGuid(), createPartyDto.UserId, createPartyDto.Title, createPartyDto.Description, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, createPartyDto.Location, createPartyDto.Budget); 
            parties.Add(party);

            // Returns the GetById link of the created party
            return CreatedAtAction(nameof(GetByIdAsync), new {id = party.Id}, party);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, UpdatePartyDto updatePartyDto)
        {
            var existingParty = parties.Where(party => party.Id == id).SingleOrDefault();
            
            if(existingParty == null)
            {
                return NotFound();
            }

            var updatedParty = existingParty with {
                Title = updatePartyDto.Title,
                Description = updatePartyDto.Description,
                Starts = DateTimeOffset.UtcNow,
                Ends = DateTimeOffset.UtcNow,
                Location = updatePartyDto.Location,
                Budget = updatePartyDto.Budget
            };

            var index = parties.FindIndex(existingParty => existingParty.Id == id);
            parties[index] = updatedParty;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var index = parties.FindIndex(existingParty => existingParty.Id == id);
            
            if(index < 0)
            {
                return NotFound();
            }
            
            parties.RemoveAt(index);

            return NoContent();
        }

    }
}