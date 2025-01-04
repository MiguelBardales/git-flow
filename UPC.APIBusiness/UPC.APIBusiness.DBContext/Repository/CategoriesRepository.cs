using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DBContext
{
    public class CategoriesRepository : BaseRepository, ICategoriesRepository
    {
        public async Task<List<EntityBaseResponse>> GetCategories()
        {
            List<EntityBaseResponse> list = new List<EntityBaseResponse>();
            var response = new EntityBaseResponse();

            try
            {
                await using (var db = GetSqlConnection())
                {
                    var returnEntity = new List<EntityCategories>();
                    const string sql = @"usp_Categories_Obtener";

                    returnEntity = db.Query<EntityCategories>(sql,
                        commandType: CommandType.StoredProcedure).ToList();

                    if (returnEntity != null)
                    {
                        response.issuccess = true;
                        response.errorcode = "00";
                        response.errormessage = string.Empty;
                        response.data = returnEntity;
                    }
                    else
                    {
                        response.issuccess = true;
                        response.errorcode = "10";
                        response.errormessage = String.Empty;
                        response.data = null;
                    }
                    list.Add(response);
                }
                return list;
            }
            catch (Exception ex)
            {
                response.issuccess = true;
                response.errorcode = "001";
                response.errormessage = ex.Message;
                response.data = null;
                list.Add(response);
            }
            return list;
        }

        public EntityBaseResponse GetCategoriesId(int categoryID)
        {
            var response = new EntityBaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    var returnEntity = new List<EntityCategories>();
                    const string sql = @"usp_Categories_Obtener_Id";
                    var p = new DynamicParameters();

                    p.Add(name: "@CategoryID", value: categoryID, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    returnEntity = db.Query<EntityCategories>(sql,
                        param: p,
                        commandType: CommandType.StoredProcedure).ToList();


                    if (returnEntity != null)
                    {
                        response.issuccess = true;
                        response.errorcode = "00";
                        response.errormessage = string.Empty;
                        response.data = returnEntity;
                    }
                    else
                    {
                        response.issuccess = true;
                        response.errorcode = "10";
                        response.errormessage = String.Empty;
                        response.data = null;
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                response.issuccess = true;
                response.errorcode = "001";
                response.errormessage = ex.Message;
                response.data = null;
            }
            return response;
        }

        public EntityBaseResponse Insert(EntityCategories categories)
        {
            var response = new EntityBaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = "usp_Categories_Insert";

                    var p = new DynamicParameters();
                    p.Add(name: "@CategoryID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add(name: "@CategoryName", value: categories.CategoryName, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@Description", value: categories.Description, dbType: DbType.String, direction: ParameterDirection.Input);


                    db.Query<EntityCategories>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    int idEmployee = p.Get<int>("@CategoryID");

                    if (idEmployee > 0)
                    {
                        response.issuccess = true;
                        response.errorcode = "000";
                        response.errormessage = string.Empty;
                        response.data = new
                        {
                            id = idEmployee,
                            nombre = categories.CategoryName
                        };
                    }
                    else
                    {
                        response.issuccess = false;
                        response.errorcode = "0000";
                        response.errormessage = string.Empty;
                        response.data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                response.issuccess = false;
                response.errorcode = "0000";
                response.errormessage = ex.Message;
                response.data = null;
            }

            return response;
        }

        public EntityBaseResponse Update(EntityCategories categories)
        {
            var response = new EntityBaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = "usp_Categories_Update";

                    var p = new DynamicParameters();
                    p.Add(name: "@CategoryID", value: categories.CategoryID, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add(name: "@CategoryName", value: categories.CategoryName, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@Description", value: categories.Description, dbType: DbType.String, direction: ParameterDirection.Input);


                    db.Query<EntityCategories>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();


                    if (categories.CategoryID > 0)
                    {
                        response.issuccess = true;
                        response.errorcode = "000";
                        response.errormessage = string.Empty;
                        response.data = new
                        {
                            id = categories.CategoryID,
                            nombre = categories.CategoryName
                        };
                    }
                    else
                    {
                        response.issuccess = false;
                        response.errorcode = "0000";
                        response.errormessage = string.Empty;
                        response.data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                response.issuccess = false;
                response.errorcode = "0000";
                response.errormessage = ex.Message;
                response.data = null;
            }

            return response;
        }

        public EntityBaseResponse Delete(EntityCategories categories)
        {
            var response = new EntityBaseResponse();

            //List<EntityBaseResponse> list = new List<EntityBaseResponse>();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = "usp_Categories_Delete";

                    var p = new DynamicParameters();
                    p.Add(name: "@CategoryID", value: categories.CategoryID, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    db.Query<EntityCategories>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();


                    if (categories.CategoryID > 0)
                    {
                        response.issuccess = true;
                        response.errorcode = "000";
                        response.errormessage = string.Empty;
                        response.data = new
                        {
                            id = categories.CategoryID
                        };
                    }
                    else
                    {
                        response.issuccess = false;
                        response.errorcode = "0000";
                        response.errormessage = string.Empty;
                        response.data = null;
                    }
                }
            }
            catch (Exception ex)
            {
                response.issuccess = false;
                response.errorcode = "0000";
                response.errormessage = ex.Message;
                response.data = null;
            }

            return response;
        }

    }
}
