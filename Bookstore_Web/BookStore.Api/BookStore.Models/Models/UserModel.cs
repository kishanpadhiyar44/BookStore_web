using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.ViewModels;

namespace BookStore.Models.Models
{
    public class UserModel
    {
        public UserModel() { }

        public UserModel(UserModel user)
        {
            Id = user.Id;
            Firstname = user.Firstname;
            Lastname = user.Lastname;
            Email = user.Email;
            Roleid = user.Roleid;
        }

        public UserModel(User user)
        {
        }

        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public int Roleid { get; set; }
    }
}
