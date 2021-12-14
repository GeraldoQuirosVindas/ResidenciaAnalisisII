using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCompra.Models.Auth
{
	public class SignIn
	{

		public int IdUser { get; set; }
		public string Password { get; set; }
		public string Correo { get; set; }
		public string Role { get; set; }

	}
}