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
    public class DAL_KhachHang : DAL_IKhachHang
    {
        private IDatabaseHelper databaseHelper;
        public DAL_KhachHang(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }

        public List<Model_KhachHang> GetAll()
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetAllKhachHang");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<Model_KhachHang>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Model_KhachHang> Search(string tenKhachHang)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SearchKhachHangByName",
                    "@TenKhachHang", tenKhachHang);
                if (!string.IsNullOrEmpty(msgError.ToString()))
                {
                    throw new Exception(msgError.ToString());
                }
                return result.ConvertTo<Model_KhachHang>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Model_KhachHang ModelKhachHang)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_UpdateKhachHang",
                    "@MaKhachHang", ModelKhachHang.MaKhachHang,
                    "@TenKhachHang", ModelKhachHang.TenKhachHang,
                    "@GioiTinh", ModelKhachHang.GioiTinh,
                    "@SoDienThoai", ModelKhachHang.SoDienThoai,
                    "@Email", ModelKhachHang.Email);
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
