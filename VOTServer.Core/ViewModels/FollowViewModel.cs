using System;

namespace VOTServer.Core.ViewModels
{
    public class FollowViewModel
    {
        public UserViewModel Followed { get; set; }

        public DateTime FollowTime { get; set; }
    }
}
