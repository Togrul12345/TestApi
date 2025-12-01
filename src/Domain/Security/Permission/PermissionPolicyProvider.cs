using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Domain.Security.Permission
{
    public class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        public DefaultAuthorizationPolicyProvider FallbackPolicyProvider { get; }

        public PermissionPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            FallbackPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            // Məsələn: policyName "PermissionUserList" ilə başlayırsa:
            if (policyName.StartsWith("Permission", StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder();
                // Məsələn: "PermissionUserList"
                policy.AddRequirements(new PermissionRequirement(policyName));
                return Task.FromResult(policy.Build());
            }
            // Əks halda fallback
            return FallbackPolicyProvider.GetPolicyAsync(policyName);
        }

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync()
        {
            return Task.FromResult<AuthorizationPolicy>(null);
        }
    }
}
