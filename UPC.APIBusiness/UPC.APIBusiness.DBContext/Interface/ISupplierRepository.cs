using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DBContext
{
    public interface ISupplierRepository
    {
        Task<List<EntityBaseResponse>> GetSupplier();
    }
}
