using Microsoft.AspNetCore.Mvc;
using epos.Models;
using epos.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System;

namespace epos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IFoodItemRepository _foodItemRepo;

        public ItemController(IFoodItemRepository foodItemRepo)
        {
            _foodItemRepo = foodItemRepo;
        }

        //Add item in db
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> PostTodoItem(FoodItem foodItem)
        {
            return Ok(await _foodItemRepo.addItem(foodItem));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public ActionResult GetAllItems()
        {
            return Ok(_foodItemRepo.getItems());
        }

        //Get item by id
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(long id)
        {
            var item = await _foodItemRepo.getItemById(id);

            if (item == null)
            {
                return NotFound("Item does not exist");
            }

            return Ok(item);
        }

        //Update item details in db
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> UpdateItem([FromBody] FoodItem item)
        {
            try
            {
                await _foodItemRepo.updateItem(item);
                return Ok(item);
            }
            
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //Remove item from db
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem([FromRoute] long itemId)
        {
            try
            {
                await _foodItemRepo.removeItem(itemId);
                return Ok();
            }

            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}