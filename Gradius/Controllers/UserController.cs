using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Gradius_DataBaseLayer;
using Gradius_Core;

namespace Gradius.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public PartialViewResult UserPage()
        {
            string userId = HttpContext.Session.GetString("User");
            Usuario usuario = new Usuario();
            usuario.Load(userId);
            usuario.LoadApps();
            Models.User.UserPageModel userPageModel = new Models.User.UserPageModel();
            userPageModel.Usuario = usuario;




            return PartialView(userPageModel);
        }

        public PartialViewResult Authentication()
        {
            string email = Request.Form["Email"].ToString();
            string senha = Request.Form["Senha"].ToString();

            Usuario usuario = new Usuario();
            usuario.Auth(email, senha);

            if (usuario.Id != null)
            {
                usuario.LoadApps();
                Models.User.UserPageModel userPageModel = new Models.User.UserPageModel();
                userPageModel.Usuario = usuario;
                HttpContext.Session.SetString("User", usuario.Id);
                return PartialView("~/Views/User/UserPage.cshtml", userPageModel);
            }
            else
            {

                return PartialView("~/Views/Home/Login.cshtml");
            }



        }

        public PartialViewResult Cadastro()
        {
            string name = Request.Form["Name"].ToString();
            string email = Request.Form["Email"].ToString();
            string senha = Request.Form["Senha"].ToString();

            Usuario usuario = new Usuario();
            usuario.Email = email;
            usuario.Senha = senha;
            usuario.Nome = name;

            if (!usuario.Existis())
            {
                usuario.Insert();
                usuario.Auth(email, senha);
                usuario.LoadApps();
                Models.User.UserPageModel userPageModel = new Models.User.UserPageModel();
                userPageModel.Usuario = usuario;
                HttpContext.Session.SetString("User", usuario.Id);
                return PartialView("~/Views/User/UserPage.cshtml", userPageModel);
            }

            else
            {
                return PartialView("~/Views/Home/Cadastro.cshtml");
            }

        }
    }
}