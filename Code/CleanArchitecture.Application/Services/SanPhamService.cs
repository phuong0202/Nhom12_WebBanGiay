using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.ViewModels;
using CleanArchitecture.Domain.Interfaces;
using CleanArchitecture.Domain.Models;

namespace CleanArchitecture.Application.Services
{
    public class SanPhamService : ISanPhamService
    {
        private readonly ISanPhamRepository sanphamRepository;
        private readonly IMapper iMapper;

        public SanPhamService(ISanPhamRepository sanphamRepository, IMapper mapper)
        {
            this.sanphamRepository = sanphamRepository;
            this.iMapper = mapper;
        }
        public IEnumerable<SanPhamDTO> GetSearchSanPham(string dataTimKiem, string loaiTimKiem)
        {
            return iMapper.Map<IEnumerable<SanPham>, IEnumerable<SanPhamDTO>>(sanphamRepository.GetSearchSanPham(dataTimKiem, loaiTimKiem));
        }
        public void Create(SanPhamDTO sanpham)
        {
            sanphamRepository.Create(iMapper.Map<SanPhamDTO, SanPham>(sanpham));
        }

        public SanPhamDTO GetSanPham(int? Id)
        {
            return iMapper.Map<SanPham, SanPhamDTO>(sanphamRepository.GetSanPham(Id));
        }

        public IEnumerable<SanPhamDTO> GetSanPhams()
        {
            return iMapper.Map<IEnumerable<SanPham>, IEnumerable<SanPhamDTO>>(sanphamRepository.GetSanPhams());
        }

        public ICollection<SanPhamDTO> GetSanPhams(int? Id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int? Id)
        {
            sanphamRepository.Remove(Id);
        }
    }
}
