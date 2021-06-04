using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VOTServer.Core.Interface;
using VOTServer.Core.ViewModels;
using VOTServer.Models;

namespace VOTServer.Core.Services
{
    public class VideoService : IVideoService
    {
        private readonly IVideoRepository videoRepository;
        public VideoService(IVideoRepository videoRepository)
        {
            this.videoRepository = videoRepository;
        }

        public async Task<VideoTagViewModel> GetVideo(long id)
        {
            var v = await videoRepository.GetEntityByIdAsync(id);
            if (v == null)
            {
                return null;
            }
            return new VideoTagViewModel
            {
                Id = v.Id,
                CommentsCount = v.CommentsCount,
                VideoStatus = v.IsDelete.HasValue ? v.IsDelete.Value ? VideoStatus.Deleted : VideoStatus.Normal : VideoStatus.WaitUpload,
                FavoriteCount = v.FavoriteCount,
                GoodCount = v.GoodCount,
                Info = v.Info,
                Tags = v.Tags.Select(t => new TagViewModel
                {
                    Id = t.Tag.Id,
                    Name = t.Tag.Name
                }),
                Title = v.Title,
                Uploader = new UserViewModel
                {
                    Id = v.UploaderId,
                    UserName = v.Uploader.UserName
                },
                UploadTime = v.UploadTime
            };
        }

        public async Task<IEnumerable<VideoViewModel>> GetVideos(int page, int pageSize)
        {
            return (await videoRepository.GetAllAsync(pageSize, page))
                .Select(x => new VideoViewModel
                {
                    CommentsCount = x.CommentsCount,
                    FavoriteCount = x.FavoriteCount,
                    GoodCount = x.GoodCount,
                    Id = x.Id,
                    Info = x.Info,
                    Title = x.Title,
                    Uploader = new UserViewModel
                    {
                        Id = x.UploaderId,
                        UserName = x.Uploader.UserName
                    },
                    VideoStatus = x.IsDelete.HasValue ? x.IsDelete.Value ? VideoStatus.Deleted : VideoStatus.Normal : VideoStatus.WaitUpload,
                    UploadTime = x.UploadTime
                });
        }

        public async Task CreateAsync(VideoTagViewModel video)
        {
            var v = new Video
            {
                Info = video.Info,
                UploaderId = video.UploaderId,
                Title = video.Title,
                UploadTime = DateTime.Now,
                CommentsCount = 0,
                FavoriteCount = 0,
                GoodCount = 0
            };
            await videoRepository.AddAsync(v);
            await videoRepository.AddTagsAsync(video.Tags.Select(t => new VideoTag { TagId = t.Id, VideoId = v.Id }));
        }

        public async Task UpdateAsync(VideoTagViewModel video)
        {
            await videoRepository.AddAsync(new Video
            {
                Id = video.Id,
                Info = string.IsNullOrWhiteSpace(video.Info) ? null : video.Info,
                Title = string.IsNullOrWhiteSpace(video.Title) ? null : video.Title,
                IsDelete = video.VideoStatus switch
                {
                    VideoStatus.Deleted => true,
                    VideoStatus.Normal => false,
                    VideoStatus.WaitUpload => null,
                    _ => null
                }
            });
        }

        public async Task<IEnumerable<VideoViewModel>> SearchVideos(string name, int page, int pageSize)
        {
            return (await videoRepository.SearchAsync(v => v.Title.Contains(name), pageSize, page))
                .Select(x => new VideoViewModel
                {
                    CommentsCount = x.CommentsCount,
                    FavoriteCount = x.FavoriteCount,
                    GoodCount = x.GoodCount,
                    Id = x.Id,
                    Info = x.Info,
                    Title = x.Title,
                    Uploader = new UserViewModel
                    {
                        Id = x.UploaderId,
                        UserName = x.Uploader.UserName
                    },
                    VideoStatus = x.IsDelete.HasValue ? x.IsDelete.Value ? VideoStatus.Deleted : VideoStatus.Normal : VideoStatus.WaitUpload,
                    UploadTime = x.UploadTime
                });
        }
    }
}
