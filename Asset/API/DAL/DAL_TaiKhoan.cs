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
    public class DAL_TaiKhoan : DAL_ITaiKhoan
    {
        private IDatabaseHelper databaseHelper;
        public DAL_TaiKhoan(IDatabaseHelper databaseHelper)
        {
            this.databaseHelper = databaseHelper;
        }
        public bool Create(Model_CreateTaiKhoan taikhoan)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_CreateTaiKhoan",
                    "@TaiKhoan", taikhoan.TaiKhoan,
                    "@MatKhau", taikhoan.MatKhau,
                    "@Quyen", taikhoan.Quyen,
                    "@TenNguoiDung", taikhoan.TenNguoiDung,
                    "@GioiTinh", taikhoan.GioiTinh,
                    "@SoDienThoai", taikhoan.SoDienThoai,
                    "Email", taikhoan.Email,
                    "QueQuan", taikhoan.QueQuan);
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

        public bool DangNhapAdmin(Model_TaiKhoan taikhoan)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_DangNhapAdmin",
                    "@TaiKhoan", taikhoan.TaiKhoan,
                    "@MatKhau", taikhoan.MatKhau);

                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }

                // Kiểm tra nếu DataTable có dữ liệu
                if (result != null && result.Rows.Count > 0)
                {
                    return true;  // Đăng nhập thành công
                }
                else
                {
                    return false; // Đăng nhập thất bại
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đăng nhập: {ex.Message}");
                return false;  
            }
        }

        public bool DangNhapUser(Model_TaiKhoan taikhoan)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_DangNhapUser",
                    "@TaiKhoan", taikhoan.TaiKhoan,
                    "@MatKhau", taikhoan.MatKhau);

                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }

                // Kiểm tra nếu DataTable có dữ liệu
                if (result != null && result.Rows.Count > 0)
                {
                    return true;  // Đăng nhập thành công
                }
                else
                {
                    return false; // Đăng nhập thất bại
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi đăng nhập: {ex.Message}");
                return false;
            }
        }


        public bool Delete(string idTaiKhoan)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_DeleteTaiKhoan",
                    "@TaiKhoan", idTaiKhoan);
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

        public List<Model_TaiKhoan> GetAll()
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetAllTaiKhoan");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<Model_TaiKhoan>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Model_TaiKhoanUpdateMatKhau> GetEmailNhanVien()
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetEmailNhanVien");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<Model_TaiKhoanUpdateMatKhau>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Model_TaiKhoanUpdateMatKhau> GetEmailKhachHang()
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetEmailKhachHang");
                if (!string.IsNullOrEmpty(msgError))
                {
                    throw new Exception(msgError);
                }
                return result.ConvertTo<Model_TaiKhoanUpdateMatKhau>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<Model_TaiKhoan> Search(string tenNguoiDung)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_SearchTaiKhoanByName",
                    "@TenNguoiDung", tenNguoiDung);
                if (!string.IsNullOrEmpty(msgError.ToString()))
                {
                    throw new Exception(msgError.ToString());
                }
                return result.ConvertTo<Model_TaiKhoan>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Model_TaiKhoan> GetQuyenByID(string id)
        {
            string msgError = "";
            try
            {
                var result = databaseHelper.ExecuteSProcedureReturnDataTable(out msgError, "sp_GetQuyenByID",
                    "@TaiKhoan", id);
                if (!string.IsNullOrEmpty(msgError.ToString()))
                {
                    throw new Exception(msgError.ToString());
                }
                return result.ConvertTo<Model_TaiKhoan>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateMatKhau(Model_TaiKhoanUpdateMatKhau taikhoan)
        {
            try
            {
                var result = databaseHelper.ExecuteSProcedure("sp_UpdateMatKhau",
                    "@Email", taikhoan.Email,
                    "@MatKhau", taikhoan.MatKhau);
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
