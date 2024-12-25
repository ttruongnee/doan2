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

        public List<Model_Truyen> SearchByID(string id)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SearchByID",
                    "@MaTruyen", id);
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


        public List<Model_Truyen> SearchByName(string name)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SearchByName",
                    "@TimKiem", name);
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

        public List<Model_Truyen> GetTruyenByTheLoai(string idorname)
        {
            List <Model_Truyen> tam = new List <Model_Truyen>();
            if (idorname == "get-all")
            {
                tam = GetAll();
            }    
            else
            {
                string msgError = "";
                try
                {
                    var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetTruyenByTheLoai",
                        "@TenTheLoai", idorname);
                    if (!string.IsNullOrEmpty(msgError.ToString()))
                    {
                        throw new Exception(msgError.ToString());
                    }
                    tam =  result.ConvertTo<Model_Truyen>().ToList();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return tam;
        }

    }
}
