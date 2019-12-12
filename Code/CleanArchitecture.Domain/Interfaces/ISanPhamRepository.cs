using CleanArchitecture.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Domain.Interfaces
{
    public interface ISanPhamRepository
    {
        IEnumerable<SanPham> GetSanPhams();
        public ICollection<SanPham> GetSearchSanPham(string dataTimKiem, string loaiTimKiem);
        SanPham GetSanPham(int? Id);

        void Create(SanPham sanpham);

        void Remove(int? Id);
    }
}
