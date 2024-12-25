using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLTheLoai
    {
        private readonly DALTheLoai _dalTheLoai;

        public BLLTheLoai(DALTheLoai dalTheLoai)
        {
            _dalTheLoai = dalTheLoai;
        }

        public List<DTOTheLoai> GetAllTheLoai()
        {
            return _dalTheLoai.GetAllTheLoai();
        }

        public void AddTheLoai(DTOTheLoai theLoai)
        {
            _dalTheLoai.AddTheLoai(theLoai); 
        }

        public void UpdateTheLoai(DTOTheLoai theLoai)
        {
            _dalTheLoai.UpdateTheLoai(theLoai); 
        }

        public void DeleteTheLoai(string maTheLoai)
        {
            _dalTheLoai.DeleteTheLoai(maTheLoai); 
        }

        public DTOTheLoai GetTheLoaiById(string maTheLoai)
        {
            return _dalTheLoai.GetTheLoaiById(maTheLoai); 
        }
    }
}
