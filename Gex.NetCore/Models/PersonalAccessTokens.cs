using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Gex.NetCore.Models
{
    public partial class PersonalAccessTokens
    {
        public long Id { get; set; }
        public string TokenableType { get; set; }
        public long TokenableId { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string Abilities { get; set; }
        public DateTimeOffset? LastUsedAt { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public DateTimeOffset? UpdatedAt { get; set; }
    }
}
