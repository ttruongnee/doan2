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

        public List<Model_GiamGia> ApplyMaGiamGia(string magiamgia)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_ApplyMaGiamGia",
                    "@MaGiamGia", magiamgia);
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

    }
}
