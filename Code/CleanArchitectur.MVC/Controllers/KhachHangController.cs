﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectur.MVC.Controllers
{
    public class KhachHangController : Controller
    {
        private IKhachHangService khachHangService;

        public KhachHangController(IKhachHangService khachHangService)
        {
            this.khachHangService = khachHangService;
        }

        /*

        public IActionResult Index(string dataTimKiem, string loaiTimKiem, int PageNumber = 1)
        {
            ViewBag.Name = HttpContext.Session.GetString("Ten");
            if (HttpContext.Session.GetString("VaiTro") == "NguoiQuanTri")
            {
                if (dataTimKiem == null)
                {
                    NguoiDungViewModel model = iNguoiDungService.GetNguoiDungs();
                    ViewBag.TotalPages = Math.Ceiling(model.NguoiDungs.Count() / 5.0);
                    ViewBag.dataTimKiem = dataTimKiem;
                    ViewBag.loaiTimKiem = loaiTimKiem;
                    var user = model.NguoiDungs.Skip((PageNumber - 1) * 5).Take(5).ToList();
                    return View(user);
                }
                else
                {

                    var model = iNguoiDungService.GetSearchTenNguoiDung(dataTimKiem, loaiTimKiem);
                    ViewBag.TotalPages = Math.Ceiling(model.Count() / 5.0);
                    ViewBag.dataTimKiem = dataTimKiem;
                    ViewBag.loaiTimKiem = loaiTimKiem;
                    if (Math.Ceiling(model.Count() / 5.0) <= PageNumber - 1)
                    {
                        var user = model.Skip((1 - 1) * 5).Take(5).ToList();
                        return View(user);
                    }
                    else
                    {
                        var user = model.Skip((PageNumber - 1) * 5).Take(5).ToList();
                        return View(user);
                    }


                }

            }
            return RedirectToAction("Login");
        }

            */
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterNguoiDung nguoiDung)
        {
            if (ModelState.IsValid)
            {
                if (khachHangService.CheckTaiKhoan(nguoiDung.TaiKhoan))
                {
                    ViewBag.TB = "Tên Đăng Nhập Đã Tồn Tại";
                }
                else
                {
                    KhachHangDTO save = new KhachHangDTO()
                    {
                        TaiKhoan = nguoiDung.TaiKhoan,
                        MatKhau = nguoiDung.MatKhau,
                        VaiTro = KhachHangDTO.VaiTroo.NguoiDungThuong
                    };
                    khachHangService.Create(save);
                    ViewBag.TC = "Đăng Ký Thành Công";
                    return RedirectToAction("Login");
                }
            }
            return View(nguoiDung);
        }

        
        /*/////
        [HttpGet]
        public IActionResult Edit(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                if (HttpContext.Session.GetString("VaiTro") == "NguoiQuanTri")
                {
                    ViewBag.Name = HttpContext.Session.GetString("Ten");
                    var nguoiDung = iNguoiDungService.GetNguoiDung(Id);
                    return View(nguoiDung);
                }
                return RedirectToAction("Login");
            }
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirm(SaveNguoiDung save)
        {
            if (ModelState.IsValid)
            {
                iNguoiDungService.Create(save);
                return RedirectToAction("Index");
            }
            return View(save);
        }

        */

        [HttpGet]
        public IActionResult Login()
        {
            HttpContext.Session.Clear();
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = khachHangService.Login(loginModel.TaiKhoan, loginModel.MatKhau);
                if (result != null)
                {
                    HttpContext.Session.SetString("ID", result.Id + "");
                    HttpContext.Session.SetString("VaiTro", result.VaiTro + "");
                    HttpContext.Session.SetString("Ten", result.TenKhachHang + "");
                    ViewBag.DNTC = "Đăng Nhập Thành Công";
                    ViewBag.Name = result.TenKhachHang;
                    return Redirect(@"~/Home/Index");
                }
                else
                {
                    ViewBag.KTC = "Tên Đăng Nhập Hoặc Mật Khẩu Không Đúng";
                }
            }
            return View(loginModel);
        }



        public IActionResult Index(string dataTimKiem, string loaiTimKiem, int PageNumber = 1)
        {
            if (dataTimKiem == null)
            {
                var model = khachHangService.GetKhachHangs();
                ViewBag.TotalPages = Math.Ceiling(model.Count() / 1.0);
                ViewBag.dataTimKiem = dataTimKiem;
                ViewBag.loaiTimKiem = loaiTimKiem;
                var user = model.Skip((PageNumber - 1) * 1).Take(1).ToList();
                return View(user);
            }
            else
            {

                var model = khachHangService.GetSearchKhachHang(dataTimKiem, loaiTimKiem);
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
        public IActionResult Create(KhachHangDTO khachhangDTO)
        {
            if (ModelState.IsValid)
            {
                khachhangDTO.Id = 0;
                khachHangService.Create(khachhangDTO);
                return RedirectToAction("Index");
            }
            return View(khachhangDTO);
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
                var kh = khachHangService.GetKhachHang(Id);
                return View(kh);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int? Id)
        {
            khachHangService.Remove(Id);
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
                var kh = khachHangService.GetKhachHang(Id);
                return View(kh);
            }
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirm(KhachHangDTO khachhang)
        {
            if (ModelState.IsValid)
            {
                khachHangService.Create(khachhang);
                return RedirectToAction("Index");
            }
            return View(khachhang);
        }
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                var khachhangViewDetails = new KhachHangViewModel()
                {
                    khachhang = khachHangService.GetKhachHang(Id),

                };
                return View(khachHangService.GetKhachHang(Id));
            }
        }
    }
}