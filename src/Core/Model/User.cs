using Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class User : IdentityUser
    {
        #region Properties
        public override string Id { get; set; } = Guid.NewGuid().ToString();
        public string CompanyId { get; protected set; }
        public State Status { get; protected set; }

        #endregion

        #region Navigations
        public virtual Company Company { get; protected set; }
        public virtual List<UserProject> UserProjects { get; protected set; } = new List<UserProject>();
        #endregion
        #region Construtors
        protected User()
        {

        }
        public User(Company company,string email,string username)
        {
            CompanyId = company.Id;
            UserName = username;
            Email = email;
        }


        #endregion
        #region Behaviour 

        public void Inactivate()
          => Status = State.Inactive;
        public void Activate()
            => Status = State.Active;
        #endregion
    }

    public class Role : IdentityRole
    {

    }
    public class UserRole : IdentityUserRole<int>
    {

    }
    public class RoleClaim : IdentityRoleClaim<int>
    {

    }
}
