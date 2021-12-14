using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCompra.Models.Auth
{
	public class User
	{
		public string Nombre { get; set; }
		public string Apellido1 { get; set; }
		public string Apellido2 { get; set; }
		public string Password { get; set; }
		public string Cedula { get; set; }
		public string Correo { get; set; }
		public string Estado { get; set; }
		public int IdRole { get; set; }
		public int IdManager{ get; set; }

	}
}