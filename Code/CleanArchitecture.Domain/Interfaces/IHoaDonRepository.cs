using CleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface IHoaDonRepository
    {
        IEnumerable<HoaDon> GetHoaDons();
        public ICollection<HoaDon> GetSearchHoaDon(string dataTimKiem, string loaiTimKiem);
        HoaDon GetHoaDon(int? Id);

        void Create(HoaDon hoadon);

        void Remove(int? Id);
    }
}
