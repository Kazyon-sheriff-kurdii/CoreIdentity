using Microsoft.AspNetCore.Authorization;

namespace CoreIdentity.Authorization
{
    public class AdminFullAccessRequirmentResultHandeler : AuthorizationHandler<AdminFullAccessRequirment>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AdminFullAccessRequirment requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "EmploymentDate"))
            {
                return Task.CompletedTask;
            }
            var employmentDate = DateTime.Parse(context.User.FindFirst(c => c.Type == "EmploymentDate")!.Value);
            var period = DateTime.Now - employmentDate;
            if(period.Days > 30 * requirement.ProbationMonths)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;

        }
    }
}
