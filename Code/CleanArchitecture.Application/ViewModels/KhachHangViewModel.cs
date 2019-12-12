using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArchitecture.Application.ViewModels
{
    public class KhachHangViewModel
    {
        public IEnumerable<KhachHangViewModel> NguoiDungs { get; set; }

        public KhachHangDTO khachhang { get; set; }

        public ICollection<KhachHangDTO> khachhangs { get; set; }
    }
}
