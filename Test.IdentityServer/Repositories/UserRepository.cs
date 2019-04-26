using System;
using Test.IdentityServer.IdentityModels;

namespace Test.IdentityServer.Repositories
{
    public class UserRepository : IUserRepository
    {
        public bool ValidateUser(string username, string password)
        {
            throw new NotImplementedException();
        }

        public IdentityUserModel FindBySubjectId(string subjectId)
        {
            throw new NotImplementedException();
        }

        public IdentityUserModel FindByUsername(string username)
        {
            throw new NotImplementedException();
        }
    }
}
