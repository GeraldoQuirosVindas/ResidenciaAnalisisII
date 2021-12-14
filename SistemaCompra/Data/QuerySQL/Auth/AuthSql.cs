using SistemaCompra.Interface;
using SistemaCompra.Models.Auth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SistemaCompra.Data.QuerySQL.Auth
{
	public class AuthSql
	{

        ProviderSQL _conn = new ProviderSQL();

      
        #region CreateUser
        public async Task<string> CreateUser(User user)
        {
            string queryString = "";

            if (user.IdManager == 0)
			{
                 queryString = $"INSERT INTO tbl_usuario (nombre, apellido1, apellido2,correo,cedula,password,estado,idmanager) VALUES('{user.Nombre}','{user.Apellido1}','{user.Apellido2}','{user.Correo}','{user.Cedula}','{user.Password}','{user.Estado}',NULL); SELECT SCOPE_IDENTITY();";

			}
			else
			{
                queryString = $"INSERT INTO tbl_usuario (nombre, apellido1, apellido2,correo,cedula,password,estado,idmanager) VALUES('{user.Nombre}','{user.Apellido1}','{user.Apellido2}','{user.Correo}','{user.Cedula}','{user.Password}','{user.Estado}',{user.IdManager}); SELECT SCOPE_IDENTITY();";

            }

            //string queryString2 = $"INSERT INTO tbl_usuario (nombre, apellido1, apellido2,correo,cedula,password,estado) VALUES('{user.Nombre}','{user.Apellido1}','{user.Apellido2}','{user.Correo}','{user.Cedula}','{user.Password}','{user.Estado}')";

            _conn.Open();

            SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);

            var id = await cmd.ExecuteScalarAsync().ConfigureAwait(false);

            string queryString2 = $"INSERT INTO tbl_usuariorol (idusuario,idrol) VALUES('{id}','{user.IdRole}')";

            SqlCommand cmd2 = new SqlCommand(queryString2, _conn.conectarbd);

            int i = await cmd2.ExecuteNonQueryAsync();


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


        #region SingIn
        public async Task<SignIn> SingIn(SignIn user)
        {

            string queryString = $"SELECT tbl_usuario.idusuario,tbl_usuario.correo, tbl_usuario.password, tbl_rol.nombrerol FROM tbl_rol INNER JOIN tbl_usuariorol ON tbl_rol.idrol = tbl_usuariorol.idrol INNER JOIN tbl_usuario ON tbl_usuariorol.idusuario = tbl_usuario.idusuario where tbl_usuario.correo = '{user.Correo}'";

            _conn.Open();

            SqlCommand cmd = new SqlCommand(queryString, _conn.conectarbd);
            SqlDataReader dr = await cmd.ExecuteReaderAsync();


            SignIn UserModel = new SignIn();

            while (await dr.ReadAsync())
            {

              UserModel.IdUser = (int)dr[0];
              UserModel.Correo = (string)dr[1];
              UserModel.Password = (string)dr[2];
              UserModel.Role = (string)dr[3];


            }

            _conn.Close();
            return UserModel;
        }
        #endregion

    }
}