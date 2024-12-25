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
    public class DAL_GiamGia : DAL_IGiamGia
    {
        private IDatabaseHelper databaseHelper;
        public DAL_GiamGia(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        public bool Create(Model_GiamGia ModelGiamGia)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_CreateGiamGia",
                    "@MaGiamGia", ModelGiamGia.MaGiamGia,
                    "@NgayBatDau", ModelGiamGia.NgayBatDau,
                    "@NgayKetThuc", ModelGiamGia.NgayKetThuc,
                    "@PhanTramGiamGia", ModelGiamGia.PhanTramGiamGia);
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

        public bool Delete(string idGiamGia)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_DeleteGiamGia",
                    "@MaGiamGia", idGiamGia);
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

        public List<Model_GiamGia> GetAll()
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetAllGiamGia");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<Model_GiamGia>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Model_GiamGia> Search(DateTime? NgayBatDau, DateTime? NgayKetThuc)
        {
            string msgError = "";
            try
            {
                if (NgayBatDau.HasValue && NgayBatDau.Value == new DateTime(1, 1, 1))
                {
                    NgayBatDau = DateTime.Now;
                }

                if (NgayKetThuc.HasValue && NgayKetThuc.Value == new DateTime(1, 1, 1))
                {
                    NgayKetThuc = DateTime.Now;
                }

                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SearchMaGiamGiaByDateTime",
                    "@NgayBatDau", NgayBatDau,
                    "@NgayKetThuc", NgayKetThuc);
                if (!string.IsNullOrEmpty(msgError.ToString()))
                {
                    throw new Exception(msgError.ToString());
                }
                return result.ConvertTo<Model_GiamGia>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Model_GiamGia ModelGiamGia)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_UpdateGiamGia",
                    "@MaGiamGia", ModelGiamGia.MaGiamGia,
                    "@NgayBatDau", ModelGiamGia.NgayBatDau,
                    "@NgayKetThuc", ModelGiamGia.NgayKetThuc,
                    "@PhanTramGiamGia", ModelGiamGia.PhanTramGiamGia);
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
