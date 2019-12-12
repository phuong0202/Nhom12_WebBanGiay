using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitectur.MVC.Controllers
{
    public class HoaDonController : Controller
    {
        private IHoaDonService hoadonService;
        public HoaDonController(IHoaDonService hoadonService)
        {
            this.hoadonService = hoadonService;

        }
        public IActionResult Index(string dataTimKiem, string loaiTimKiem, int PageNumber = 1)
        {
            if (dataTimKiem == null)
            {
                var model = hoadonService.GetHoaDons();
                ViewBag.TotalPages = Math.Ceiling(model.Count() / 1.0);
                ViewBag.dataTimKiem = dataTimKiem;
                ViewBag.loaiTimKiem = loaiTimKiem;
                var user = model.Skip((PageNumber - 1) * 1).Take(1).ToList();
                return View(user);
            }
            else
            {

                var model = hoadonService.GetSearchHoaDon(dataTimKiem, loaiTimKiem);
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
        public IActionResult Create(HoaDonDTO hoadonDTO)
        {
            if (ModelState.IsValid)
            {
                hoadonDTO.Id = 0;
                hoadonService.Create(hoadonDTO);
                return RedirectToAction("Index");
            }
            return View(hoadonDTO);
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
                var hd = hoadonService.GetHoaDon(Id);
                return View(hd);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int? Id)
        {
            hoadonService.Remove(Id);
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
                var hd = hoadonService.GetHoaDon(Id);
                return View(hd);
            }
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult EditConfirm(HoaDonDTO hoadon)
        {
            if (ModelState.IsValid)
            {
                hoadonService.Create(hoadon);
                return RedirectToAction("Index");
            }
            return View(hoadon);
        }
        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            else
            {
                var hoadonViewDetails = new HoaDonViewDetails()
                {
                    hoadon = hoadonService.GetHoaDon(Id),

                };
                return View(hoadonService.GetHoaDon(Id));
            }
        }
    }
}