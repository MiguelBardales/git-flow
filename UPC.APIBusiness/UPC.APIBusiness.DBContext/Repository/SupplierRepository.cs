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
    public class SupplierRepository : BaseRepository, ISupplierRepository
    {
        public async Task<List<EntityBaseResponse>> GetSupplier()
        {
            List<EntityBaseResponse> list = new List<EntityBaseResponse>();
            var response = new EntityBaseResponse();

            try
            {
                await using (var db = GetSqlConnection())
                {
                    var returnEntity = new List<EntitySupplier>();
                    const string sql = @"usp_ObtenerSuppliers";

                    returnEntity = db.Query<EntitySupplier>(sql,
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

    }
}
