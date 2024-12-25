using DAL.Helper;
using DAL.Helper.Interfaces;
using DAL.Interfaces;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DAL_DonHang : DAL_IDonHang
    {
        //public Model_ChiTietDonHang Model_ChiTietDonHang;
        private IDatabaseHelper databaseHelper;
        public DAL_DonHang(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        public bool Create(Model_DonHang ModelDonHang)
        {
            ModelDonHang.MaDonHang = Guid.NewGuid().ToString();
            string msgError = "";
            try
            {
                foreach (var chiTiet in ModelDonHang.listjson_chitiet)
                {
                    chiTiet.MaCTDonHang = Guid.NewGuid().ToString();
                }

                var xxx = MessageConvert.SerializeObject(ModelDonHang.listjson_chitiet);
                var result = databaseHelper.ExecuteScalarSProcedureWithTransaction(out msgError, "sp_CreateDonHang",
                    "@MaDonHang", ModelDonHang.MaDonHang,
                    "@MaKhachHangDatHang", ModelDonHang.MaKhachHangDatHang,
                    "@MaNhanVienXuLyDonHang", ModelDonHang.MaNhanVienXuLyDonHang,
                    "@NgayDatHang", ModelDonHang.NgayDatHang,
                    "@DiaChiGiaoHang", ModelDonHang.DiaChiGiaoHang,
                    "@TrangThai", ModelDonHang.TrangThai,
                    "@PhuongThucThanhToan", ModelDonHang.PhuongThucThanhToan,
                    "@MaGiamGia", ModelDonHang.MaGiamGia,
                    "@TongTien",ModelDonHang.TongTien,
                    "@listjson_chitiet", ModelDonHang.listjson_chitiet != null ? MessageConvert.SerializeObject(ModelDonHang.listjson_chitiet) : null);
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

        public bool Delete(string idDonHang)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_DeleteDonHang",
                    "@MaDonHang", idDonHang);
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

        public List<Model_DonHang> GetAll()
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetAllDonHang");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<Model_DonHang>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<Model_DonHang> Search(string search)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SearchDonHang",
                    "@Search", search);
                if (!string.IsNullOrEmpty(msgError.ToString()))
                {
                    throw new Exception(msgError.ToString());
                }
                return result.ConvertTo<Model_DonHang>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Model_DonHang ModelDonHang)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_UpdateDonHang",
                    "@MaDonHang", ModelDonHang.MaDonHang,
                    "@MaNhanVienXuLyDonHang", ModelDonHang.MaNhanVienXuLyDonHang,
                    "@TrangThai", ModelDonHang.TrangThai);
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
