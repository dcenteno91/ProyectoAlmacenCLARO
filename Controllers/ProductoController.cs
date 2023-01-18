using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebAlmacen.Models;
using WebAlmacen.Services;

namespace WebAlmacen.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ProductoService oProductoService = new ProductoService();

        // GET: Producto
        public ActionResult Index()
        {
            if (oProductoService.ProbarConeccion() != "Exito")
                return View("Error");

            var listaProductos = oProductoService.ListarProductos();

            return View(listaProductos);
        }

        // GET: Producto/Create
        public ActionResult Create()
        {
            ViewBag.Marcas = new SelectList(oProductoService.ListarMarcas(), "IdMarca", "Descripcion");
            ViewBag.Modelos = new SelectList(oProductoService.ListarModelos(), "IdModelo", "Descripcion");
            ViewBag.Almacenes = new SelectList(oProductoService.ListarAlmacenes(), "IdAlmacen", "NombreAlmacen");

            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        public ActionResult Create(Producto model)
        {
            try
            {
                string respuesta = oProductoService.CrearProducto(model);

                if (respuesta.TrimEnd() != "Ok")
                {
                    ViewBag.Error = respuesta;
                    return View("Error");
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        // GET: Producto/Edit/5
        public ActionResult Edit(int id)
        {
            Producto oProducto = oProductoService.BuscarProducto(id);

            ViewBag.Marcas = new SelectList(oProductoService.ListarMarcas(), "IdMarca", "Descripcion", oProducto.IdMarca);
            ViewBag.Modelos = new SelectList(oProductoService.ListarModelos(), "IdModelo", "Descripcion", oProducto.IdModelo);
            ViewBag.Almacenes = new SelectList(oProductoService.ListarAlmacenes(), "IdAlmacen", "NombreAlmacen", oProducto.IdAlmacen);

            return View(oProducto);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        public ActionResult Edit(Producto model)
        {
            try
            {
                string respuesta = oProductoService.ActualizarProducto(model);

                if (respuesta.TrimEnd() != "Ok")
                {
                    ViewBag.Error = respuesta;
                    return View("Error");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Producto oProducto = oProductoService.BuscarProducto(id);

            return View(oProducto);
        }

        // GET: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                string respuesta = oProductoService.EliminarProducto(id);

                if (respuesta.TrimEnd() != "Ok")
                {
                    ViewBag.Error = respuesta;
                    return View("Error");
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
