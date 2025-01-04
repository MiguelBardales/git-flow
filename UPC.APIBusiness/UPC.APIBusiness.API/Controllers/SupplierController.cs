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
    //[Route("api/supplier")]
    [ApiController]
    public class SupplierController : Controller
    {
        /// <summary>
        /// Constructor
        /// </summary>
        protected readonly ISupplierRepository _SupplierRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SupplierRepository"></param>
        public SupplierController(ISupplierRepository SupplierRepository)
        {
            _SupplierRepository = SupplierRepository;

        }

        /// <summary>
        /// GetListUser
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        //[SwaggerOperation("GetSupplier")]
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Supplier/GetSupplier")]
        public async Task<IActionResult> GetSupplier()
        {
            var ret = await _SupplierRepository.GetSupplier();

            if (ret == null)
                return StatusCode(401);

            return Ok(Json(ret));
        }

    }
}
