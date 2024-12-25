using DAL.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.SqlClient;

namespace DAL
{
    public class DALTheLoai
    {
        private readonly DatabaseHelper _helper;

        public DALTheLoai(string connectionString)
        {
            _helper = new DatabaseHelper(connectionString);
        }

        public List<DTOTheLoai> GetAllTheLoai()
        {
            string query = "SELECT MaTheLoai, TenTheLoai, MoTa FROM TheLoai";
            return _helper.ExecuteSelect(query, reader => new DTOTheLoai
            {
                MaTheLoai = reader["MaTheLoai"].ToString(),
                TenTheLoai = reader["TenTheLoai"].ToString(),
                MoTa = reader["MoTa"].ToString()
            });
        }

        public void AddTheLoai(DTOTheLoai theLoai)
        {
            string query = "INSERT INTO TheLoai (MaTheLoai, TenTheLoai, MoTa) VALUES (@MaTheLoai, @TenTheLoai, @MoTa)";
            var parameters = new[]
            {
            new SqlParameter("@MaTheLoai", theLoai.MaTheLoai),
            new SqlParameter("@TenTheLoai", theLoai.TenTheLoai),
            new SqlParameter("@MoTa", theLoai.MoTa)
        };
            _helper.ExecuteNonQuery(query, parameters);
        }

        public void UpdateTheLoai(DTOTheLoai theLoai)
        {
            string query = "UPDATE TheLoai SET TenTheLoai = @TenTheLoai, MoTa = @MoTa WHERE MaTheLoai = @MaTheLoai";
            var parameters = new[]
            {
            new SqlParameter("@MaTheLoai", theLoai.MaTheLoai),
            new SqlParameter("@TenTheLoai", theLoai.TenTheLoai),
            new SqlParameter("@MoTa", theLoai.MoTa)
        };
            _helper.ExecuteNonQuery(query, parameters);
        }

        public void DeleteTheLoai(string maTheLoai)
        {
            string query = "DELETE FROM TheLoai WHERE MaTheLoai = @MaTheLoai";
            var parameters = new[]
            {
                new SqlParameter("@MaTheLoai", maTheLoai)
            };
            _helper.ExecuteNonQuery(query, parameters);
        }

        public DTOTheLoai GetTheLoaiById(string maTheLoai)
        {
            string query = "SELECT MaTheLoai, TenTheLoai, MoTa FROM TheLoai WHERE MaTheLoai = @MaTheLoai";
            var parameters = new[]
            {
        new SqlParameter("@MaTheLoai", maTheLoai) 
    };
            var result = _helper.ExecuteSelect(query, reader => new DTOTheLoai
            {
                MaTheLoai = reader["MaTheLoai"].ToString(),
                TenTheLoai = reader["TenTheLoai"].ToString(),
                MoTa = reader["MoTa"].ToString()
            }, parameters);

            return result.FirstOrDefault(); 
        }

    }
}
