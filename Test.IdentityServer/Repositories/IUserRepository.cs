using Test.IdentityServer.IdentityModels;

namespace Test.IdentityServer.Repositories
{
    public interface IUserRepository
    {
        bool ValidateUser(string username, string password);
        
        IdentityUserModel FindBySubjectId(string subjectId);
        
        IdentityUserModel FindByUsername(string username);
    }
}
