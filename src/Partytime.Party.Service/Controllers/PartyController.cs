using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Partytime.Party.Contracts;
using Partytime.Party.Service.Dtos;

namespace Partytime.Party.Service.Controllers
{
    [ApiController]
    [Route("parties")]
    public class PartyController : ControllerBase
    {
        private readonly IPublishEndpoint publishEndpoint;
        
        public PartyController(IPublishEndpoint publishEndpoint)
        {
            this.publishEndpoint = publishEndpoint;
        }

        private static readonly List<PartyDto> parties = new()
        {
            new PartyDto(Guid.NewGuid(), "userIdString", "Party 1", "Description of party 1", DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, "ExampleStreet 1", 123456 , DateTimeOffset.UtcNow),
            new PartyDto(Guid.NewGuid(), "userIdString", "Party 2", "Description of party 2", DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, "ExampleStreet 3", 123456 , DateTimeOffset.UtcNow),
            new PartyDto(Guid.NewGuid(), "userIdString", "Party 3", "Description of party 3", DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, "ExampleStreet 5", 123456 , DateTimeOffset.UtcNow)
        };

        [HttpGet]
        public async Task<IEnumerable<PartyDto>> GetAsync()
        {
            return parties;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartyDto>> GetByIdAsync(Guid id)
        {
            var party = parties.Where(party => party.Id == id).SingleOrDefault();
            
            if(party == null)
            {
                return NotFound();
            }

            // For now limited to these variables to make the base for my walking skeleton
            // Guid id, string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location
            await publishEndpoint.Publish(new PartyGetById(party.Id, party.Title, party.Description, party.Starts, party.Ends, party.Location));
            
            return party;
        }

        [HttpPost]
        public ActionResult<PartyDto> Post(CreatePartyDto createPartyDto)
        {
            // Need to add a function in Program.cs that automatically converts to ISODateTime for starts and ends
            // https://stackoverflow.com/questions/68539924/c-wrong-datetime-format-passed-to-front-end
            var party = new PartyDto(Guid.NewGuid(), createPartyDto.UserId, createPartyDto.Title, createPartyDto.Description, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow, createPartyDto.Location, createPartyDto.Budget, DateTimeOffset.UtcNow); 
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