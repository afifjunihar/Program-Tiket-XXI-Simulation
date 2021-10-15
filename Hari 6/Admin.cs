using System;
using System.Collections.Generic;
using System.Text;

namespace Hari_6
{
    public class Admin
    {
        public string fullName;
        private string usernameAdmin;
        public string unameAdmin
        {
            get
            {
                return usernameAdmin;
            }
            set
            {
                usernameAdmin = value;
            }
        }

        private string pass;
        public string passwordAdmin
        {
            get
            {
                return pass;
            }
            set
            {
                pass = BCrypt.Net.BCrypt.HashPassword(value); ;
            }
        }

        public Admin(string fullName, string usernameAdmin, string pass)
        {
            this.fullName = fullName;
            this.usernameAdmin = usernameAdmin;
            this.pass = BCrypt.Net.BCrypt.HashPassword(pass); ;
        }
    }
}
