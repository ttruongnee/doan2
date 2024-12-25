using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BLL
{
    public class BLLTruyen
    {
        private readonly DALTruyen _dalTruyen;

        public BLLTruyen(DALTruyen dalTruyen)
        {
            _dalTruyen = dalTruyen;
        }

        public List<DTOTruyen> GetAllTruyen()
        {
            return _dalTruyen.GetAllTruyen();
        }

    }
}
