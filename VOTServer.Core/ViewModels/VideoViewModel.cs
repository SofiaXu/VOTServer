using System;

namespace VOTServer.Core.ViewModels
{
    public class VideoViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public string Info { get; set; }

        public DateTime UploadTime { get; set; }

        public long UploaderId { get; set; }

        public UserViewModel Uploader { get; set; }

        public long GoodCount { get; set; }

        public long CommentsCount { get; set; }

        public long FavoriteCount { get; set; }

        public VideoStatus VideoStatus { get; set; }
    }

    public enum VideoStatus
    {
        WaitUpload = 1,
        Normal = 2,
        Deleted = 3
    }
}
