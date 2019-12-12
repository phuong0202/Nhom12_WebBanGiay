using System;
using System.Collections.Generic;
using System.Text;
using CleanArchitecture.Application.ViewModels;

namespace CleanArchitecture.Application.Interfaces
{
    public interface IKhachHangService
    {
        KhachHangViewModel GetNguoiDungs();
        public IEnumerable<KhachHangDTO> GetSearchKhachHang(string dataTimKiem, string loaiTimKiem);
        void Create(KhachHangDTO save);
        public IEnumerable<KhachHangDTO> GetSearchTenNguoiDung(string dataTimKiem, string loaiTimKiem);
        KhachHangDTO GetNguoiDung(int? iD);

        bool CheckTaiKhoan(string taiKhoan);

        public KhachHangDTO Login(string tenDangNhap, string matKhau);




        IEnumerable<KhachHangDTO> GetKhachHangs();

        KhachHangDTO GetKhachHang(int? Id);

        

        void Remove(int? Id);


        ICollection<KhachHangDTO> GetKhachHangs(int? Id);
    }
}
