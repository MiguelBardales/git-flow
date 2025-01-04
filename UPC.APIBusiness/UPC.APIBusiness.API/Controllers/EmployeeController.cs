using DBContext;
using DBEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    //[Route("api/employee")]
    [ApiController]
    public class EmployeeController : Controller
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected readonly IEmployeeRepository _EmployeeRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="EmployeeRepository"></param>
        public EmployeeController(IEmployeeRepository EmployeeRepository)
        {
            _EmployeeRepository = EmployeeRepository;

        }

        /// <summary>
        /// GetListUser
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        //[SwaggerOperation("GetEmployees")]
        [AllowAnonymous]
        [HttpGet]
        [Route("api/employee/GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            var ret = await _EmployeeRepository.GetEmployees();

            if (ret == null)
                return StatusCode(401);

            return Ok(Json(ret));
        }

        /// <summary>
        /// GetListUser
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        //[SwaggerOperation("GetEmployees")]
        [AllowAnonymous]
        [HttpGet]
        [Route("api/employee/GetEmployeesId/{employeeId:int}")]
        //[Route("GetEmployeesId")]
        public ActionResult GetEmployeesId(int employeeId)
        {
            var ret = _EmployeeRepository.GetEmployeesId(employeeId);

            if (ret == null)
                return StatusCode(401);

            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [Produces("application/json")]        
        [AllowAnonymous]
        [HttpPost]
        [Route("api/employee/Insert")]

        public ActionResult Insert(EntityEmployee employee)
        {
            var ret = _EmployeeRepository.Insert(employee);

            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost]
        [Route("api/employee/Update")]

        public ActionResult Update(EntityEmployee employee)
        {
            var ret = _EmployeeRepository.Update(employee);

            return Json(ret);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost]
        [Route("api/employee/Delete")]

        public ActionResult Delete(EntityEmployee employee)
        {
            var ret = _EmployeeRepository.Delete(employee);

            return Json(ret);
        }
    }
}
