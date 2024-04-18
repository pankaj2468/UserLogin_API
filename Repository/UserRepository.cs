using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace UserLogin_API
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(string connectionString)
        {
            _connectionString = connectionString;
        }


        public User GetUserByCredentials([FromBody] string username, string password)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("GetUserByCredentials", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Username", username); 
                    command.Parameters.AddWithValue("@Password", password);

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString()
                            };
                        }
                    }
                }
            }

            return user;
        }

       

        public bool RegisterUser(UserRegister model)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("registerUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters for the user registration details
                    command.Parameters.AddWithValue("@FullName", model.FullName);
                    command.Parameters.AddWithValue("@Email", model.Email);
                    command.Parameters.AddWithValue("@Password", model.Password);
                    command.Parameters.AddWithValue("@ConfirmPassword", model.ConfirmPassword);

                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    // If registration is successful (rowsAffected > 0), return true; otherwise, return false
                    return rowsAffected > 0;
                }
            }
        }

        public List<UserRegisterDetails> GetUserDetails()
        {
            List<UserRegisterDetails> users = new List<UserRegisterDetails>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                using (SqlCommand command = new SqlCommand("userdetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Add parameters if needed

                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            UserRegisterDetails user = new UserRegisterDetails
                            {
                                FullName = reader["FullName"].ToString(),
                                Email = reader["Email"].ToString(),
                                Password = reader["Password"].ToString(),
                                ConfirmPassword = reader["ConfirmPassword"].ToString()
                            };
                            users.Add(user);
                        }
                    }
                }
            }

            return users;
        }
    }

}
