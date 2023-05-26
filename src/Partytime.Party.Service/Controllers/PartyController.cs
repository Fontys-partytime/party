using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Partytime.Party.Contracts;
using Partytime.Party.Service.Clients;
using Partytime.Party.Service.Dtos;
using Partytime.Party.Service.Entities;
using Partytime.Party.Service.Repositories;
using AutoMapper;

namespace Partytime.Party.Service.Controllers
{
    [ApiController]
    [Route("parties")]
    public class PartyController : ControllerBase
    {
        private readonly IPartyRepository _partyRepository;
        private readonly IPublishEndpoint _publishEndpoint;

        //private readonly JoinedClient joinedClient;

        public PartyController(IPartyRepository partyRepository, IPublishEndpoint publishEndpoint//, JoinedClient joinedClient
        )
        {
            this._partyRepository = partyRepository ?? throw new ArgumentNullException(nameof(partyRepository));
            this._publishEndpoint = publishEndpoint;
            //this.joinedClient = joinedClient;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPartiesByUserId(Guid userId)
        {
            var partyFound = await _partyRepository.GetPartiesByUserId(userId);

            if (partyFound == null)
                return NotFound();

            return Ok(partyFound);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var partyFound = await _partyRepository.GetPartyById(id);

            if (partyFound == null)
                return NotFound();

            return Ok(partyFound);
        }

        [HttpPost]
        public async Task<ActionResult<PartyDto>> Post([FromBody] CreatePartyDto createPartyDto)
        {
            var party = new Entities.Party
            {
                Userid = createPartyDto.Userid,
                Title = createPartyDto.Title,
                Description = createPartyDto.Description,
                Starts = createPartyDto.Starts,
                Ends = createPartyDto.Ends,
                Location = createPartyDto.Location,
                Budget = createPartyDto.Budget,
            };

            Entities.Party createdParty = await _partyRepository.CreateParty(party);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdatePartyDto updatePartyDto)
        {
            var party = new Entities.Party
            {
                Userid = updatePartyDto.Userid,
                Title = updatePartyDto.Title,
                Description = updatePartyDto.Description,
                Starts = updatePartyDto.Starts,
                Ends = updatePartyDto.Ends,
                Location = updatePartyDto.Location,
                Budget = updatePartyDto.Budget,
            };

            var updatedParty = await _partyRepository.UpdateParty(id, party);

            if(updatedParty == null)
                return NotFound();

            return Ok(updatedParty);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            var partyFound = await _partyRepository.GetPartyById(id);

            if (partyFound == null)
                return NotFound();
            
            bool partyDeleted = await _partyRepository.DeleteParty(id);
            return Ok(partyDeleted);
        }

    }
}