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
using Microsoft.AspNetCore.Cors;
using System.Net.Http.Headers;
using System;

namespace API
{
    /// <summary>
    /// 
    /// </summary>
    [Produces("application/json")]
    //[Route("api/categories")]
    //[Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        static HttpClient client = new HttpClient();
        /// <summary>
        /// Constructor
        /// </summary>
        protected readonly ICategoriesRepository _CategoriesRepository;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CategoriesRepository"></param>
        public CategoriesController(ICategoriesRepository CategoriesRepository)
        {
            _CategoriesRepository = CategoriesRepository;
        }

        /// <summary>
        /// GetListUser
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        //[SwaggerOperation("GetCategories")]
        [AllowAnonymous]
        [HttpGet]
        [Route("api/categories/GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var ret = await _CategoriesRepository.GetCategories();

            if (ret == null)
                return StatusCode(401);

            return Ok(Json(ret));
        }

        /// <summary>
        /// GetListCategories
        /// </summary>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpGet]
        [Route("api/categories/GetCategoriesId/{categoryID:int}")]
        //[Route("GetEmployeesId")]
        public ActionResult GetCategoriesId(int categoryID)
        {
            var ret = _CategoriesRepository.GetCategoriesId(categoryID);

            if (ret == null)
                return StatusCode(401);

            return Json(ret);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        [HttpPost]
        [Route("api/categories/Insert")]

        public ActionResult Insert(EntityCategories categories)
        {
            var ret = _CategoriesRepository.Insert(categories);

            return Json(ret);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        //[HttpPut]
        [HttpPost]
        [Route("api/categories/Update")]

        public ActionResult Update(EntityCategories categories)
        {
            var ret = _CategoriesRepository.Update(categories);

            return Json(ret);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="categories"></param>
        /// <returns></returns>
        [Produces("application/json")]
        [AllowAnonymous]
        //[HttpDelete()]
        [HttpPost]
        //[Route("api/categories/Eliminar/{categoryID:int}")]
        //[Route("api/categories/Eliminar")]
        [Route("api/categories/Delete")]
        //[Route("Eliminar/{categoryID:int}")]
        //[Route("{categoryID:int}")]
        public ActionResult Delete(EntityCategories categories)
        {
            var ret = _CategoriesRepository.Delete(categories);

            return Json(ret);
        }

    }
}
