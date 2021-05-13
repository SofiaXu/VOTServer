using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VOTServer.Models.Interface;

namespace VOTServer.Models
{
    public class User : IEntity
    {
        [Key]
        public long Id { get; set; }

        public bool? IsDelete { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }

        public long UserRoleId { get; set; }

        public UserRole UserRole { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }

        public virtual ICollection<Video> UploadedVideos { get; set; }

        public UserSecurity UserSecurity { get; set; }
    }
}
