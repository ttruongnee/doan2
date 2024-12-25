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
    public class DAL_Truyen : DAL_ITruyen
    {
        private IDatabaseHelper databaseHelper;
        public DAL_Truyen(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        public string getFolderPath()
        {
            return databaseHelper.GetImgFolder();
        }
        public bool Create(Model_Truyen ModelTruyen)
        {
            try
            {
                ModelTruyen.MaTruyen = Guid.NewGuid().ToString();
                var result = databaseHelper.ExecuteSProcedure("sp_CreateTruyen",
                    "@MaTruyen", ModelTruyen.MaTruyen,
                    "@AnhTruyen", ModelTruyen.AnhTruyen,
                    "@TenTruyen", ModelTruyen.TenTruyen,
                    "@ISBN", ModelTruyen.ISBN,
                    "@TacGia", ModelTruyen.TacGia,
                    "@DoiTuong", ModelTruyen.DoiTuong,
                    "@SoTrang", ModelTruyen.SoTrang,
                    "@DinhDang", ModelTruyen.DinhDang,
                    "@TrongLuong", ModelTruyen.TrongLuong,
                    "@MaTheLoai", ModelTruyen.MaTheLoai,
                    "@SoLuong", ModelTruyen.SoLuong,
                    "@GiaGoc", ModelTruyen.GiaGoc,
                    "@PhanTramGiamGia", ModelTruyen.PhanTramGiamGia);
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

        public bool Delete(string idTruyen)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_DeleteTruyen",
                    "@MaTruyen", idTruyen);
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

        public List<Model_Truyen> GetAll()
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetAllTruyen");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<Model_Truyen>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Model_Truyen> Search(string timKiem)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SearchTruyen",
                    "@TimKiem", timKiem);
                if (!string.IsNullOrEmpty(msgError.ToString()))
                {
                    throw new Exception(msgError.ToString());
                }
                return result.ConvertTo<Model_Truyen>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Model_Truyen ModelTruyen)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_UpdateTruyen",
                    "@MaTruyen", ModelTruyen.MaTruyen,
                    "@AnhTruyen", ModelTruyen.AnhTruyen,
                    "@TenTruyen", ModelTruyen.TenTruyen,
                    "@ISBN", ModelTruyen.ISBN,
                    "@TacGia", ModelTruyen.TacGia,
                    "@DoiTuong", ModelTruyen.DoiTuong,
                    "@SoTrang", ModelTruyen.SoTrang,
                    "@DinhDang", ModelTruyen.DinhDang,
                    "@TrongLuong", ModelTruyen.TrongLuong,
                    "@MaTheLoai", ModelTruyen.MaTheLoai,
                    "@SoLuong", ModelTruyen.SoLuong,
                    "@GiaGoc", ModelTruyen.GiaGoc,
                    "@PhanTramGiamGia", ModelTruyen.PhanTramGiamGia);
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
