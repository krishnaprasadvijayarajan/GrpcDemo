using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustomersService:Customer.CustomerBase
    {
        private readonly ILogger<CustomersService> _logger;

        public CustomersService(ILogger<CustomersService> logger)
        {
            _logger = logger;
        }

        public override Task<CustomerModel> GetCustomerInfo(CustomerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new();
            switch (request.UserId)
            {
                case 1:
                    output.FirstName = "FirstName1";
                    output.LastName = "LastName1";
                    break;
                case 2:
                    output.FirstName = "FirstName2";
                    output.LastName = "LastName2";
                    break;
                _:
                    output.FirstName = "FirstNameDefault";
                    output.LastName = "LastNameDefault";
            }

            return Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            List<CustomerModel> customers = new()
            {
                new CustomerModel
                {
                    FirstName = "New FirstName1",
                    LastName="New LastName1",    
                    EmailAddress="email1",
                    Age=21,
                    IsAlive=true
                },
                new CustomerModel
                {
                    FirstName = "New FirstName2",
                    LastName = "New LastName2",
                    EmailAddress = "email2",
                    Age = 45,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "New FirstName3",
                    LastName = "New LastName3",
                    EmailAddress = "email3",
                    Age = 95,
                    IsAlive = false
                }
            };
            foreach(var cust in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(cust);
            }
        }
    }
}
