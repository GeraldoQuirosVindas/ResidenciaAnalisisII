using SistemaCompra.Models.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCompra.Interface
{
	public interface IAuth
	{
		Task<string> CreateUser(User user);
	}
}
