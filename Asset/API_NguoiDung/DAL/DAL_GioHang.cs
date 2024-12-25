using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_GioHang : DAL_IGioHang
    {
        private IDatabaseHelper databaseHelper;
        public DAL_GioHang(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        public bool Create(Model_GioHang giohang)
        {
            string msgError = "";
            try
            {
                foreach (var chiTiet in giohang.listjson_chitiet)
                {
                    chiTiet.MaCTGioHang = Guid.NewGuid().ToString();
                }

                var xxx = MessageConvert.SerializeObject(giohang.listjson_chitiet);
                var result = databaseHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_CreateGioHang",
                    "@MaGioHang", giohang.MaGioHang,
                    "@MaKhachHang", giohang.MaKhachHang,
                    "@TongTien", giohang.TongTien,
                    "@listjson_chitiet", giohang.listjson_chitiet != null ? MessageConvert.SerializeObject(giohang.listjson_chitiet) : null);
                if ((result != null && !string.IsNullOrEmpty(result.ToString())) || !string.IsNullOrEmpty(msgError))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool Delete(string username)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_DeleteGioHang",
                    "@MaGioHang", username);
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

        public List<Model_GioHang> GetGioHangByUser(string username)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetGioHangByUser",
                    "@MaGioHang", username);
                if (!string.IsNullOrEmpty(msgError.ToString()))
                {
                    throw new Exception(msgError.ToString());
                }
                return result.ConvertTo<Model_GioHang>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
