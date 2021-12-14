using SistemaCompra.Data.QuerySQL.Auth;
using SistemaCompra.Data.QuerySQL.Usuarios;
using SistemaCompra.Models.Auth;
using SistemaCompra.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using BC = BCrypt.Net.BCrypt;

namespace SistemaCompra.Controllers
{
	public class AuthController : Controller
    {

        AuthSql _authSql = new AuthSql();
        UsuariosSql _usuarioSql = new UsuariosSql();

        public async Task<ActionResult> CreateUser()
        {

            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> selectListsManager = new List<SelectListItem>();

            var model = new List<Manager>();

            items.Add(new SelectListItem { Text = "Aprobador Financiero", Value = "1" });

            items.Add(new SelectListItem { Text = "Aprobador Jefe", Value = "2" });

            items.Add(new SelectListItem { Text = "Comprador", Value = "3" });

            ViewBag.Roles = items;

            model = await _usuarioSql.GetManagers();


			foreach (var item in model)
			{
                selectListsManager.Add(new SelectListItem { Text = item.Nombre, Value = item.IdManager.ToString() });

            }


            ViewBag.Manager = selectListsManager;




            return View();
        }


        [HttpPost]
        public async Task<ActionResult> CreateUser(User User)
        {
			try {


	
                string Newpassword = BC.HashPassword(User.Password);
                User.Password = Newpassword;

                var res = await _authSql.CreateUser(User);

				// TODO: Add update logic here

				return RedirectToAction("Index","Home");

			}
			catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }


        public ActionResult LogOut()
        {
            Session["Role"] = null;
            Session["Email"] = null;
            Session["IdUser"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult SingIn()
        {


            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SingIn(SignIn User)
            
        {
            try
            {

                var res = await _authSql.SingIn(User);

      
                if (res.Correo == null)
				{
                    ViewBag.MessageLogin = "Correo no registrado";
                    return View();
                }

                var flagPass = BC.Verify(User.Password, res.Password);

                if (res.Correo.Equals(User.Correo) && flagPass==true)
				{

                    Session["Role"] = res.Role;
                    Session["Email"] = res.Correo;
                    Session["IdUser"] = res.IdUser;

                    return RedirectToAction("Index", "Home");
				}
				else
				{
                  
                    ViewBag.MessageLogin = "Crendciales incorrectas";
                    return View();
                }
                // TODO: Add update logic here
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }
    }
}
