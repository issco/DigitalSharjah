using MRHE;
using MRHE.Datasources;
using MRHE.Datasources.Users;
using MRHE.Helpers;

namespace MRHE.Datasources.Users
{
    public class User
    {
        public static string token;
        public static string otp;

        public User() { }
        public User(string name, string email, string password,UserRole role ,Stage stage,string eid,string mobile)
        {
            Name = name;
            Email = email;
            Password = password;
            Role = role;
            Stage = stage;
            EID = eid;
            Mobile = mobile;
        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string EID { get; set; }
        public string Mobile { get; set; }
        public Stage Stage { get; set; }
        public UserRole Role { get; set; }

        
    }
}

namespace MRHE.Datasources
{
public enum UserRole
    {
        Customer=1,
        Contractor=2,
        Guest = 3
    }
}