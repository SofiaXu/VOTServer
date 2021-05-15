using System;

namespace VOTServer.Core.ViewModels
{
    public class FollowViewModel
    {
        public UserViewModel Followee { get; set; }

        public DateTime FollowTime { get; set; }
    }
}
