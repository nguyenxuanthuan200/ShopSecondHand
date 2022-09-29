using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class Building
    {
        public Building()
        {
            Posts = new HashSet<Post>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
