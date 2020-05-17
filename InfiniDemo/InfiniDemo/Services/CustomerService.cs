using InfiniDemo.Interfaces;
using InfiniDemo.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfiniDemo.Services
{
    public class CustomerService : ICustomerService
    {
        private IBaseService _baseService;
        public CustomerService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<CustomerListResult> GetCustomerListAsync()
        {
            CustomerListResult customerListResult = new CustomerListResult();

            customerListResult = await _baseService.GetAsync<CustomerListResult>("/Card/GetListMobile");

            return customerListResult;
        }
    }
}
