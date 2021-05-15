using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VOTServer.Models.Interface;

namespace VOTServer.Models
{
    public class UserSecurity : IEntity
    {
        public long Id { get; set; }

        public string PasswordHash { get; set; }

        [ForeignKey(nameof(Id))]
        public User User { get; set; }

        public DateTime? PasswordUpdateTime { get; set; }

        [Phone]
        [StringLength(11)]
        public string PhoneNumber { get; set; }

        public bool? IsDelete { get; set; }
    }
}
