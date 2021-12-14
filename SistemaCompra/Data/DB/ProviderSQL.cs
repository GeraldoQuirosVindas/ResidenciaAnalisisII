using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SistemaCompra.Data
{
	public class ProviderSQL
	{
		public readonly string _connectionString = string.Empty;
		public SqlConnection conectarbd = new SqlConnection();

        public ProviderSQL()
        {
			string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

			conectarbd.ConnectionString = conn;
		}

        public void Open()
        {
            try
            {
                conectarbd.Open();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void Close()
        {
            try
            {
                conectarbd.Close();
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}