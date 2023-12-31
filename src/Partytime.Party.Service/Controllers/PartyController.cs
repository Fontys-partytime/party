using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Partytime.Party.Contracts;
using Partytime.Party.Service.Dtos;
using Partytime.Party.Service.Entities;
using Partytime.Party.Service.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Partytime.Party.Service.Controllers
{
    [ApiController]
    [Route("parties")]
    public class PartyController : ControllerBase
    {
        private readonly IPartyRepository _partyRepository;

        public PartyController(IPartyRepository partyRepository)
        {
            this._partyRepository = partyRepository ?? throw new ArgumentNullException(nameof(partyRepository));
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPartiesByUserId(Guid userId)
        {
            var partyFound = await _partyRepository.GetPartiesByUserId(userId);

            if (partyFound == null)
                return NotFound();

            return Ok(partyFound);
        }

        [HttpGet("testvoorbeeld")]
        public async Task<IActionResult> Test()
        {
            return Ok("Je kan bij de service");
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
                Amount = createPartyDto.Amount,
                Paymentlink = createPartyDto.Paymentlink,
                Linkexperation = createPartyDto.Linkexperation
            };

            Entities.Party createdParty = await _partyRepository.CreateParty(party);

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> Put(Guid id, [FromBody] UpdatePartyDto updatePartyDto)
        {
            var party = new Entities.Party
            {
                Title = updatePartyDto.title,
                Description = updatePartyDto.description,
                Starts = updatePartyDto.starts,
                Ends = updatePartyDto.ends,
                Amount = updatePartyDto.amount,
                Paymentlink = updatePartyDto.paymentlink,
                Linkexperation = updatePartyDto.linkexperation
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