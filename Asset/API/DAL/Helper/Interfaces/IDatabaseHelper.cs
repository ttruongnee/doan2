using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Helper.Interfaces
{
    public class StoreParameterInfo
    {
        public string StoreProcedureName { get; set; }
        public List<Object> StoreProcedureParams { get; set; }
    }
    public interface IDatabaseHelper
    {
        void SetConnectionString(string connectionString);
        string OpenConnection();
        string CloseConnection();
        string ExecuteNoneQuery(string strquery);
        DataTable ExecuteQueryToDataTable(string strquery, out string msgError);
        string GetImgFolder();
        object ExecuteScalar(string strquery, out string msgError);
        #region Execute StoreProcedure
        /// <summary>
        /// Execute Procedure None Query
        /// </summary>
        /// <param name="sprocedureName">Procedure Name</param>
        /// <param name="paramObjects">List Param Objects, Each Item include 'ParamName' and 'ParamValue'</param>
        /// <returns>String.Empty when run query success or Message Error when run query happen issue</returns>

        object ExecuteScalarSProcedureWithTransaction(out string msgError, string sprocedureName, params object[] paramObjects);
        /// <summary>
        /// Execute Scalar Procedure query List store and command
        /// </summary>
        /// <param name="msgErrors">List Error message</param>
        /// <param name="storeParameterInfos">List Store and ListList Parameter</param>
        /// <returns>List Object return from storeprocedure</returns>

        string ExecuteSProcedure(string sprocedureName, params object[] paramObjects);
        /// <summary>
        /// Execute Procedure return DataTale
        /// </summary>
        /// <param name="msgError">String.Empty when run query success or Message Error when run query happen issue</param>
        /// <param name="sprocedureName">Procedure Name</param>
        /// <param name="paramObjects">List Param Objects, Each Item include 'ParamName' and 'ParamValue'</param>
        /// <returns>Table result</returns>

        DataTable ExecuteSProcedureReturnDataTable(out string msgError, string sprocedureName, params object[] paramObjects);
        /// <summary>
        /// Execute Procedure return DataSet
        /// </summary>
        /// <param name="msgError">String.Empty when run query success or Message Error when run query happen issue</param>
        /// <param name="sprocedureName">Procedure Name</param>
        /// <param name="paramObjects">List Param Objects, Each Item include 'ParamName' and 'ParamValue'</param>
        /// <returns>DataSet result</returns>

        object ExecuteScalarSProcedure(out string msgError, string sprocedureName, params object[] paramObjects);
        /// <summary>
        ///  Execute Scalar Procedure query with transaction
        /// </summary>
        /// <param name="msgError">String.Empty when run query success or Message Error when run query happen issue</param>        
        /// <param name="sprocedureName">Procedure Name</param>
        /// <param name="paramObjects">List Param Objects, Each Item include 'ParamName' and 'ParamValue'</param>
        /// <returns>Value return from Store</returns>
        #endregion
    }
}
