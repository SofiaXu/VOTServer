namespace VOTServer.Core.ViewModels
{
    public class CommentViewModel
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public long GoodCount { get; set; }

        public UserViewModel Commenter { get; set; }

        public VideoViewModel Video { get; set; }
    }
}
