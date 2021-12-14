using SistemaCompra.Models.Auth;
using SistemaCompra.Models.Purchase;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SistemaCompra.Data.QuerySQL.Purchase
{
	public class PurchaseSql
	{

		ProviderSQL _conn = new ProviderSQL();

		#region CreatePurchase

		public async Task<string> CreatePurchase(PurchaseModel purchase)
		{

			purchase.Estado = "En proceso";

			string queryString = $"INSERT INTO tbl_requisicion (idcomprador,numOrden,nombreArticulo, numarticulo, precio,cantidad,estado,prioridad,comentario) VALUES('{purchase.idcomprador}','{purchase.NumOrden}','{purchase.NomArticulo}','{purchase.NumArticulo}','{purchase.Precio}','{purchase.Cantidad}','{purchase.Estado}','{purchase.Prioridad}','{purchase.Comentario}');";


			_conn.Open();

			SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);

			int i = await cmd.ExecuteNonQueryAsync();

			_conn.Close();

			if (i >= 1)
			{
				return "1";
				

			}
			else
			{
				return "0";

			}

		}

		#endregion

		public async Task<int> GetLastNumOrden()
		{

			string queryString = "SELECT ISNULL(Max(IDENT_CURRENT('tbl_requisicion')), 0 ) FROM tbl_requisicion";

			_conn.Open();

			SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);

			SqlDataReader dr = await cmd.ExecuteReaderAsync();

			decimal orden = 0; 

			while (dr.Read())
			{
				orden = (decimal)dr[0];
		

			}

			int numOrden = Convert.ToInt32(orden);

			_conn.Close();

			return numOrden;

		}

		public async Task<List<PurchaseModel>> GetPurchaseById(int UserId)
		{

			string queryString = $"Select *  from tbl_requisicion where  idcomprador = {UserId}";

			_conn.Open();

			SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);

			SqlDataReader dr = await cmd.ExecuteReaderAsync();

			List<PurchaseModel> ListPurchase = new List<PurchaseModel>();

			while (dr.Read())
			{

				PurchaseModel PurchaseModel = new PurchaseModel
				{
					idrequisicion = (int)dr[0],
					idcomprador = (int)dr[1],
					NumOrden = (int)dr[2],
					NomArticulo = (string)dr[3],
					NumArticulo = (int)dr[4],
					Precio = (decimal)dr[5],
					Cantidad = (int)dr[6],
					Estado = (string)dr[7],
					Prioridad = (string)dr[8],
					Comentario = (string)dr[9]
				};


		  	ListPurchase.Add(PurchaseModel);

			}


			_conn.Close();

			return ListPurchase;

		}

		public async Task<List<PurchaseManagerModel>> GetPurchaseByManager(int UserId)
		{

			string queryString = $"SELECT dbo.tbl_usuario.cedula, CONCAT(dbo.tbl_usuario.nombre, ' ',dbo.tbl_usuario.apellido1,' ',dbo.tbl_usuario.apellido2)as nombre, dbo.tbl_requisicion.idrequisicion, dbo.tbl_requisicion.numOrden, dbo.tbl_requisicion.nombreArticulo, dbo.tbl_requisicion.numarticulo, dbo.tbl_requisicion.precio, dbo.tbl_requisicion.cantidad, dbo.tbl_requisicion.estado, dbo.tbl_requisicion.prioridad,dbo.tbl_requisicion.comentario FROM dbo.tbl_rol INNER JOIN dbo.tbl_usuariorol ON dbo.tbl_rol.idrol = dbo.tbl_usuariorol.idrol INNER JOIN dbo.tbl_usuario ON dbo.tbl_usuariorol.idusuario = dbo.tbl_usuario.idusuario INNER JOIN dbo.tbl_requisicion ON dbo.tbl_usuario.idusuario = dbo.tbl_requisicion.idcomprador where tbl_usuario.idmanager = {UserId}";

			_conn.Open();

			SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);

			SqlDataReader dr = await cmd.ExecuteReaderAsync();

			List<PurchaseManagerModel> ListPurchase = new List<PurchaseManagerModel>();

			while (dr.Read())
			{

				PurchaseManagerModel purchaseManager = new PurchaseManagerModel
				{
					Cedula = (string)dr[0],
					Nombre = (string)dr[1],
					Idrequisicion = (int)dr[2],
					NumOrden = (int)dr[3],
					NomArticulo = (string)dr[4],
					NumArticulo = (int)dr[5],
					Precio = (decimal)dr[6],
					Cantidad = (int)dr[7],
					Estado = (string)dr[8],
					Prioridad = (string)dr[9],
					Comentario = (string)dr[10]
				};


				ListPurchase.Add(purchaseManager);

			}


			_conn.Close();

			return ListPurchase;

		}

		public async Task<PurchaseModel> GetPurchaseByIdOrder(int OrderId)
		{

			string queryString = $"select * from  tbl_requisicion  where idrequisicion = {OrderId}";

			_conn.Open();

			SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);

			SqlDataReader dr = await cmd.ExecuteReaderAsync();

			PurchaseModel PurchaseModel = new PurchaseModel();

			while (dr.Read())
			{


				PurchaseModel.idrequisicion = (int)dr[0];
				PurchaseModel.idcomprador = (int)dr[1];
				PurchaseModel. NumOrden = (int)dr[2];
				PurchaseModel.NomArticulo = (string)dr[3];
				PurchaseModel.NumArticulo = (int)dr[4];
				PurchaseModel.Precio = (decimal)dr[5];
				PurchaseModel.Cantidad = (int)dr[6];
				PurchaseModel.Estado = (string)dr[7];
				PurchaseModel.Prioridad = (string)dr[8];
				PurchaseModel.Comentario = (string)dr[9];
				

			}


			_conn.Close();

			return PurchaseModel;

		}

		public async Task<string> UpdatePurchaseByIdOrder(int OrderId,PurchaseModel model)
		{

			if (model.Estado.Equals("Aprobar"))
			{
				model.Estado = "Aprobado";
			}
			else
			{
				model.Estado = "Rechazado";
			}

			string queryString = $"UPDATE tbl_requisicion SET  estado = '{model.Estado}' where idrequisicion = {OrderId}";

			_conn.Open();

			SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);

			int i = await cmd.ExecuteNonQueryAsync();


			_conn.Close();

			if (i >= 1)
			{
				return "1";

			}
			else
			{
				return "0";

			}

		}


	}
}