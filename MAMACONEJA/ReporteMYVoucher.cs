using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MAMACONEJA
{
    public partial class ReporteMYVoucher : Form
    {
        public List<DatosEtiquetas> listDatosEtiquetas = new List<DatosEtiquetas>();
        public int opcion = 0;

        public ReporteMYVoucher()
        {
            InitializeComponent();
        }
        private void EtiquetasMYVoucher_Load(object sender, EventArgs e)
        {
            try
            {
                this.MaximumSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
                this.WindowState = FormWindowState.Maximized;
                //ReportParameter[] parametros = new ReportParameter[1];
                // parametros[0] = new ReportParameter("ParametroPrecio","dato");
                //this.reportViewer1.LocalReport.SetParameters(parametros);
                // dataGridView1.DataSource = listDatosEtiquetas;
                if (opcion == 1)
                {
                    this.rvEtiquetasMYVoucher.LocalReport.ReportEmbeddedResource = "MAMACONEJA.REPORTE_ETIQUETAS.ReporteEtiquetasMYVoucher.rdlc";
                }
                else if (opcion == 2)
                {
                    this.rvEtiquetasMYVoucher.LocalReport.ReportEmbeddedResource = "MAMACONEJA.REPORTE_ETIQUETAS.ReporteEtiquetasMYVoucherChico.rdlc";
                }
                else if (opcion == 3)
                {
                    this.rvEtiquetasMYVoucher.LocalReport.ReportEmbeddedResource = "MAMACONEJA.REPORTE_ETIQUETAS.ReporteEtiquetasMYCarta.rdlc";
                }
                else if (opcion == 4)
                {
                    this.rvEtiquetasMYVoucher.LocalReport.ReportEmbeddedResource = "MAMACONEJA.REPORTE_ETIQUETAS.ReporteEtiquetasMYMica.rdlc";
                }
                else
                {
                }
                rvEtiquetasMYVoucher.LocalReport.DataSources.Clear();
                rvEtiquetasMYVoucher.LocalReport.DataSources.Add(new ReportDataSource("dsEtiquetasMY", listDatosEtiquetas));
                this.rvEtiquetasMYVoucher.SetDisplayMode(DisplayMode.PrintLayout);
                this.rvEtiquetasMYVoucher.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   
    }
}
