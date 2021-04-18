using Common;
namespace Core.Model
{


    public class UserProject : Entity<int>
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public State Status { get; set; }
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
