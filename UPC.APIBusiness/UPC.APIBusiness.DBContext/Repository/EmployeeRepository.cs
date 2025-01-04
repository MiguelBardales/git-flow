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
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        //public List<EntityEmployee> GetEmployees()
        //{
        //    var returnEntity = new List<EntityEmployee>();
        //    using (var db = GetSqlConnection())
        //    {
        //        const string sql = @"usp_ObtenerEmployee";


        //        returnEntity = db.Query<EntityEmployee>(sql,
        //            commandType: CommandType.StoredProcedure).ToList();
        //    }
        //    return returnEntity;
        //}
        public async Task<List<EntityBaseResponse>> GetEmployees()
        {
            List<EntityBaseResponse> list = new List<EntityBaseResponse>();
            var response = new EntityBaseResponse();

            try
            {
                await using (var db = GetSqlConnection())
                {
                    var returnEntity = new List<EntityEmployee>();
                    const string sql = @"usp_ObtenerEmployee";

                    returnEntity = db.Query<EntityEmployee>(sql,
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
        public EntityBaseResponse GetEmployeesId(int employeeId)
        {
            var response = new EntityBaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    var returnEntity = new List<EntityEmployee>();
                    const string sql = @"usp_ObtenerEmployee_Id";
                    var p = new DynamicParameters();
                    
                    p.Add(name: "@EmployeeID", value: employeeId, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    returnEntity = db.Query<EntityEmployee>(sql,
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

        public EntityBaseResponse Insert(EntityEmployee employee)
        {
            var response = new EntityBaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = "usp_InsertEmployee";

                    var p = new DynamicParameters();
                    p.Add(name: "@EmployeeID", dbType: DbType.Int32, direction: ParameterDirection.Output);
                    p.Add(name: "@FirstName", value: employee.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@LastName", value: employee.LastName, dbType: DbType.String, direction: ParameterDirection.Input);                    
                    p.Add(name: "@Title", value: employee.Title, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@TitleOfCourtesy", value: employee.TitleOfCourtesy, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@BirthDate", value: employee.BirthDate, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@HireDate", value: employee.HireDate, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@Address", value: employee.Address, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@City", value: employee.City, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@Region", value: employee.Region, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@PostalCode", value: employee.PostalCode, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@Country", value: employee.Country, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@HomePhone", value: employee.HomePhone, dbType: DbType.String, direction: ParameterDirection.Input);

                    db.Query<EntityEmployee>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();

                    int idEmployee = p.Get<int>("@EmployeeID");

                    if(idEmployee > 0)
                    {
                        response.issuccess = true;
                        response.errorcode = "000";
                        response.errormessage = string.Empty;
                        response.data = new
                        {
                            id = idEmployee,
                            nombre = employee.FirstName
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

        public EntityBaseResponse Update(EntityEmployee employee)
        {
            var response = new EntityBaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = "usp_UpdateEmployee";

                    var p = new DynamicParameters();
                    p.Add(name: "@EmployeeID", value: employee.EmployeeID, dbType: DbType.Int32, direction: ParameterDirection.Input);
                    p.Add(name: "@FirstName", value: employee.FirstName, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@LastName", value: employee.LastName, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@Title", value: employee.Title, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@TitleOfCourtesy", value: employee.TitleOfCourtesy, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@BirthDate", value: employee.BirthDate, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@HireDate", value: employee.HireDate, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@Address", value: employee.Address, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@City", value: employee.City, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@Region", value: employee.Region, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@PostalCode", value: employee.PostalCode, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@Country", value: employee.Country, dbType: DbType.String, direction: ParameterDirection.Input);
                    p.Add(name: "@HomePhone", value: employee.HomePhone, dbType: DbType.String, direction: ParameterDirection.Input);

                    db.Query<EntityEmployee>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();


                    if (employee.EmployeeID > 0)
                    {
                        response.issuccess = true;
                        response.errorcode = "000";
                        response.errormessage = string.Empty;
                        response.data = new
                        {
                            id = employee.EmployeeID,
                            nombre = employee.FirstName
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

        public EntityBaseResponse Delete(EntityEmployee employee)
        {
            var response = new EntityBaseResponse();

            try
            {
                using (var db = GetSqlConnection())
                {
                    const string sql = "usp_DeleteEmployee";

                    var p = new DynamicParameters();
                    p.Add(name: "@EmployeeID", value: employee.EmployeeID, dbType: DbType.Int32, direction: ParameterDirection.Input);

                    db.Query<EntityEmployee>(
                        sql: sql,
                        param: p,
                        commandType: CommandType.StoredProcedure
                    ).FirstOrDefault();


                    if (employee.EmployeeID > 0)
                    {
                        response.issuccess = true;
                        response.errorcode = "000";
                        response.errormessage = string.Empty;
                        response.data = new
                        {
                            id = employee.EmployeeID
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
