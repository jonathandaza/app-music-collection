using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Playlist
    {
        public Playlist()
        {
            PlayListTracks = new HashSet<PlayListTrack>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public short Fans { get; set; }
        public DateTime Updated { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<PlayListTrack> PlayListTracks { get; set; }
    }
}
