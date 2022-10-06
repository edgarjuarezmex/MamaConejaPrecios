
namespace MAMACONEJA
{
    partial class ReporteMYVoucher
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReporteMYVoucher));
            this.rvEtiquetasMYVoucher = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rvEtiquetasMYVoucher
            // 
            this.rvEtiquetasMYVoucher.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rvEtiquetasMYVoucher.LocalReport.ReportEmbeddedResource = "MAMACONEJA.ReporteEtiquetasMYVoucher.rdlc";
            this.rvEtiquetasMYVoucher.Location = new System.Drawing.Point(-2, 0);
            this.rvEtiquetasMYVoucher.Name = "rvEtiquetasMYVoucher";
            this.rvEtiquetasMYVoucher.ServerReport.BearerToken = null;
            this.rvEtiquetasMYVoucher.ShowBackButton = false;
            this.rvEtiquetasMYVoucher.ShowCredentialPrompts = false;
            this.rvEtiquetasMYVoucher.ShowDocumentMapButton = false;
            this.rvEtiquetasMYVoucher.ShowExportButton = false;
            this.rvEtiquetasMYVoucher.ShowFindControls = false;
            this.rvEtiquetasMYVoucher.ShowParameterPrompts = false;
            this.rvEtiquetasMYVoucher.ShowRefreshButton = false;
            this.rvEtiquetasMYVoucher.ShowStopButton = false;
            this.rvEtiquetasMYVoucher.Size = new System.Drawing.Size(802, 451);
            this.rvEtiquetasMYVoucher.TabIndex = 0;
            // 
            // ReporteMYVoucher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rvEtiquetasMYVoucher);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ReporteMYVoucher";
            this.Text = "Reporte";
            this.Load += new System.EventHandler(this.EtiquetasMYVoucher_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvEtiquetasMYVoucher;
    }
}