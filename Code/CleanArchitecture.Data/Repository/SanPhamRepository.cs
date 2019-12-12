using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Data.Context;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using System.Linq;

namespace CleanArchitecture.Data.Repository
{
    public class SanPhamRepository : ISanPhamRepository
    {
        private WebEnglishDBContext webEnglishDBContext;

        public SanPhamRepository(WebEnglishDBContext webEnglishDBContext)
        {
            this.webEnglishDBContext = webEnglishDBContext;
        }
        public ICollection<SanPham> GetSearchSanPham(string dataTimKiem, string loaiTimKiem)
        {
            var sanPham = from m in webEnglishDBContext.SanPham
                          select m;
            if (!String.IsNullOrEmpty(dataTimKiem) && loaiTimKiem == "tensanpham")
            {
                sanPham = sanPham.Where(m => m.TenSanPham.Contains(dataTimKiem));
                sanPham = sanPham.OrderBy(x => x.TenSanPham);
            }
            else
            {
                if (!String.IsNullOrEmpty(dataTimKiem) && loaiTimKiem == "tenloaisanpham")
                {
                    sanPham = sanPham.Where(m => m.TenLoai.Contains(dataTimKiem));
                    sanPham = sanPham.OrderBy(x => x.TenLoai);
                }
            }
            return sanPham.ToList();
        }
        public void Create(SanPham sanpham)
        {
            if (sanpham.Id == 0)
            {
                webEnglishDBContext.SanPham.Add(sanpham);
                webEnglishDBContext.SaveChanges();
            }
            else
            {
                SanPham findResults = webEnglishDBContext.SanPham.Find(sanpham.Id);
                findResults.TenLoai = sanpham.TenLoai;
                findResults.TenSanPham = sanpham.TenSanPham;
                findResults.Size = sanpham.Size;
                findResults.Gia = sanpham.Gia;
                findResults.Hinh = sanpham.Hinh;
                findResults.SoLuong = sanpham.SoLuong;
                webEnglishDBContext.SaveChanges();
            }
        }

        public SanPham GetSanPham(int? Id)
        {
            SanPham findResults = webEnglishDBContext.SanPham.Find(Id);
            return findResults;
        }

        public IEnumerable<SanPham> GetSanPhams()
        {
            return webEnglishDBContext.SanPham;
        }

        public void Remove(int? Id)
        {
            SanPham findResults = webEnglishDBContext.SanPham.Find(Id);
            webEnglishDBContext.SanPham.Remove(findResults);
            webEnglishDBContext.SaveChanges();
        }
    }
}
