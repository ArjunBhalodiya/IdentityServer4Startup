using System;
using Test.IdentityServer.IdentityModels;

namespace Test.IdentityServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        public bool ValidateUser(string username, string password)
        {
            return true;
        }

        public IdentityUserModel FindBySubjectId(string subjectId)
        {
            return null;
        }

        public IdentityUserModel FindByUsername(string username)
        {
            return new IdentityUserModel
            {
                UserId = "bb74e8fa-ce2f-49c7-a615-5ccd24b9f4d4",
                EmailAddress = "adbhalodiya@gmail.com",
                Username = "ArjunBhalodiya"
            };
        }
    }
}