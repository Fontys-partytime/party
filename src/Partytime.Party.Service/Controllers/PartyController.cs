using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Partytime.Party.Contracts;
using Partytime.Party.Service.Clients;
using Partytime.Party.Service.Dtos;
using Partytime.Party.Service.Entities;
using Partytime.Party.Service.Repositories;
using AutoMapper;
using static Dapper.SqlMapper;

namespace Partytime.Party.Service.Controllers
{
    [ApiController]
    [Route("parties")]
    public class PartyController : ControllerBase
    {
        private readonly IPartyRepository _partyRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        //private readonly JoinedClient joinedClient;

        public PartyController(IPartyRepository partyRepository, IPublishEndpoint publishEndpoint, IMapper mapper//, JoinedClient joinedClient
        )
        {
            this._partyRepository = partyRepository ?? throw new ArgumentNullException(nameof(partyRepository));
            this._publishEndpoint = publishEndpoint;
            this._mapper = mapper;
            //this.joinedClient = joinedClient;
        }

        [HttpGet]
        public async Task<ActionResult<List<PartyDto>>> GetAsync()
        {
            var parties = await _partyRepository.GetParties();

            if (parties == null)
                return NotFound();

            return (Ok(parties));

            return Ok(_mapper.Map<List<PartyDto>>(parties));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PartyDto>> GetByIdAsync(Guid id)
        {
            var partyFound = await _partyRepository.GetPartyById(id);

            if (partyFound == null)
                return NotFound();

            // Internal communication between microservices Party & Joined
            //var joinedParty = await joinedClient.GetPartyJoinedByPartyAsync(id);

            // Need extensions to 'extend' the normal Party entity together with joined
            //if(joinedParty != null)
            //{
            //    party = party.AsDto(joinedParty); // Add joined to party when found
            //}

            // For now limited to these variables to make the base for my walking skeleton
            // Guid id, string Title, string Description, DateTimeOffset Starts, DateTimeOffset Ends, string Location
            await _publishEndpoint.Publish(new PartyGetById(partyFound.Id, partyFound.Title, partyFound.Description, partyFound.Starts, partyFound.Ends, partyFound.Location));
            
            //return Ok(hardcodedReply);
            return Ok(_mapper.Map<PartyDto>(partyFound));
        }

        [HttpPost]
        public async Task<ActionResult<PartyDto>> Post([FromBody] CreatePartyDto createPartyDto)
        {
            var party = new Entities.Party
            {
                UserId = createPartyDto.UserId,
                Title = createPartyDto.Title,
                Description = createPartyDto.Description,
                Starts = createPartyDto.Starts,
                Ends = createPartyDto.Ends,
                Location = createPartyDto.Location,
                Budget = createPartyDto.Budget,
            };

            Entities.Party createdParty = await _partyRepository.CreateParty(party);

            // Returns the GetById link of the created party
            return CreatedAtAction(nameof(GetByIdAsync), new {id = createdParty.Id}, createdParty);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdatePartyDto updatePartyDto)
        {
            Entities.Party partyFound = await _partyRepository.GetPartyById(id);

            if (partyFound == null)
                return NotFound();

            return Ok(await _partyRepository.UpdateParty(_mapper.Map<Entities.Party>(updatePartyDto)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Delete(Guid id)
        {
            Entities.Party partyFound = await _partyRepository.GetPartyById(id);

            if (partyFound == null)
                return NotFound();
            
            return Ok(await _partyRepository.DeleteParty(id));
        }

    }
}