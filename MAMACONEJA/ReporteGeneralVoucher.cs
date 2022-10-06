using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MAMACONEJA
{
    public partial class ReporteGeneralVoucher : Form
    {
        public List<DatosEtiquetas> listDatosEtiquetas = new List<DatosEtiquetas>();
        public int opcion = 0;
        public ReporteGeneralVoucher()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.MaximumSize = SystemInformation.PrimaryMonitorMaximizedWindowSize;
                this.WindowState = FormWindowState.Maximized;
                if (opcion == 1)
                {
                    this.rvEtiquetasGeneral.LocalReport.ReportEmbeddedResource = "MAMACONEJA.REPORTE_ETIQUETAS.ReporteEtiquetasGeneralVoucherChico.rdlc";
                }
                else if (opcion == 2)
                {
                    this.rvEtiquetasGeneral.LocalReport.ReportEmbeddedResource = "MAMACONEJA.REPORTE_ETIQUETAS.ReporteEtiquetasGeneralCarta.rdlc";
                }
                else if (opcion == 3)
                {
                    this.rvEtiquetasGeneral.LocalReport.ReportEmbeddedResource = "MAMACONEJA.REPORTE_ETIQUETAS.ReporteEtiquetasGeneralMica.rdlc";
                }
                else if (opcion == 4)
                {
                    this.rvEtiquetasGeneral.LocalReport.ReportEmbeddedResource = "MAMACONEJA.REPORTE_ETIQUETAS.ReporteEtiquetasGeneralVoucher.rdlc";
                }
                else 
                {
                }
                rvEtiquetasGeneral.LocalReport.DataSources.Clear();
                rvEtiquetasGeneral.LocalReport.DataSources.Add(new ReportDataSource("dsEtiquetas", listDatosEtiquetas));
                this.rvEtiquetasGeneral.SetDisplayMode(DisplayMode.PrintLayout);
                this.rvEtiquetasGeneral.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
