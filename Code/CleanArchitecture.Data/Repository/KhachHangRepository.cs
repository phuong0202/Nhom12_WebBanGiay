using System;
using System.Collections.Generic;
using CleanArchitecture.Data.Context;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;
using System.Linq;


namespace CleanArchitecture.Data.Repository
{
    public class KhachHangRepository : IKhachHangRopository
    {
        private readonly WebEnglishDBContext webEnglishDBContext;

        public KhachHangRepository(WebEnglishDBContext webEnglishDBContext)
        {
            this.webEnglishDBContext = webEnglishDBContext;
        }

        public ICollection<KhachHang> GetSearchKhachHang(string dataTimKiem, string loaiTimKiem)
        {
            var sanPham = from m in webEnglishDBContext.KhachHang
                          select m;
            if (!String.IsNullOrEmpty(dataTimKiem) && loaiTimKiem == "tenkhachhang")
            {
                sanPham = sanPham.Where(m => m.TenKhachHang.Contains(dataTimKiem));
                sanPham = sanPham.OrderBy(x => x.TenKhachHang);
            }
            else
            {
                if (!String.IsNullOrEmpty(dataTimKiem) && loaiTimKiem == "taikhoan")
                {
                    sanPham = sanPham.Where(m => m.TaiKhoan.Contains(dataTimKiem));
                    sanPham = sanPham.OrderBy(x => x.TaiKhoan);
                }
            }
            return sanPham.ToList();
        }
        public void Add(KhachHang nguoi)
        {
            if (nguoi.Id == 0)
            {
                nguoi.VaiTro = 1;
                webEnglishDBContext.KhachHang.Add(nguoi);
                webEnglishDBContext.SaveChanges();
            }
            else
            {
                KhachHang findResults = webEnglishDBContext.KhachHang.Find(nguoi.Id);
                findResults.TenKhachHang = nguoi.TenKhachHang;
                findResults.TaiKhoan = nguoi.TaiKhoan;
                findResults.Email = nguoi.Email;
                findResults.MatKhau = nguoi.MatKhau;
                findResults.Sdt = nguoi.Sdt;
                findResults.VaiTro = nguoi.VaiTro;
                webEnglishDBContext.SaveChanges();
            }
        }

        public bool CheckTaiKhoan(string taiKhoan)
        {
            return webEnglishDBContext.KhachHang.Count(x => x.TaiKhoan == taiKhoan) > 0;
        }

        public void Create(KhachHang khachhang)
        {
            if (khachhang.Id == 0)
            {
                webEnglishDBContext.KhachHang.Add(khachhang);
                webEnglishDBContext.SaveChanges();
            }
            else
            {
                KhachHang findResults = webEnglishDBContext.KhachHang.Find(khachhang.Id);
                findResults.TenKhachHang = khachhang.TenKhachHang;
                findResults.TaiKhoan = khachhang.TaiKhoan;
                findResults.Sdt = khachhang.Sdt;
                findResults.Email = khachhang.Email;
                findResults.VaiTro = khachhang.VaiTro;
                webEnglishDBContext.SaveChanges();
            }
        }

        public KhachHang GetNguoiDung(int? iD)
        {
            KhachHang findResults = webEnglishDBContext.KhachHang.Find(iD);
            return findResults;
        }

        public IEnumerable<KhachHang> GetNguoiDungs()
        {
            return webEnglishDBContext.KhachHang;
        }

        public ICollection<KhachHang> GetSearchTenNguoiDung(string dataTimKiem, string loaiTimKiem)
        {
            throw new NotImplementedException();
        }

        public KhachHang Login(string tenDangNhap, string matKhau)
        {
            var khachhang = from m in webEnglishDBContext.KhachHang
                            where m.TaiKhoan.Equals(tenDangNhap)
                            select m;
            if (khachhang.Count() > 0)
            {
                if (khachhang.First().MatKhau == matKhau)
                {
                    return khachhang.First();
                }
                return null;
            }
            return null;
        }

        public void Remove(int? Id)
        {
            KhachHang findResults = webEnglishDBContext.KhachHang.Find(Id);
            webEnglishDBContext.KhachHang.Remove(findResults);
            webEnglishDBContext.SaveChanges();
        }
    }
}
