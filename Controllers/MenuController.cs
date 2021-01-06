using Microsoft.AspNetCore.Mvc;
using epos.Models;
using epos.Repository.IRepository;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace epos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepo;

        public MenuController(IMenuRepository menuRepo)
        {
            _menuRepo = menuRepo;
        }

        //Add menu in db
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<ActionResult> PostMenu([FromBody] Menu menu)
        {
            return Ok(await _menuRepo.CreateMenu(menu));
        }

        //Add a category to the Menu
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("{name}")]
        public async Task<ActionResult> AddCategoryToMenu([FromRoute] string name, [FromBody] Category category)
        {
            var menu = await _menuRepo.GetMenuByName(name);

            if (menu == null)
            {
                return NotFound("Menu with the name " + name + " does not exist");
            }

            await _menuRepo.AddCategoryToMenu(menu, category);
            return Ok();

        }

        //See all menus
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public ActionResult GetMenus()
        {
            return Ok(_menuRepo.GetMenu());
        }


        //Get menu by name
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetMenuByName(string name)
        {
            var item = await _menuRepo.GetMenuByName(name);

            if (item == null)
            {
                return NotFound("Item does not exists");
            }

            Console.WriteLine(item);
            return Ok();
        }

        //Update menu details in db
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> UpdateMenu([FromBody] Menu menu)
        {
            try
            {
                await _menuRepo.UpdateMenu(menu);
                return Ok(menu);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        //Remove menu from db
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu([FromRoute] long id)
        {
            try
            {
                await _menuRepo.DeleteMenu(id);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}