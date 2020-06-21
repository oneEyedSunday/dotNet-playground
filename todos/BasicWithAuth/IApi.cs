using Microsoft.AspNetCore.Routing;

namespace BasicWithAuth
{
    interface IApi
    {
        void MapRoutes(IEndpointRouteBuilder endpoints);
    }
}