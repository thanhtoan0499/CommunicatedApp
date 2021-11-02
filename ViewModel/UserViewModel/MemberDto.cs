using DatingApp.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace DatingApp.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Username {  get; set; }
        public string PhotoUrl { get; set; }
        public DateTime DateOfBirth { get; set; }

        public bool? Gender { get; set; }


        //public ICollection<PhotoDto> Photos { get; set; }

    }
}
