using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBContext
{
    public interface IEmployeeRepository
    {
        //List<EntityEmployee> GetEmployees();
        //EntityBaseResponse GetEmployees();
        Task<List<EntityBaseResponse>> GetEmployees();
        EntityBaseResponse GetEmployeesId(int employeeId);
        EntityBaseResponse Insert(EntityEmployee employee);
        EntityBaseResponse Update(EntityEmployee employee);
        EntityBaseResponse Delete(EntityEmployee employee);
    }
}
