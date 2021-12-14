using Mailjet.Client;
using SistemaCompra.Data.ApiEmail;
using SistemaCompra.Data.QuerySQL.Purchase;
using SistemaCompra.Data.QuerySQL.Usuarios;
using SistemaCompra.Models.Purchase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaCompra.Controllers
{
    public class PurchaseController : Controller
    {

        PurchaseSql _purchaseSql = new PurchaseSql();
        UsuariosSql _usuariosSql = new UsuariosSql();

        EmailProvider _emailProvider = new EmailProvider();

        // GET: purchaseRequest
        public static int NumOrden = 0;

        public async Task<ActionResult> CreatePurchase()
        {

            try
            {

                var numMax = await _purchaseSql.GetLastNumOrden();
                NumOrden = numMax + 1;
                ViewBag.NumOrden = NumOrden;

                return View();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }

        }

        [HttpPost]
        public async Task<ActionResult> CreatePurchase(PurchaseModel purchase)
        {

            try
            {
                int IdUser = (int)System.Web.HttpContext.Current.Session["IdUser"];

                purchase.NumOrden = NumOrden;
                purchase.idcomprador = IdUser;


                var modelManager = await _usuariosSql.GetManagersByCorreo(IdUser);

               

                var responseEmail = await _emailProvider.EmailMaijet(modelManager.Correo);

				if (responseEmail.IsSuccessStatusCode)
				{
                    var res = await _purchaseSql.CreatePurchase(purchase);
                    return RedirectToAction("GetPurchaseById", "Purchase");

                }
                else
				{
                    ViewBag.MessageReponse = "Ha ocurrido un error, intente de nuevo.";
                    return View();

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }
        }

        public async Task<ActionResult> GetPurchaseById()
        {
            try
            {
                int IdUser = (int)System.Web.HttpContext.Current.Session["IdUser"];

                List<PurchaseModel> listModel = new List<PurchaseModel>();

                var resList = await _purchaseSql.GetPurchaseById(IdUser);

                return View(resList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }

        }


        public async Task<ActionResult> GetPurchaseManagerById()
        {
            try
            {
                int IdUser = (int)System.Web.HttpContext.Current.Session["IdUser"];

                List<PurchaseManagerModel> listModel = new List<PurchaseManagerModel>();

                var resList = await _purchaseSql.GetPurchaseByManager(IdUser);

                return View(resList);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return View();
            }

        }


        // GET: Test/Edit/5
        public async Task<ActionResult> Edit(int id)
        {

            List<SelectListItem> items = new List<SelectListItem>();
            List<SelectListItem> selectListsManager = new List<SelectListItem>();


            items.Add(new SelectListItem { Text = "Aprobar", Value = "Aprobar" });

            items.Add(new SelectListItem { Text = "Rechazar", Value = "Rechazar" });

            ViewBag.Estados = items;


            var model = await _purchaseSql.GetPurchaseByIdOrder(id);

            return View(model);
        }

        // POST: Test/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, PurchaseModel model)
        {
            try
            {

                var user = await _usuariosSql.GetPurchaseByCorreo(model.idrequisicion);

                string estadoEmail = "";

				if (model.Estado.Equals("Aprobar"))
				{
;                    estadoEmail = "Aprobada";
				}
				else
				{
                    estadoEmail = "Rechazada";

                }

                var mailMaijet = await _emailProvider.EmailUpdate(user.Correo, estadoEmail, model.NumOrden.ToString());


				if (mailMaijet.IsSuccessStatusCode)
				{
                    var update = await _purchaseSql.UpdatePurchaseByIdOrder(id, model);


                    return RedirectToAction("GetPurchaseManagerById", "Purchase");
                }
                else
                {
                    ViewBag.MessageReponse = "Ha ocurrido un error, intente de nuevo.";
                    return View();

                }
            }
            catch
            {
                return View();
            }
        }



    }
}