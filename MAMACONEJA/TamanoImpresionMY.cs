using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAMACONEJA
{
    public partial class TamanoImpresionMY : Form
    {
        public int ParametroRetorno { get; set; }

        public TamanoImpresionMY()
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
                ParametroRetorno = 3;
            }
            if (cbTamano.SelectedIndex == 2)
            {
                ParametroRetorno = 4;
            }
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TamanoImpresionMY_Load(object sender, EventArgs e)
        {
            cbTamano.SelectedIndex = 0;
        }
    }
}
