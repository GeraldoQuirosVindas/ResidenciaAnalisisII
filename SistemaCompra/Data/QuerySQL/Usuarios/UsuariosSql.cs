using SistemaCompra.Models.Auth;
using SistemaCompra.Models.Usuarios;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SistemaCompra.Data.QuerySQL.Usuarios
{
	public class UsuariosSql
	{
		ProviderSQL _conn = new ProviderSQL();

		public async Task<List<Manager>> GetManagers()
		{

			string queryString = "SELECT dbo.tbl_usuario.nombre, dbo.tbl_usuario.idusuario, dbo.tbl_rol.nombrerol FROM  dbo.tbl_rol INNER JOIN dbo.tbl_usuariorol ON dbo.tbl_rol.idrol = dbo.tbl_usuariorol.idrol INNER JOIN dbo.tbl_usuario ON dbo.tbl_usuariorol.idusuario = dbo.tbl_usuario.idusuario where dbo.tbl_usuariorol.idrol = 2";

			_conn.Open();

			SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);

			SqlDataReader dr = await cmd.ExecuteReaderAsync();

			List<Manager> ListManager = new List<Manager>();

			while (dr.Read())
			{

				Manager ManagerModel = new Manager
				{
					Nombre = (string)dr[0],
					IdManager = (int)dr[1],
					Role = (string)dr[2]
				};


				ListManager.Add(ManagerModel);

			}


			_conn.Close();

			return ListManager;

		}

		public async Task<Manager> GetManagersByCorreo(int IdUsuario)
		{

			string queryString = $"SELECT correo FROM tbl_usuario WHERE (idusuario in (select idmanager from tbl_usuario WHERE idusuario = {IdUsuario}))";

			_conn.Open();

			SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);

			SqlDataReader dr = await cmd.ExecuteReaderAsync();

			Manager ManagerModel = new Manager();

			while (dr.Read())
			{

				ManagerModel.Correo = (string)dr[0];

			}


			_conn.Close();

			return ManagerModel;

		}

		public async Task<User> GetPurchaseByCorreo(int IdPurchase)
		{

			string queryString = $"select correo from tbl_usuario u inner join tbl_requisicion c on u.idusuario = c.idcomprador where c.idrequisicion = {IdPurchase}";

			_conn.Open();

			SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);

			SqlDataReader dr = await cmd.ExecuteReaderAsync();

			User User = new User();

			while (dr.Read())
			{

				User.Correo = (string)dr[0];

			}


			_conn.Close();

			return User;

		}


	}
}