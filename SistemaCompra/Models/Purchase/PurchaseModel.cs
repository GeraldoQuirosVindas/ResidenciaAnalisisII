using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SistemaCompra.Models.Purchase
{
	public class PurchaseModel

	{

		//[DisplayName("Numero articulo")]
		public int idrequisicion { get; set; }
		public int idcomprador { get; set; }
		public int NumOrden { get; set; }
		public int NumArticulo { get; set; }
		public  string NomArticulo { get; set; }
		public decimal Precio { get; set; }
		public int Cantidad { get; set; }
		public string Prioridad { get; set; }
		public string Comentario { get; set; }

		//para saber si la solicitud de compra esta Aprobada , rechazada o en espera
		public string Estado { get; set; }
	}
}