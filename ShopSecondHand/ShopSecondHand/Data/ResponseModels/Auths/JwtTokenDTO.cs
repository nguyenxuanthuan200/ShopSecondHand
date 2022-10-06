using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VoiceAPI.Models.Responses.Auths
{
    public class JwtTokenDTO
    {
        public Guid Id { get; set; }
        public string Phone { get; set; }
        public string UserName { get; set; }
        public Guid RoleId { get; set; }
        //public int Status { get; set; }
        public string JwtToken { get; set; }
    }
}
