﻿using Literary_Junction.Data;
using Literary_Junction.Models;
using Microsoft.AspNetCore.Mvc;

namespace Literary_Junction.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList=_db.Categories.ToList();
            return View(objCategoryList);
        }


        //------------Create Method---------
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name==obj.DisplayOrder.ToString()) 
            {
                ModelState.AddModelError("name","DisplayOrder and CategoryName cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Created Successfully";
                return RedirectToAction("Index");
            }

            return View();
        }



        //========EDIT===================
        public IActionResult Edit(int? id)
        {
            if(id==null || id==0)
            {
                return NotFound();  
            }

            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id==id );
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();


            if (categoryFromDb==null)
            {
                return NotFound(categoryFromDb);
            }
            return View(categoryFromDb);
        }
        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category Updated Successfully";
                return RedirectToAction("Index");
            }

            return View();
        }


        //---------Delete Method------------
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category? categoryFromDb = _db.Categories.Find(id);
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id==id );
            //Category? categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();


            if (categoryFromDb == null)
            {
                return NotFound(categoryFromDb);
            }
            return View(categoryFromDb);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeletePost( int? id)
        {
            Category? obj=_db.Categories.Find(id);
            if (obj==null)
            {
                return NotFound() ;
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category Deleted Successfully";
            return RedirectToAction("Index");
        }
    }
}