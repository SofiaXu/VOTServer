using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VOTServer.Requests
{
    public class CreateVideoInformationRequest
    {
        [Required]
        [MinLength(1)]
        public string Title { get; set; }

        [Required]
        public string Info { get; set; }

        public IList<long> Tags { get; set; }
    }
}
