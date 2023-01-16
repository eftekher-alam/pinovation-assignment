using NuGet.Protocol.Plugins;
using PinovationAssignment.Models;
using System.Data;
using System.Data.SqlClient;

namespace PinovationAssignment.Data
{
    public class ApplicationDbContext : IApplicationDbContext
    {
        private readonly IConfiguration _configuration;
        public ApplicationDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //User
        public List<TblUsers> GetAllUsers()
        {
            List<TblUsers> users = new List<TblUsers>();

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myconnection"));
            connection.Open();
            SqlCommand command = new SqlCommand("sp_GetUsers", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                TblUsers user = new TblUsers();
                user.userId = (int)data.GetValue(0);
                user.fName = (string)data.GetValue(1);
                user.lName = data.GetValue(2) == DBNull.Value ? null : (string)data.GetValue(2);
                user.phoneNo = (long)data.GetValue(3);
                user.emailNo = (string)data.GetValue(4);
                user.cityId = (int)data.GetValue(5);
                user.userImg = (string)data.GetValue(6);
                user.userCV = data.GetValue(7) == DBNull.Value ? null : (string)data.GetValue(7);
                user.password = (string)data.GetValue(8);
                user.dob = Convert.ToDateTime(data.GetValue(9));
                users.Add(user);
            }
            connection.Close();
            return users;
        }

        public TblUsers GetUserById(int id)
        {
            TblUsers user = new TblUsers();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myconnection"));
            connection.Open();
            SqlCommand command = new SqlCommand("sp_GetUserById", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                user.userId = (int)data.GetValue(0);
                user.fName = (string)data.GetValue(1);
                user.lName = data.GetValue(2) == DBNull.Value ? null : (string)data.GetValue(2);
                user.phoneNo = (long)data.GetValue(3);
                user.emailNo = (string)data.GetValue(4);
                user.cityId = (int)data.GetValue(5);
                user.userImg = (string)data.GetValue(6);
                user.userCV = data.GetValue(7) == DBNull.Value ? null : (string)data.GetValue(7);
                user.password = (string)data.GetValue(8);
                user.dob = Convert.ToDateTime(data.GetValue(9));
            }
            connection.Close();
            return user;
        }

        public bool InsertUser(TblUsers user)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myconnection"));
            connection.Open();
            SqlCommand command = new SqlCommand("sp_InsertUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", user.userId);
            command.Parameters.AddWithValue("@fName", user.fName);
            command.Parameters.AddWithValue("@lName", user.lName == null ? DBNull.Value : user.lName);
            command.Parameters.AddWithValue("@phoneNo", user.phoneNo);
            command.Parameters.AddWithValue("@emailNo", user.emailNo);
            command.Parameters.AddWithValue("@userCity", user.cityId);
            command.Parameters.AddWithValue("@userImg", user.userImg);
            command.Parameters.AddWithValue("@userCV", user.userCV == null ? DBNull.Value : user.userCV);
            command.Parameters.AddWithValue("@password", user.password);
            command.Parameters.AddWithValue("@dob", user.dob);

            if (0 < command.ExecuteNonQuery())
            {
                return true;
            }
            connection.Close();
            return false;
        }

        public bool UpdateUser(TblUsers user)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myconnection"));
            connection.Open();
            SqlCommand command = new SqlCommand("sp_UpdateUser", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@userId", user.userId);
            command.Parameters.AddWithValue("@fName", user.fName);
            command.Parameters.AddWithValue("@lName", user.lName == null ? DBNull.Value : user.lName);
            command.Parameters.AddWithValue("@phoneNo", user.phoneNo);
            command.Parameters.AddWithValue("@emailNo", user.emailNo);
            command.Parameters.AddWithValue("@userCity", user.cityId);
            command.Parameters.AddWithValue("@userImg", user.userImg);
            command.Parameters.AddWithValue("@userCV", user.userCV);
            command.Parameters.AddWithValue("@password", user.password);
            command.Parameters.AddWithValue("@dob", user.dob);

            if (0 < command.ExecuteNonQuery())
            {
                return true;
            }
            connection.Close();
            return false;
        }

        //Country

        public List<TblCountry> GetAllCountry()
        {
            List<TblCountry> countries = new List<TblCountry>();

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myconnection"));
            connection.Open();
            SqlCommand command = new SqlCommand("sp_GetCountry", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                TblCountry country = new TblCountry();
                country.countryId = (int)data.GetValue(0);
                country.countryName = (string)data.GetValue(1);
                countries.Add(country);
            }
            connection.Close();
            return countries;
        }

        //City
        public List<TblCity> GetCityByCountryId(int countryId)
        {
            List<TblCity> cities = new List<TblCity>();

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myconnection"));
            connection.Open();
            SqlCommand command = new SqlCommand("sp_GetCityByCountryId", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@countryId", countryId);
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                TblCity city = new TblCity();
                city.cityId = (int)data.GetValue(0);
                city.cityName = (string)data.GetValue(1);
                city.countryId = (int)data.GetValue(2);
                cities.Add(city);
            }
            connection.Close();
            return cities;
        }

        public List<TblCity> GetAllCity()
        {
            List<TblCity> cities = new List<TblCity>();

            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myconnection"));
            connection.Open();
            SqlCommand command = new SqlCommand("sp_GetCity", connection);
            command.CommandType = CommandType.StoredProcedure;
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                TblCity city = new TblCity();
                city.cityId = (int)data.GetValue(0);
                city.cityName = (string)data.GetValue(1);
                city.countryId = (int)data.GetValue(2);
                cities.Add(city);
            }
            connection.Close();
            return cities;
        }


        //Valid Email
        public bool IsEmailUnique(string email)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myconnection"));
            connection.Open();
            SqlCommand command = new SqlCommand("sp_IsEmailUnique", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@email", email);
            SqlDataReader data = command.ExecuteReader();
            if(!data.HasRows)
            {
                return true;
            }
            return false;
        }

        //To get currnet maximum user id
        public int GetMaxUserId()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myconnection"));
            connection.Open();
            SqlCommand command = new SqlCommand("sp_GetMaxUserId", connection);
            int maxId = (command.ExecuteScalar() == DBNull.Value) ? 0 : (int)command.ExecuteScalar();
            return maxId;
        }

        public TblUsers GetUserByEmail(string email)
        {

            TblUsers user = new TblUsers();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("myconnection"));
            connection.Open();
            SqlCommand command = new SqlCommand("sp_GetUserByEmail", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@email", email);
            SqlDataReader data = command.ExecuteReader();
            while (data.Read())
            {
                user.userId = (int)data.GetValue(0);
                user.fName = (string)data.GetValue(1);
                user.lName = data.GetValue(2) == DBNull.Value ? null : (string)data.GetValue(2);
                user.phoneNo = (long)data.GetValue(3);
                user.emailNo = (string)data.GetValue(4);
                user.cityId = (int)data.GetValue(5);
                user.userImg = (string)data.GetValue(6);
                user.userCV = (string)data.GetValue(7);
                user.password = (string)data.GetValue(8);
                user.dob = Convert.ToDateTime(data.GetValue(9));
            }
            connection.Close();
            return user;
        }

    }
}
