using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBContext
{
    public interface ICategoriesRepository
    {
        //List<EntityEmployee> GetEmployees();
        //EntityBaseResponse GetEmployees();
        Task<List<EntityBaseResponse>> GetCategories();
        EntityBaseResponse GetCategoriesId(int categoryID);
        EntityBaseResponse Insert(EntityCategories categories);
        EntityBaseResponse Update(EntityCategories categories);
        //EntityBaseResponse Delete(int categoryID);
        EntityBaseResponse Delete(EntityCategories categories);
    }
}
