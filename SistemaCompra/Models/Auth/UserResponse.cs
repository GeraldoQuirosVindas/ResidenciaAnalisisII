using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaCompra.Models.Auth
{
	public class UserResponse
	{

		public string Password { get; set; }
		public string Correo { get; set; }
		public string Role { get; set; }
		public string Message { get; set; }
		public Boolean IsSuccess { get; set; }

	}
}