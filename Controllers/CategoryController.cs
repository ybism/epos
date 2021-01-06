using Microsoft.AspNetCore.Mvc;
using epos.Repository.IRepository;
using epos.Models;
using System.Threading.Tasks;
using System;

namespace epos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _catRepo;

        public CategoryController(ICategoryRepository CatRepo)
        {
            _catRepo = CatRepo;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory(Category category)
        {
            return Ok(await _catRepo.CreateCategory(category));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] long id)
        {
            try
            {
                await _catRepo.DeleteCategory(id);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{name}")]
        public async Task<ActionResult> GetCategories([FromRoute] string name)
        {
            return Ok(await _catRepo.GetSubcategoriesForCategory(name));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCategory(Category category)
        {
            try
            {
                await _catRepo.UpdateCategory(category);
                return Ok(category);
            }

            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}