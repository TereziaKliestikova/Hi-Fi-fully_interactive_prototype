using System.Diagnostics;
using HIPA_BE.Enums;
using HIPA_BE.Models;
using Microsoft.AspNetCore.Identity;

namespace HIPA_BE.Data
{
    public class DbInitializer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        
        private struct UserEnvironmentDetails
        {
            public string FirstName;
            public string LastName;
            public string Email;
            public string Password;
        }

        private UserEnvironmentDetails _dummyStudentUserDetails;
        private UserEnvironmentDetails _dummyAdminUserDetails;

        public DbInitializer(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        public async Task Initialize()
        {
            ReadEnvironmentVariables();
            await CreateDummyUsers();
            await AssignRolesToUsers();
        }

        private void ReadEnvironmentVariables()
        {
            // Prepare dummy user
            var firstName = Environment.GetEnvironmentVariable("DUMMY_USER_FIRST_NAME");
            var lastName = Environment.GetEnvironmentVariable("DUMMY_USER_LAST_NAME");
            var email = Environment.GetEnvironmentVariable("DUMMY_USER_EMAIL");
            var password = Environment.GetEnvironmentVariable("DUMMY_USER_PASSWORD");
            if (email == null || password == null || firstName == null || lastName == null)
            {
                ///throw new Exception("Environment variables DUMMY_USER_EMAIL and DUMMY_USER_PASSWORD must be set");
            }
            _dummyStudentUserDetails = new UserEnvironmentDetails
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };
            
            // Prepare dummy admin user
            var adminFirstName = Environment.GetEnvironmentVariable("DUMMY_USER_ADMIN_FIRST_NAME");
            var adminLastName = Environment.GetEnvironmentVariable("DUMMY_USER_ADMIN_LAST_NAME");
            var adminEmail = Environment.GetEnvironmentVariable("DUMMY_USER_ADMIN_EMAIL");
            var adminPassword = Environment.GetEnvironmentVariable("DUMMY_USER_ADMIN_PASSWORD");
            if (adminEmail == null || adminPassword == null || adminFirstName == null || adminLastName == null)
            {
                throw new Exception("Environment variables for DUMMY_USER_ADMIN must be set");
            }
            _dummyAdminUserDetails = new UserEnvironmentDetails
            {
                FirstName = adminFirstName,
                LastName = adminLastName,
                Email = adminEmail,
                Password = adminPassword
            };
        }

        private async Task CreateDummyUsers()
        {
            await CreateUser(_dummyStudentUserDetails);
            await CreateUser(_dummyAdminUserDetails);
        }

        private async Task CreateUser(UserEnvironmentDetails userDetails)
        {
            var newUser = new ApplicationUser
            {
                FirstName = userDetails.FirstName,
                LastName = userDetails.LastName,
                Email = userDetails.Email,
                UserName = userDetails.Email,
                EmailConfirmed = true
            };

            var existingUser = await _userManager.FindByEmailAsync(userDetails.Email);
            if (existingUser == null)
            {
                var result = await _userManager.CreateAsync(newUser, userDetails.Password);
            }
        }

        private async Task AssignRolesToUsers()
        {
            await AssignRole(_dummyStudentUserDetails.Email, Roles.Student.ToString());
            await AssignRole(_dummyAdminUserDetails.Email, Roles.Admin.ToString());
        }

        private async Task AssignRole(string email, string role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, role);
            }
        }
    }
}
