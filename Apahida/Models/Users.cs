namespace Apahida.Models
{
    public class Users
    {

        private int id;
        private String name;
        private String role;
        private String username;
        private String password;
        public Users() { }
        public Users(int id, String name, String role, String username, String password)
        {
            Id = id;
            Username = username;
            Password = password;
            Name = name;
            Role = role;
        }

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Role
        {
            get { return role; }
            set { role = value; }
        }
    }
}
