using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_NhanVien : DAL_INhanVien
    {
        private IDatabaseHelper databaseHelper;
        public DAL_NhanVien(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        public List<Model_NhanVien> GetAll()
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetAllNhanVien");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<Model_NhanVien>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Model_NhanVien> Search(string tenNhanVien)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SearchNhanVienByName", 
                    "@TenNhanVien", tenNhanVien);
                if (!string.IsNullOrEmpty(msgError.ToString()))
                {
                    throw new Exception(msgError.ToString());
                }
                return result.ConvertTo<Model_NhanVien>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Model_NhanVien> GetNhanVienByUser(string user)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetNhanVienByUser",
                    "@User", user);
                if (!string.IsNullOrEmpty(msgError.ToString()))
                {
                    throw new Exception(msgError.ToString());
                }
                return result.ConvertTo<Model_NhanVien>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Model_NhanVien ModelNhanVien)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_UpdateNhanVien",
                    "@MaNhanVien", ModelNhanVien.MaNhanVien,
                    "@TenNhanVien", ModelNhanVien.TenNhanVien,
                    "@GioiTinh", ModelNhanVien.GioiTinh,
                    "@QueQuan", ModelNhanVien.QueQuan,
                    "@SoDienThoai", ModelNhanVien.SoDienThoai,
                    "@Email", ModelNhanVien.Email);
                if (result != null && !string.IsNullOrEmpty(result.ToString()))
                {
                    throw new Exception(result.ToString());
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
