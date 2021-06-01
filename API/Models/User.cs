using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class User
    {
        public User()
        {
            Playlists = new HashSet<Playlist>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public byte GenreId { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual ICollection<Playlist> Playlists { get; set; }

        internal void SetGenre(Genre genre)
        {
            Genre = genre;
        }
    }
}
