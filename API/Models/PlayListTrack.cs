using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class PlayListTrack
    {
        public int Id { get; set; }
        public int PlayListId { get; set; }
        public int TrackId { get; set; }
        public byte State { get; set; }
        public short Tracks { get; set; }

        public virtual Playlist PlayList { get; set; }
        public virtual Track Track { get; set; }
    }
}
