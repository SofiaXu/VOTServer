namespace VOTServer.Core.ViewModels
{
    public class UserViewModel
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string EmailAddress { get; set; }

        public UserRoleViewModel UserRole { get; set; }

        public bool IsFollowed { get; set; }
    }
}
