using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DockPanel.Components
{
    class User
    {
        public User() { }

        public User (string login, string pass, string email)
        {
            this.login = login;
            password = pass;
            this.email = email;
        }

        public override string ToString()
        {
            return id.ToString() + " | " + login + " | " + password + " | " + email;
        }

        public int id { get; set; }
        public string login { get; set; }

        public string password { get; set; }
        public string email { get; set; }


    }
}
