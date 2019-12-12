using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Data.Context;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace CleanArchitecture.Data.Repository
{
    public class HoaDonRepository : IHoaDonRepository
    {
        private WebEnglishDBContext webEnglishDBContext;

        public HoaDonRepository(WebEnglishDBContext webEnglishDBContext)
        {
            this.webEnglishDBContext = webEnglishDBContext;
        }
        public ICollection<HoaDon> GetSearchHoaDon(string dataTimKiem, string loaiTimKiem)
        {
            var hoaDon = from m in webEnglishDBContext.HoaDon
                          select m;
            if (!String.IsNullOrEmpty(dataTimKiem) && loaiTimKiem == "tenkhachhang")
            {
                hoaDon = hoaDon.Where(m => m.IdkhachHangNavigation.TenKhachHang.Contains(dataTimKiem));
                hoaDon = hoaDon.OrderBy(x => x.IdkhachHangNavigation.TenKhachHang);
            }
            return hoaDon.Include(t => t.IdkhachHangNavigation).ToList();
        }
        public void Create(HoaDon hoadon)
        {
            if (hoadon.Id == 0)
            {
                webEnglishDBContext.HoaDon.Add(hoadon);
                webEnglishDBContext.SaveChanges();
            }
            else
            {
                HoaDon findResults = webEnglishDBContext.HoaDon.Find(hoadon.Id);
                findResults.IdkhachHang = hoadon.IdkhachHang;
                findResults.Ngay = hoadon.Ngay;
                findResults.TongTien = hoadon.TongTien;
                webEnglishDBContext.SaveChanges();
            }
        }

        public HoaDon GetHoaDon(int? Id)
        {
            HoaDon findResults = webEnglishDBContext.HoaDon.Find(Id);
            return findResults;
        }

        public IEnumerable<HoaDon> GetHoaDons()
        {
            return webEnglishDBContext.HoaDon.Include(t => t.IdkhachHangNavigation);
        }

        public void Remove(int? Id)
        {
            HoaDon findResults = webEnglishDBContext.HoaDon.Find(Id);
            webEnglishDBContext.HoaDon.Remove(findResults);
            webEnglishDBContext.SaveChanges();
        }
    }
}
