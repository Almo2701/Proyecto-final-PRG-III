using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers.Procesos
{
    public class salidasController : Controller
    {
        private RHumanosDBEntities db = new RHumanosDBEntities();

        // GET: salidas
        public ActionResult Index()
        {
            var salida = db.salida.Include(s => s.Empleados);
            return View(salida.ToList());
        }

        // GET: salidas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salida salida = db.salida.Find(id);
            if (salida == null)
            {
                return HttpNotFound();
            }
            return View(salida);
        }

        // GET: salidas/Create
        public ActionResult Create()
        {
            ViewBag.Empleado = new SelectList(db.Empleados, "Codigo_Empleado", "Codigo_Empleado");
            return View();
        }

        // POST: salidas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,Empleado,Fecha_Salida,Motivo,Tipo_Salida")] salida salida)
        {
            var consulta = from s in db.salida

                           select s;


            if (ModelState.IsValid)
            {

               int? EM = salida.Empleado;

                consulta = consulta.Where(a => a.Empleado == EM );
                if (consulta.Count() > 0)
                {

                    ViewBag.error = "El usuario no esta activo";
                }
                else
                {

                    var consulta2 = (from y in db.Empleados
                                     where y.Codigo_Empleado == EM
                                     select y).FirstOrDefault();

                    consulta2.Estatus = "nactivo";
                    db.salida.Add(salida);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
               
            }

            ViewBag.Empleado = new SelectList(db.Empleados, "Codigo_Empleado", "Codigo_Empleado", salida.Empleado);
            return View(salida);
        }

        // GET: salidas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salida salida = db.salida.Find(id);
            if (salida == null)
            {
                return HttpNotFound();
            }
            ViewBag.Empleado = new SelectList(db.Empleados, "Codigo_Empleado", "Codigo_Empleado", salida.Empleado);
            return View(salida);
        }

        // POST: salidas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,Empleado,Fecha_Salida,Motivo,Tipo_Salida")] salida salida)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salida).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Empleado = new SelectList(db.Empleados, "Codigo_Empleado", "Codigo_Empleado", salida.Empleado);
            return View(salida);
        }

        // GET: salidas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            salida salida = db.salida.Find(id);
            if (salida == null)
            {
                return HttpNotFound();
            }
            return View(salida);
        }

        // POST: salidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            salida salida = db.salida.Find(id);
            db.salida.Remove(salida);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
