using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace FirstDemo.Persistence.Features.Membership
{
    public class ApplicationUserRole
        : IdentityUserRole<Guid>
    {
       
    }
}
