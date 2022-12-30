
using Bogus;
using Template.Core.Models;
using Template.Core.Services;

namespace Template.Data.Services
{
    public static class Seeder
    {
        // use this class to seed the database with dummy test data using an IUserService 
        public static void Seed(IUserService svc)
        {
            // seeder destroys and recreates the database - NOT to be called in production!!!
            svc.Initialise();

            // add users
            svc.AddUser("Administrator", "admin@mail.com", "admin", Role.admin);
            svc.AddUser("Manager", "manager@mail.com", "manager", Role.manager);
            svc.AddUser("Guest", "guest@mail.com", "guest", Role.guest);

            // add 100 dummy user accounts using Faker
            var userFaker = new Faker<User>()
                                .RuleFor(u => u.Name, f => f.Name.FullName())
                                .RuleFor(u => u.Email, f => f.Internet.Email());

            var users = userFaker.Generate(100);
            users.ForEach( u => svc.AddUser(u.Name, u.Email, "password", Role.guest ));
    
        }
    }
}
