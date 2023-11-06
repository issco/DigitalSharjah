using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MRHE;
using MRHE.Datasources;
using MRHE.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MRHE.Datasources.Users
{
    public static class UsersData
    {
        private static IEnumerable<User> _users;

        private static IEnumerable<User> ImportUsers()
        {

            return File
                 .ReadAllLines(Path.Combine("Datasources", "Users", "users.csv"))
                 .Skip(1)
                 .Select(e => e.Split(','))
                 .Where(e => e.Length > 0)
                 .Select(
                 e => new User(
                     e[0].Trim(),                                   // Name
                     e[1].Trim(),                                   // Email
                     e[2].Trim(),                                   // Password
                     (UserRole)Enum.Parse(typeof(UserRole), e[3]),  // Role
                     (Stage)Enum.Parse(typeof(Stage), e[4]),        // Stage
                     e[5].Trim(),                                   // EID
                     e[6].Trim()                                    // Mobile
                     )
                 );

  
        }



        public static IEnumerable<User> SearchUsers(Stage stage,UserRole role, int take = 1, string name = "")
        {
        

            _users ??= ImportUsers();

          
            var query = _users
                .Where(e => e.Stage == stage && e.Role ==role);
    
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query
                    .Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            return query
                .Take(take);
        }
    }
}