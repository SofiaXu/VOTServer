using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VOTServer.Models.Interface;

namespace VOTServer.Models
{
    public class Video : IEntity
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Column(TypeName = "TEXT")]
        public string Info { get; set; }

        public DateTime UploadTime { get; set; }

        public long UploaderId { get; set; }

        public User Uploader { get; set; }

        public long GoodCount { get; set; }

        public long CommentsCount { get; set; }

        public long FavoriteCount { get; set; }

        public bool? IsDelete { get; set; }

        public virtual ICollection<VideoTag> Tags { get; set; }
    }
}
