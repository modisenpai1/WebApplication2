using WebApplication2.Domain.Models;

namespace WebApplication2.Services
{
    public interface IOrgenizationServices
    {
        //Organizatoin
        void addOrgenization(Orginization orginization);
        void removeOrgenization(Orginization orginization);
        void updateorginzation(Orginization orginization);
        IEnumerable<Orginization> getAllorginizations();
        Orginization GetOrginzation(int id);
        bool SaveChanges();

        //orgUser
        void AddOrgUser(UserOrg userOrg);
        void UpdateRole(UserOrg userOrg);
        UserOrg GetOrgUser(int orgId,string userId);
        void DeleteOrgUser(UserOrg userOrg);

        //Address
        void AddAddress(Adress Address);
        void UpdateAddress(Adress Address);
        void DeleteAddress(Adress Address);
        Adress GetAddress(int AddressId);
        IEnumerable<Adress> GetOrgAddresses(int OrganizatoinId);
    }
} 
