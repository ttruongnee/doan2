using DAL.Helper;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALTruyen
    {
        private readonly DatabaseHelper _helper;

        public DALTruyen(string connectionString)
        {
            _helper = new DatabaseHelper(connectionString);
        }

        public List<DTOTruyen> GetAllTruyen()
        {
            string query = "SELECT MaTruyen, TenTruyen, TacGia, MaTheLoai, SoLuong, GiaBan, MoTa, AnhTruyenURL, DanhSachBinhLuanJSON FROM Truyen";

            return _helper.ExecuteSelect(query, reader => new DTOTruyen
            {
                MaTruyen = reader["MaTruyen"].ToString(),
                TenTruyen = reader["TenTruyen"].ToString(),
                TacGia = reader["TacGia"].ToString(),
                MaTheLoai = reader["MaTheLoai"].ToString(),
                SoLuong = (int)reader["SoLuong"],
                GiaBan = (double)reader["GiaBan"],
                MoTa = reader["MoTa"].ToString(),
                AnhTruyenURL = reader["AnhTruyenURL"].ToString(),
                DanhSachBinhLuanJSON = reader["DanhSachBinhLuanJSON"].ToString()
            });
        }
    }
}
