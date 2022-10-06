using FirebirdSql.Data.FirebirdClient;
using System;
using System.Windows.Forms;

namespace MAMACONEJA
{
    public static class Conexion
    {
        public static readonly string ConnectionString;
        static Conexion()
        {
            try
            {
                var parametros_conexion = new FbConnectionStringBuilder
                {
                    DataSource = "192.168.1.100",
                    Port = 3050,
                    Database = @"D:\Microsip datos\MAMA CONEJA 2013.FDB",
                    Dialect = 3,
                    Role = "USUARIO_MICROSIP",
                    UserID = "SYSDBA",
                    Password = "ALSANCHEZ",                  
                    Charset = "NONE",
                    ServerType = FbServerType.Default                    
                };
                ConnectionString = parametros_conexion.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
