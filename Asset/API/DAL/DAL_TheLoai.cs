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
    public class DAL_TheLoai : DAL_ITheLoai
    {
        private IDatabaseHelper databaseHelper;
        public DAL_TheLoai(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        public bool Create(Model_TheLoai ModelTheLoai)
        {
            try
            {
                ModelTheLoai.MaTheLoai = Guid.NewGuid().ToString();
                var result = databaseHelper.ExecuteSProcedure("sp_CreateTheLoai",
                    "@MaTheLoai", ModelTheLoai.MaTheLoai,
                    "@TenTheLoai", ModelTheLoai.TenTheLoai,
                    "@MoTa", ModelTheLoai.MoTa);
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

        public bool Delete(string idTheLoai)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_DeleteTheLoai",
                    "@MaTheLoai", idTheLoai);
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

        public List<Model_TheLoai> GetAll()
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetAllTheLoai");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<Model_TheLoai>().ToList();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public List<Model_TheLoai> Search(string tenTheLoai)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SearchTheLoaiByName",
                    "@TenTheLoai", tenTheLoai);
                if (!string.IsNullOrEmpty(msgError.ToString()))
                {
                    throw new Exception(msgError.ToString());
                }
                return result.ConvertTo<Model_TheLoai>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(Model_TheLoai ModelTheLoai)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_UpdateTheLoai",
                    "@MaTheLoai", ModelTheLoai.MaTheLoai,
                    "@TenTheLoai", ModelTheLoai.TenTheLoai,
                    "@MoTa", ModelTheLoai.MoTa);
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
