using System.Collections.Generic;

namespace VOTServer.Core.ViewModels
{
    public class VideoTagViewModel : VideoViewModel
    {
        public IEnumerable<TagViewModel> Tags { get; set; }
    }
}
