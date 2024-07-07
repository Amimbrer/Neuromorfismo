using Microsoft.Extensions.Configuration;
using System.Text;

namespace Neuromorfismo.BackEnd.API
{
    public class DBSettings
    {


        public static string DBConnectionString(IConfiguration config)
        {
            // Bindeamos uno a uno los valores del config a los de nuestra clase de configuracion
            DbConnectionSettings dbConfig = new();
            config.Bind("database", dbConfig);

            // Asignamos los valroes de la configuracion
            StringBuilder sb = new();
            sb.Append($"Server={dbConfig.Server};");
            sb.Append($"Port={dbConfig.Port};");
            sb.Append($"DataBase={dbConfig.DataBase};");
            sb.Append($"User={dbConfig.User};");
            sb.Append($"Password={dbConfig.Password};");
            sb.Append($"AllowUserVariables={dbConfig.AllowUserVariables};");

            // devolvemos el string builder
            return sb.ToString();
        }
    }

    public class DbConnectionSettings
    {
        public string? Server { get; set; }
        public int Port { get; set; }
        public string? DataBase { get; set; }
        public string? User { get; set; }
        public string? Password { get; set; }
        public bool AllowUserVariables { get; set; }

    }
}