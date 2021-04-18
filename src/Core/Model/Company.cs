using Common;
using System;
using System.Collections.Generic;

namespace Core.Model
{
    public class Company : Entity<int>
    {
        #region Properties
        public string Name { get; protected set; }
        public State Status { get; protected set; }
        #endregion

        #region Constructors
        protected Company() { }
        public Company(string name)
        {
            Name = name;
            Activate();
        }
        #endregion

        #region Navigations
        public virtual List<User> Users { get; protected set; } = new List<User>();
        public virtual List<Project> Projects { get; protected set; } = new List<Project>();
        #endregion

        #region Behavior
        public void Update(string name)
        {
            if (Status == State.Inactive)
                throw new Exception("Company is inactive.");
            Name = name;
        }
        public void Inactivate()
            => Status = State.Inactive;
        public void Activate()
            => Status = State.Active;
        #endregion
    }
}
