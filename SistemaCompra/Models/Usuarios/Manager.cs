using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCompra.Models.Usuarios
{
	public class Manager
	{
		public string Nombre { get; set; }
		public string Role { get; set; }
		public int IdManager { get; set; }

		public string Correo { get; set; }
	}
}