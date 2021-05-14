using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VOTServer.Models;

namespace VOTServer.Infrastructure.Data
{
    public class VOTDbContext : DbContext
    {
        private DbSet<Video> Videos { get; set; }

        private DbSet<User> Users { get; set; }

        private DbSet<Comment> Comments { get; set; }

        private DbSet<Favorite> Favorites { get; set; }

        private DbSet<Tag> Tags { get; set; }

        private DbSet<VideoTag> VideoTags { get; set; }

        private DbSet<Follow> Follows { get; set; }

        private DbSet<UserSecurity> UserSecurities { get; set; }

        private VOTDbContext() : base() { }

        public VOTDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Follow>().HasCheckConstraint("CK_NotEqual", "FollowerId <> FolloweeId");
            modelBuilder.Entity<Follow>().HasIndex(x => new { x.FollowerId, x.FolloweeId }).IsUnique();
            modelBuilder.Entity<Follow>().HasOne(x => x.Follower).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.FollowerId);
            modelBuilder.Entity<Follow>().HasOne(x => x.Followee).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.FolloweeId);

            modelBuilder.Entity<Tag>().HasIndex(x => x.Name).IsUnique();

            modelBuilder.Entity<Favorite>().HasIndex(x => new { x.UserId, x.VideoId }).IsUnique();
            modelBuilder.Entity<Favorite>().HasOne(x => x.User).WithMany(x => x.Favorites).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.UserId);
            modelBuilder.Entity<Favorite>().HasOne(x => x.Video).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.VideoId);

            modelBuilder.Entity<VideoTag>().HasIndex(x => new { x.VideoId, x.TagId }).IsUnique();
            modelBuilder.Entity<VideoTag>().HasOne(x => x.Tag).WithMany(x => x.Videos).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.TagId);
            modelBuilder.Entity<VideoTag>().HasOne(x => x.Video).WithMany(x => x.Tags).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.VideoId);

            modelBuilder.Entity<Video>().HasOne(x => x.Uploader).WithMany(x => x.UploadedVideos).OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.UploaderId);

            modelBuilder.Entity<User>().HasOne(x => x.UserRole).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.UserRoleId);
            modelBuilder.Entity<User>().HasIndex(x => x.EmailAddress).IsUnique();

            modelBuilder.Entity<Comment>().HasOne(x => x.Video).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.VideoId);
            modelBuilder.Entity<Comment>().HasOne(x => x.Commenter).WithMany().OnDelete(DeleteBehavior.NoAction).HasForeignKey(x => x.CommenterId);

            modelBuilder.Entity<UserSecurity>().HasOne(x => x.User).WithOne().HasForeignKey<UserSecurity>(x => x.Id);
            modelBuilder.Entity<UserSecurity>().HasIndex(x => x.PhoneNumber).IsUnique();

            modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = 1, Name = "Administrators", AccessLevel = 999 });
            modelBuilder.Entity<UserRole>().HasData(new UserRole { Id = 2, Name = "Guest", AccessLevel = 1 });
            modelBuilder.Entity<User>().HasData(new User { Id = 1, UserName = "admin", EmailAddress = "admin@aoba.site", UserRoleId = 1 });
            modelBuilder.Entity<UserSecurity>().HasData(new UserSecurity { Id = 1, PasswordHash = "i8FdgByv78Ooocd0Q78EbAPBsHWdEcUkqt/2rWg6UmE=", PasswordUpdateTime = DateTime.Now });
        }
    }
}
