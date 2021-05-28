using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Track
    {
        public Track()
        {
            PlayListTracks = new HashSet<PlayListTrack>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public short Span { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }

        public virtual ICollection<PlayListTrack> PlayListTracks { get; set; }
    }
}
