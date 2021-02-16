using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class Users
    {
        public Users()
        {
            Mesas = new HashSet<Mesas>();
            MesasAlumnos = new HashSet<MesasAlumnos>();
            RespuestasAlumnos = new HashSet<RespuestasAlumnos>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTimeOffset? EmailVerifiedAt { get; set; }
        public string Password { get; set; }
        public string TwoFactorSecret { get; set; }
        public string TwoFactorRecoveryCodes { get; set; }
        public string RememberToken { get; set; }
        public long? CurrentTeamId { get; set; }
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
