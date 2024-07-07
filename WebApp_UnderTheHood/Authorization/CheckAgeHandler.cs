using Microsoft.AspNetCore.Authorization;

namespace WebApp_UnderTheHood.Authorization
{
    public class CheckAgeHandler : AuthorizationHandler<CheckAge>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CheckAge requirement)
        {
            var ageClaim = context.User.FindFirst(c => c.Type == "over18");
            if (ageClaim == null)
            {
                return Task.CompletedTask;
            }
            if (int.TryParse(ageClaim.Value, out int EmployeeAge))
            {
                var age = EmployeeAge - requirement.Age;
                if (age >= 0)
                {
                    context.Succeed(requirement);
                }
            }
            return Task.CompletedTask;
        }
    }
}
