using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers.ControllersInformes
{
    public class EmpleadosActivosController : Controller
    {
        private RRHHPROGIIIEntities1 db = new RRHHPROGIIIEntities1();

        // GET: EmpleadosActivos
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.Cargos).Include(e => e.Departamento1);
            return View(empleados.ToList());
        }

        public ActionResult Buscar(String Estatus)
        {
            var Consulta = from e in db.Empleados select e;

            if (!String.IsNullOrEmpty(Estatus))
            {
                Consulta = Consulta.Where(A => A.Estatus.Contains(Estatus));
            }
            return View(Consulta);
        }

        // GET: EmpleadosActivos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // GET: EmpleadosActivos/Create
        public ActionResult Create()
        {
            ViewBag.Cargo = new SelectList(db.Cargos, "id", "Cargo");
            ViewBag.Departamento = new SelectList(db.Departamento, "Codigo_departamento", "Nombre");
            return View();
        }

        // POST: EmpleadosActivos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre,Apellido,Telefono,Fecha_ingreso,Cargo,Departamento,Estatus,Codigo_Empleado")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Empleados.Add(empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cargo = new SelectList(db.Cargos, "id", "Cargo", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamento, "Codigo_departamento", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // GET: EmpleadosActivos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.Cargo = new SelectList(db.Cargos, "id", "Cargo", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamento, "Codigo_departamento", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // POST: EmpleadosActivos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nombre,Apellido,Telefono,Fecha_ingreso,Cargo,Departamento,Estatus,Codigo_Empleado")] Empleados empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Cargo = new SelectList(db.Cargos, "id", "Cargo", empleados.Cargo);
            ViewBag.Departamento = new SelectList(db.Departamento, "Codigo_departamento", "Nombre", empleados.Departamento);
            return View(empleados);
        }

        // GET: EmpleadosActivos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleados = db.Empleados.Find(id);
            if (empleados == null)
            {
                return HttpNotFound();
            }
            return View(empleados);
        }

        // POST: EmpleadosActivos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleados = db.Empleados.Find(id);
            db.Empleados.Remove(empleados);
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
