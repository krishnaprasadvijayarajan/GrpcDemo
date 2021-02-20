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
    }
}
