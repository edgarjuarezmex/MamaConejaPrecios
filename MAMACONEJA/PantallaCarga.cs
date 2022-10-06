using FirebirdSql.Data.FirebirdClient;
using System;
using System.Windows.Forms;

namespace MAMACONEJA
{
    public partial class PantallaCarga : Form
    {
        public PantallaCarga()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string con;
            progressBar1.Increment(4);
            lblPorcentaje.Text = progressBar1.Value.ToString() + "%";
            if (progressBar1.Value == progressBar1.Maximum) 
            {
                timer1.Stop();
                this.Hide();             
                if (Metodos.ValidarConexion(con = "") == "1")
                {
                    Etiquetas etiquetas = new Etiquetas();
                    etiquetas.ShowDialog();
                }
                else {
                    MessageBox.Show("Consulte con el administrador", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Application.Exit();
                }               
            }
        }
    }
}
