using CleanArchitecture.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.Interfaces
{
    public interface ISanPhamService
    {
       
            IEnumerable<SanPhamDTO> GetSanPhams();

        public IEnumerable<SanPhamDTO> GetSearchSanPham(string dataTimKiem, string loaiTimKiem);
        SanPhamDTO GetSanPham(int? Id);

            void Create(SanPhamDTO sanpham);

            void Remove(int? Id);

            ICollection<SanPhamDTO> GetSanPhams(int? Id);


        
    }
}
