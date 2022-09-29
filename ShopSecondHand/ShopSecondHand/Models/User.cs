using System;
using System.Collections.Generic;

#nullable disable

namespace ShopSecondHand.Models
{
    public partial class User
    {
        public User()
        {
            Messages = new HashSet<Message>();
            Orders = new HashSet<Order>();
            Posts = new HashSet<Post>();
            Wallets = new HashSet<Wallet>();
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? BuildingId { get; set; }

        public virtual Building Building { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
        public virtual ICollection<Wallet> Wallets { get; set; }
    }
}
