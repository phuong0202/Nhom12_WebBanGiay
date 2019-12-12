using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectur.MVC.Controllers
{
    public class SanPhamController : Controller
    {
        private ISanPhamService sanphamService;
        public SanPhamController(ISanPhamService sanphamService)
        {
            this.sanphamService = sanphamService;

        }
        public IActionResult Index(string dataTimKiem, string loaiTimKiem, int PageNumber = 1)
        {
            if (dataTimKiem == null)
            {
                var model = sanphamService.GetSanPhams();
                ViewBag.TotalPages = Math.Ceiling(model.Count() / 1.0);
                ViewBag.dataTimKiem = dataTimKiem;
                ViewBag.loaiTimKiem = loaiTimKiem;
                var user = model.Skip((PageNumber - 1) * 1).Take(1).ToList();
                return View(user);
            }
            else
            {

                var model = sanphamService.GetSearchSanPham(dataTimKiem, loaiTimKiem);
                ViewBag.TotalPages = Math.Ceiling(model.Count() / 1.0);
                ViewBag.dataTimKiem = dataTimKiem;
                ViewBag.loaiTimKiem = loaiTimKiem;
                if (Math.Ceiling(model.Count() / 1.0) <= PageNumber - 1)
                {
                    var user = model.Skip((1 - 1) * 1).Take(1).ToList();
                    return View(user);
                }
                else
                {
                    var user = model.Skip((PageNumber - 1) * 1).Take(1).ToList();
                    return View(user);
                }
            }
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPhamDTO sanphamDTO)
        {
            if (ModelState.IsValid)
            {
                sanphamDTO.Id = 0;
                sanphamService.Create(sanphamDTO);
                return RedirectToAction("Index");
            }
            return View(sanphamDTO);
        }
        [HttpGet]
        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                var sp = sanphamService.GetSanPham(Id);
                return View(sp);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int? Id)
        {
            sanphamService.Remove(Id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                var sp = sanphamService.GetSanPham(Id);
                return View(sp);
            }
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirm(SanPhamDTO sanpham)
        {
            if (ModelState.IsValid)
            {
                sanphamService.Create(sanpham);
                return RedirectToAction("Index");
            }
            return View(sanpham);
        }
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                var sanphamViewDetails = new SanPhamViewDetails()
                {
                    SanPham = sanphamService.GetSanPham(Id),

                };
                return View(sanphamService.GetSanPham(Id));
            }
        }
    }
}