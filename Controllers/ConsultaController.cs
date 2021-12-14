using RegistroBioseguridad.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Data.Entity;

namespace RegistroBioseguridad.Controllers
{
    public class ConsultaController : Controller
    {
        ContextBd context = new ContextBd();
        // GET: Consulta
        public ActionResult Index()
        {
            return View();
        }

        /* Parametros para la datatable */
        public string draw = "";
        public string start = "";
        public string length = "";
        public string sortColumn = "";
        public string sortColumnDir = "";
        public string searchValue = "";
        public int pageSize, skip, recordsTotal;
        [HttpPost]
        public ActionResult MostrarTabla()
        {
            List<ConsultaViewModel> list = new List<ConsultaViewModel>();
            var draw = Request.Form.GetValues("draw").FirstOrDefault();
            var start = Request.Form.GetValues("start").FirstOrDefault();
            var length = Request.Form.GetValues("length").FirstOrDefault();
            var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
            var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
            var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();

            pageSize = length != null ? Convert.ToInt32(length) : 0;
            skip = start != null ? Convert.ToInt32(start) : 0;
            recordsTotal = 0;

            IQueryable<ConsultaViewModel> query = (from v in context.Visitante
                                                   select new ConsultaViewModel
                                                   {
                                                       Apellido = v.Apellido,
                                                       CodigoIngreso = v.CodigoIngreso,
                                                       FechaIngreso = v.FechaIngreso,
                                                       Identificacion = v.Identificacion,
                                                       Nombre = v.Nombre,
                                                       Organizacion = v.Organizacion,
                                                       Icono = v.Identificacion
                                                   });
            if (searchValue != "")
                query = query.Where(d => d.Nombre.Contains(searchValue) || d.Apellido.Contains(searchValue) || d.CodigoIngreso.ToString().Contains(searchValue) || d.Organizacion.Contains(searchValue) || d.Identificacion.Contains(searchValue));
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                query = query.OrderBy(sortColumn + " " + sortColumnDir);
            }
            recordsTotal = query.Count();
            list = query.Skip(skip).Take(pageSize).ToList();

            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = list });
        }

        public async Task<JsonResult> GetVisitante(string idVisitante = "0")
        {
            //Crear
            if (idVisitante == "0")
            {
                var v = new Visitante
                {
                    Identificacion = "0"
                };
                return Json(new { visitante = v });
            }
            else
            {
                var visitante = await context.Visitante.Where(x=> x.Identificacion == idVisitante).FirstOrDefaultAsync();
                return Json(new { visitante = visitante });
            }

        }

        [HttpPost]
        public async Task<JsonResult> EliminarVisitante(string identificacion)
        {
            try
            {
                var visitExiste = await context.Visitante.Where(x=> x.Identificacion == identificacion).FirstOrDefaultAsync();
                if (visitExiste == null)
                    return Json(new { mensaje = "El producto no existe" });
                context.Visitante.Remove(visitExiste);
                await context.SaveChangesAsync();
                return Json(new { mensaje = "Registro Eliminado Exitosamente!" });
            }
            catch (Exception e)
            {
                return Json(new { mensaje = "Error al intentar eliminar el registro : " + e });
            }
        }

        [HttpPost]
        public async Task<JsonResult> GuardarVisitante(string identificacion, string nombre, string apellido ,string organizacion , DateTime fechaIngreso, string id, int codigoIngreso = 0 )
        {
            if (nombre == null || identificacion == null || codigoIngreso == 0 ||apellido == null || fechaIngreso == null || organizacion == null)
            {
                return Json(new { mensaje = "Debe diligenciar todos los campos" });
            }
            if (Convert.ToInt32(id) == 0)
            {
                try
                {
                    var visitanteExiste = await context.Visitante.Where(x => x.Identificacion == identificacion).AnyAsync();
                    if (visitanteExiste)
                        return Json(new { mensaje = "Ya existe el registro en la base de datos" });

                    var visitante = new Visitante
                    {
                        Identificacion = identificacion,
                        Apellido = apellido,
                        CodigoIngreso = codigoIngreso,
                        FechaIngreso = fechaIngreso.ToString().Substring(0,10),
                        Nombre = nombre,
                        Organizacion = organizacion
                    };
                    context.Visitante.Add(visitante);
                    await context.SaveChangesAsync();

                    return Json(new { mensaje = "Registro creado exitosamente" });
                }
                catch (Exception e)
                {
                    return Json(new { mensaje = "Error al crear el registro : " + e });
                }
            }
            else
            {
                try
                {
                    var visitante = await context.Visitante.Where(x=> x.Identificacion == identificacion).FirstOrDefaultAsync();
                    if (visitante != null)
                    {
                        visitante.Identificacion = identificacion;
                        visitante.Nombre = nombre;
                        visitante.Organizacion= organizacion;
                        visitante.CodigoIngreso= codigoIngreso;
                        visitante.Apellido= apellido;
                        visitante.FechaIngreso= fechaIngreso.ToString().Substring(0,10);
                        await context.SaveChangesAsync();
                        return Json(new { mensaje = "Registro editado exitosamente" });
                    }
                    else
                    {
                        return Json(new { mensaje = "Registro no existe" });
                    }
                }
                catch (Exception e)
                {
                    return Json(new { mensaje = "Error al crear el registro : " });
                }
            }
        }
    }
}