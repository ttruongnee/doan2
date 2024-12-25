using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLL_TheLoai : BLL_ITheLoai
    {
        private DAL_ITheLoai dAL_ITheLoai;

        public BLL_TheLoai(DAL_ITheLoai dAL_ITheLoai)
        {
            this.dAL_ITheLoai = dAL_ITheLoai;
        }


        public bool Create(Model_TheLoai TheLoai)
        {
            return dAL_ITheLoai.Create(TheLoai);
        }

        public bool Delete(string idTheLoai)
        {
            return dAL_ITheLoai.Delete(idTheLoai);
        }

        public List<Model_TheLoai> GetAll()
        {
            return dAL_ITheLoai.GetAll();
        }

        public List<Model_TheLoai> Search(string tenTheLoai)
        {
            return dAL_ITheLoai.Search(tenTheLoai);
        }

        public bool Update(Model_TheLoai TheLoai)
        {
            return dAL_ITheLoai.Update(TheLoai);
        }
    }
}
