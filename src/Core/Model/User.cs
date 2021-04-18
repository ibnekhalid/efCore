using Common;
using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class User : Entity<int>
    {
        #region Properties
        public int CompanyId { get; protected set; }
        public string Email { get; protected set; }
        public string Username { get; protected set; }
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

        public User(Company company, string email, string username)
        {
            CompanyId = company.Id;
            Email = email;
            Username = username;
            Activate();
        }

        #endregion
        #region Behaviour 
        public void Update(string email, string username)
        {
            if (Status == State.Inactive)
                throw new Exception("Company is inactive.");
            Email = email;
            Username = username;
        }
        public void Inactivate()
          => Status = State.Inactive;
        public void Activate()
            => Status = State.Active;
        #endregion
    }
}
