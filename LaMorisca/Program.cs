using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaMorisca
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmPrincipal());
        }
        public static string conexion = "Data Source=mssql5.gear.host; Initial Catalog=lamorisca;User ID=lamorisca;Password=Re7oyRiW!T_U	;";
        internal static void regresar(Form form)
        {
            form.Close();
            new FrmPrincipal().Show();
        }

        internal static void retornarError(String Message, String Titulo)
        {
            MessageBox.Show(Message, Titulo, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            
        }
    }
}
