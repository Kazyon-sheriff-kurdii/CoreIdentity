using Microsoft.AspNetCore.Authorization;

namespace CoreIdentity.Authorization
{
    public class AdminFullAccessRequirment : IAuthorizationRequirement
    {
        public int ProbationMonths { get; }
        public AdminFullAccessRequirment(int probationMonths)
        {
            this.ProbationMonths = probationMonths;
        }
    }

 
}

