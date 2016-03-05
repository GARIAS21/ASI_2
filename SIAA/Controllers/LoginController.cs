using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SIAA.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(CORE_USUARIOS user)
        {
            if (ModelState.IsValid)
            {
                using (atencionCiudadanaEntities db = new atencionCiudadanaEntities())
                {
                    var usr = db.CORE_USUARIOS.Where(u => u.USUARIO.Equals(user.USUARIO) && u.CONTRASENA.Equals(user.CONTRASENA)).FirstOrDefault();
                    if (usr != null)
                    {
                        Session["UserId"] = usr.ID.ToString();
                        Session["UserName"] = usr.USUARIO.ToString();
                        return RedirectToAction("AfterLogin");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Usuario o contraseña invalida, o usuario inactivo");
                    }
                }               
            }
            return View(user);
        }

        public ActionResult AfterLogin()
        {
            if (Session["UserId"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
    }
}