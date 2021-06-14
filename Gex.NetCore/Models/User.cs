using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Identity;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class User : IdentityUser
    {
        public User()
        {
            Mesas = new HashSet<Mesas>();
            MesasAlumnos = new HashSet<MesasAlumnos>();
            RespuestasAlumnos = new HashSet<RespuestasAlumnos>();
        }

        public DateTimeOffset? EmailVerifiedAt { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public string RememberToken { get; set; }
        public string ProfilePhotoPath { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
        public long? Dni { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Observation { get; set; }
        public int? Type { get; set; }
        public int State { get; set; }

        public virtual ICollection<Mesas> Mesas { get; set; }
        public virtual ICollection<MesasAlumnos> MesasAlumnos { get; set; }
        public virtual ICollection<RespuestasAlumnos> RespuestasAlumnos { get; set; }
    }
}
