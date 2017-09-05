using Microsoft.AspNetCore.JsonPatch.Operations;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatOrderingService.Service.Swagger
{
    public class SwaggerAuthorizationHeaderParameter:IOperationFilter
    {
        public readonly string _authSchemeName;
        public SwaggerAuthorizationHeaderParameter(string authScheme)
        {
            _authSchemeName = authScheme;
        }

        public void Apply(Swashbuckle.AspNetCore.Swagger.Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<IParameter>();

            operation.Parameters.Add(new NonBodyParameter
            {
                In = "header",
                Name="Authorization",
                Description="Auth Token.",
                Required=true,
                Type="string",
                Default=$"{_authSchemeName} ###"
            });
        }
    }
}
