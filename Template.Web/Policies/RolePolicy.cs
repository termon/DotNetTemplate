using Microsoft.AspNetCore.Authorization;

namespace Template.Web.Policies
{

    // Example Policy based Authorisation config in Program.cs
    /**
        services.AddAuthorization( options => {
                options.AddPolicy("RolePolicy", policy =>                    
                    policy.Requirements.Add(new RolePolicyRequirement("admin,manager"))
                );                
            });
            services.AddSingleton<IAuthorizationHandler, RolePolicyRequirementHandler>();
    */

    public class RolePolicyRequirement : IAuthorizationRequirement
    {       
        public string Roles { get; set; }

        public RolePolicyRequirement(string roles)
        {
            this.Roles = roles;            
        }
    }

    public class RolePolicyRequirementHandler : AuthorizationHandler<RolePolicyRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       RolePolicyRequirement requirement)
        {
            if (context.User.HasOneOfRoles(requirement.Roles))
            {
                // user has required role so success
                context.Succeed(requirement);
            }
           return Task.CompletedTask;
        }
    }
}
