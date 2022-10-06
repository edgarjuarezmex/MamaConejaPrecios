
namespace MAMACONEJA
{
    partial class ReporteGeneralVoucher
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteGeneralVoucher));
            this.DatosEtiquetasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.rvEtiquetasGeneral = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.DatosEtiquetasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // DatosEtiquetasBindingSource
            // 
            this.DatosEtiquetasBindingSource.DataSource = typeof(MAMACONEJA.DatosEtiquetas);
            // 
            // rvEtiquetasGeneral
            // 
            this.rvEtiquetasGeneral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvEtiquetasGeneral.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.rvEtiquetasGeneral.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.rvEtiquetasGeneral.LocalReport.ReportEmbeddedResource = "MAMACONEJA.ReporteEtiquetasGeneralVoucher.rdlc";
            this.rvEtiquetasGeneral.Location = new System.Drawing.Point(3, 2);
            this.rvEtiquetasGeneral.Name = "rvEtiquetasGeneral";
            this.rvEtiquetasGeneral.ServerReport.BearerToken = null;
            this.rvEtiquetasGeneral.ShowBackButton = false;
            this.rvEtiquetasGeneral.ShowCredentialPrompts = false;
            this.rvEtiquetasGeneral.ShowDocumentMapButton = false;
            this.rvEtiquetasGeneral.ShowExportButton = false;
            this.rvEtiquetasGeneral.ShowFindControls = false;
            this.rvEtiquetasGeneral.ShowParameterPrompts = false;
            this.rvEtiquetasGeneral.ShowRefreshButton = false;
            this.rvEtiquetasGeneral.ShowStopButton = false;
            this.rvEtiquetasGeneral.Size = new System.Drawing.Size(799, 448);
            this.rvEtiquetasGeneral.TabIndex = 0;
            // 
            // ReporteGeneralVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rvEtiquetasGeneral);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteGeneralVoucher";
            this.Text = "Reporte";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DatosEtiquetasBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvEtiquetasGeneral;
        private System.Windows.Forms.BindingSource DatosEtiquetasBindingSource;
    }
}