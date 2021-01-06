using Microsoft.AspNetCore.Mvc;
using epos.Repository.IRepository;
using epos.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace epos.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubcategoryController : ControllerBase
    {
        private readonly ISubcategoryRepository _subcatRepo;

        public SubcategoryController(ISubcategoryRepository subcatRepo)
        {
            _subcatRepo = subcatRepo;
        }

        [HttpPost]
        public async Task<ActionResult> CreateSubcategory(Subcategory subcategory)
        {
            return Ok(await _subcatRepo.CreateSubcategory(subcategory));
        }

        [HttpGet]
        public List<Subcategory> GetSubcategories()
        {
            return _subcatRepo.GetSubcategories();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSubcategory(Subcategory subcategory)
        {
            try
            {
                await _subcatRepo.UpdateSubcategory(subcategory);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }

            return Ok(subcategory);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteSubcategory([FromRoute] long id)
        {
            try
            {
                return Ok(await _subcatRepo.DeleteSubcategory(id));
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}