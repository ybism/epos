using System.Collections.Generic;
using System.Threading.Tasks;
using epos.Models;

namespace epos.Repository.IRepository
{
    public interface ISubcategoryRepository
    {
        Task<Subcategory> CreateSubcategory(Subcategory subcategory);
        List<Subcategory> GetSubcategories();
        Task<Subcategory> UpdateSubcategory(Subcategory subcategory);
        Task<Subcategory> DeleteSubcategory(long id);
        Task<Subcategory> GetSubcategoryById(Subcategory subcategory);
    }
}