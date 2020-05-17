using InfiniDemo.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfiniDemo.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerListResult> GetCustomerListAsync();
    }
}
