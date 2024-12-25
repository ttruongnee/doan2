using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        // Constructor để khởi tạo chuỗi kết nối
        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Phương thức để lấy kết nối đến cơ sở dữ liệu
        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // Phương thức để tạo lệnh SQL với tham số
        public SqlCommand CreateCommand(string query, SqlConnection connection, params SqlParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(query, connection); // Tạo lệnh SQL mới
            if (parameters != null && parameters.Length > 0) // Kiểm tra xem có tham số không
            {
                command.Parameters.AddRange(parameters); // Thêm tham số vào lệnh
            }
            return command; // Trả về lệnh đã được tạo
        }


        // Phương thức thực thi câu lệnh SQL SELECT và trả về danh sách kết quả
        public List<T> ExecuteSelect<T>(string query, Func<SqlDataReader, T> mapFunction, params SqlParameter[] parameters)
        {
            List<T> list = new List<T>();
            using (SqlConnection conn = GetConnection())
            {
                conn.Open();
                using (SqlCommand cmd = CreateCommand(query, conn, parameters))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(mapFunction(reader));
                        }
                    }
                }
            }
            return list;
        }


        // Phương thức thực thi câu lệnh SQL INSERT, UPDATE, DELETE
        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection connection = GetConnection())
            {
                connection.Open(); // Mở kết nối

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Thêm các tham số vào câu lệnh
                    if (parameters != null && parameters.Length > 0)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    // Thực thi câu lệnh và trả về số dòng bị ảnh hưởng
                    return command.ExecuteNonQuery();
                }
            }
        }
    }
}
