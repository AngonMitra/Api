using CardsApi.Data;
using CardsApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardsApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : Controller
    {
        private readonly CardsDbContext context;

        public CardsController(CardsDbContext context)
        {
            this.context = context;
        }

        //Get All Cards
        [HttpGet]
        public async Task<IActionResult> GetAllCards()
        {
           var cards= await context.Cards.ToListAsync();
            return Ok(cards);
        }

        //Get Single Card
        [HttpGet]
        [Route("{id:int}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] int id)
        {
            var card = await context.Cards.FirstOrDefaultAsync(x=> x.Id==id);

            if(card != null)
            {
                return Ok(card);
            }
            return NotFound("Card Not Found");
        }

        //Add Card
        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] Card card)
        {
             
            await context.Cards.AddAsync(card);
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new { id=card.Id}, card);
        }

        //Update Card
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateCard([FromRoute] int id, [FromBody] Card card)
        {
            var existingcard = await context.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if(existingcard != null)
            {
                existingcard.CardholderName = card.CardholderName;
                existingcard.CardNumber = card.CardNumber;
                existingcard.ExpiryMonth = card.ExpiryMonth;
                existingcard.ExpiryYear = card.ExpiryYear;
                existingcard.CVC = card.CVC;
                await context.SaveChangesAsync();

                return Ok(existingcard);

            }
            return NotFound("Card Not Found");
        }


        //Delete Card
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCard([FromRoute] int id)
        {
            var existingcard = await context.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingcard != null)
            {
                context.Remove(existingcard);
                await context.SaveChangesAsync();

                return Ok(existingcard);

            }
            return NotFound("Card Not Found");
        }


    }
}
