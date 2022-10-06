using System;
using System.Windows.Forms;

namespace MAMACONEJA
{
    public partial class TamanoImpresion : Form
    {
        public int ParametroRetorno { get; set; }
        public TamanoImpresion()
        {
            InitializeComponent();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {        
            Etiquetas etiquetas = new Etiquetas();
            if (cbTamano.SelectedIndex == 0) 
            {
                ParametroRetorno = 1;
            }
            if (cbTamano.SelectedIndex == 1)
            {
                ParametroRetorno = 2;
            }
            if (cbTamano.SelectedIndex == 2)
            {
                ParametroRetorno = 3;
            }           
            this.Close();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TamanoImpresion_Load(object sender, EventArgs e)
        {
            cbTamano.SelectedIndex = 0;
        }
    }
}
