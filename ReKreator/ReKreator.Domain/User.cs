using System;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ReKreator.Domain
{
    public class User : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime RegistrationDate { get; set; }
        
        public virtual ICollection<EventHolding_User> EventHoldings_Users { get; set; }
        public virtual UserMailing UserMailing { get; set; }
    }
}
