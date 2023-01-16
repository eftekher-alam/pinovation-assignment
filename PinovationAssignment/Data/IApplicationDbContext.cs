using PinovationAssignment.Models;

namespace PinovationAssignment.Data
{
    public interface IApplicationDbContext
    {
        public List<TblUsers> GetAllUsers();
        public TblUsers GetUserById(int id);
        public TblUsers GetUserByEmail(string email);
        public bool InsertUser(TblUsers user);
        public bool UpdateUser(TblUsers user);
        public int GetMaxUserId();
        public bool IsEmailUnique(string email);
        public List<TblCountry> GetAllCountry();
        public List<TblCity> GetCityByCountryId(int countryId);
        public List<TblCity> GetAllCity();
    }
}
