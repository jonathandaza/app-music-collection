using System;
using System.Collections.Generic;

#nullable disable

namespace API.Models
{
    public partial class Genre
    {
        public Genre()
        {
            Users = new HashSet<User>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
