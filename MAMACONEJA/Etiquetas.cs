using FirebirdSql.Data.FirebirdClient;
using System;
using System.Windows.Forms;

namespace MAMACONEJA
{
    public partial class Etiquetas : Form
    {
        //-----------------------------------VARIABLES GLOBALES---------------------------------------------
        public static FirebirdClientFactory cmd;
        public double precio = 1;
        public string gramage = "KG.";
        public int opcion = 0;
        public int opcionGeneral = 1;
        public Etiquetas()
        {
            InitializeComponent();
        }

        //-----------------------------------METODOS GENERALES DEL FORM----------------------------------------
        private void Etiquetas_Load(object sender, EventArgs e)
        {
            //cbRol.SelectedIndex = 0;
            dtmInicio.Value = DateTime.Now.AddDays(-1);
            dtmInicio.CustomFormat = "dd.MM.yyyy 00:00:00";
            dtmFinal.Value = DateTime.Now;
            dtmFinal.CustomFormat = "dd.MM.yyyy 23:59:59";
            lbFecha.Text = "Del día " + dtmInicio.Text + " al " + dtmFinal.Text;
            //ELIMINA PESTANA GRAMAGE



            tabpGramage.Parent = null;
            Metodos.CargarArticulosComboBox(cbArticuloIndividual);
            Metodos.CargarArticulosComboBox(cbArticuloMY);
            //METODO PARA PESTANA INHABILITADA DE GRAMAGE
            //Metodos.CargarArticulosGramageComboBox(cbArticuloGramage);
        }
        private void toolStripLimpiar_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabcEtiquetas.SelectedTab == tabpGeneral)
                {
                    dtvEtiquetas.Rows.Clear();
                    Metodos.TotalArticulos(lbTotalArt, dtvEtiquetas);
                }
                else if (tabcEtiquetas.SelectedTab == tabpEtiquetas)
                {
                    dtvEtiquetasIndividual.Rows.Clear();
                    Metodos.TotalArticulos(lbTotalArtIndividual, dtvEtiquetasIndividual);
                }
                else if (tabcEtiquetas.SelectedTab == tabpGramage)
                {
                    dtvEtiquetasGramage.Rows.Clear();
                    Metodos.TotalArticulos(lbTotalArtGramage, dtvEtiquetasGramage);
                }
                else if (tabcEtiquetas.SelectedTab == tabpMYMDY)
                {
                    dtvEtiquetaMY.Rows.Clear();
                    Metodos.TotalArticulos(lbTotalArtMY, dtvEtiquetaMY);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void pasarDatosEtiquetasIndividual()
        {
            ReporteGeneralVoucher formEtiquetasGeneral = new ReporteGeneralVoucher();
            for (int i = 0; i < dtvEtiquetasIndividual.Rows.Count; i++)
            {
                DatosEtiquetas claseDatosetiquetas = new DatosEtiquetas
                {
                    Clave_Articulo = (string)dtvEtiquetasIndividual.Rows[i].Cells[0].Value,
                    Producto = (string)dtvEtiquetasIndividual.Rows[i].Cells[1].Value,
                    Precio = (double)Convert.ToDouble(dtvEtiquetasIndividual.Rows[i].Cells[2].Value),
                    Uv = (string)dtvEtiquetasIndividual.Rows[i].Cells[3].Value,
                };
                formEtiquetasGeneral.listDatosEtiquetas.Add(claseDatosetiquetas);
            }
            formEtiquetasGeneral.opcion = opcion;
            formEtiquetasGeneral.Show();
        }
        public void pasarDatosEtiquetasGramage()
        {
            ReporteGeneralVoucher formEtiquetasGeneral = new ReporteGeneralVoucher();
            for (int i = 0; i < dtvEtiquetasGramage.Rows.Count; i++)
            {
                DatosEtiquetas claseDatosetiquetas = new DatosEtiquetas
                {
                    Clave_Articulo = (string)dtvEtiquetasGramage.Rows[i].Cells[0].Value,
                    Producto = (string)dtvEtiquetasGramage.Rows[i].Cells[1].Value,
                    Precio = (double)Convert.ToDouble(dtvEtiquetasGramage.Rows[i].Cells[2].Value),
                    Uv = (string)dtvEtiquetasGramage.Rows[i].Cells[3].Value,
                };
                formEtiquetasGeneral.listDatosEtiquetas.Add(claseDatosetiquetas);
            }
            formEtiquetasGeneral.opcion = opcion;
            formEtiquetasGeneral.Show();
        }
        public void pasarDatosEtiquetasMY()
        {
            ReporteMYVoucher formEtiquetasMY = new ReporteMYVoucher();
            for (int i = 0; i < dtvEtiquetaMY.Rows.Count; i++)
            {
                DatosEtiquetas claseDatosetiquetas = new DatosEtiquetas
                {
                    Clave_Articulo = (string)dtvEtiquetaMY.Rows[i].Cells[0].Value,
                    Producto = (string)dtvEtiquetaMY.Rows[i].Cells[1].Value,
                    Precio = (double)Convert.ToDouble(dtvEtiquetaMY.Rows[i].Cells[2].Value),
                    UnidadLista = (double)Convert.ToDouble(dtvEtiquetaMY.Rows[i].Cells[3].Value),
                    PrecioMayoreo = (double)Convert.ToDouble(dtvEtiquetaMY.Rows[i].Cells[4].Value),
                    UnidadMayoreo = (double)Convert.ToDouble(dtvEtiquetaMY.Rows[i].Cells[5].Value),
                    PrecioMedMayoreo = (double)Convert.ToDouble(dtvEtiquetaMY.Rows[i].Cells[6].Value),
                    UnidadMedMayoreo = (double)Convert.ToDouble(dtvEtiquetaMY.Rows[i].Cells[7].Value),
                    Uv = (string)dtvEtiquetaMY.Rows[i].Cells[8].Value,
                };
                formEtiquetasMY.opcion = opcion;
                formEtiquetasMY.listDatosEtiquetas.Add(claseDatosetiquetas);
            }
            formEtiquetasMY.Show();
        }
        private void imprimirToolStripButton_Click(object sender, EventArgs e)
        {
            if (tabcEtiquetas.SelectedTab == tabpGeneral)
            {
                if (dtvEtiquetas.Rows.Count == 0)
                {
                    MessageBox.Show("Favor de agregar artículos.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    ReporteGeneralVoucher formEtiquetasGeneral = new ReporteGeneralVoucher();
                    for (int i = 0; i < dtvEtiquetas.Rows.Count; i++)
                    {
                        DatosEtiquetas claseDatosetiquetas = new DatosEtiquetas
                        {
                            Articulo_id = (int)dtvEtiquetas.Rows[i].Cells[0].Value,
                            Clave_Articulo = (string)dtvEtiquetas.Rows[i].Cells[1].Value,
                            Producto = (string)dtvEtiquetas.Rows[i].Cells[2].Value,
                            Linea = (string)dtvEtiquetas.Rows[i].Cells[3].Value,
                            Precio = (double)Convert.ToDouble(dtvEtiquetas.Rows[i].Cells[4].Value),
                            Uv = (string)dtvEtiquetas.Rows[i].Cells[5].Value,
                            FechaMod = (DateTime)dtvEtiquetas.Rows[i].Cells[6].Value
                        };
                        formEtiquetasGeneral.listDatosEtiquetas.Add(claseDatosetiquetas);
                    }
                    formEtiquetasGeneral.opcion = opcionGeneral;
                    formEtiquetasGeneral.Show();
                }
            }
            if (tabcEtiquetas.SelectedTab == tabpEtiquetas)
            {
                if (dtvEtiquetasIndividual.Rows.Count == 0)
                {
                    MessageBox.Show("Favor de agregar artículos.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TamanoImpresion tamanoimpresion = new TamanoImpresion();
                    tamanoimpresion.ShowDialog();
                    opcion = tamanoimpresion.ParametroRetorno;
                    if (opcion == 1)
                    {
                        pasarDatosEtiquetasIndividual();
                    }
                    else if (opcion == 2)
                    {
                        pasarDatosEtiquetasIndividual();
                    }
                    else if (opcion == 3)
                    {
                        pasarDatosEtiquetasIndividual();
                    }
                    else
                    {
                    }
                }
            }
            if (tabcEtiquetas.SelectedTab == tabpGramage)
            {
                if (dtvEtiquetasGramage.Rows.Count == 0)
                {
                    MessageBox.Show("Favor de agregar artículos.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TamanoImpresion tamanoimpresion = new TamanoImpresion();
                    tamanoimpresion.ShowDialog();
                    opcion = tamanoimpresion.ParametroRetorno;
                    if (opcion == 1)
                    {
                        pasarDatosEtiquetasGramage();
                    }
                    else if (opcion == 2)
                    {
                        pasarDatosEtiquetasGramage();
                    }
                    else if (opcion == 3)
                    {
                        pasarDatosEtiquetasGramage();
                    }
                    else if (opcion == 4)
                    {
                        pasarDatosEtiquetasGramage();
                    }
                    else
                    {
                    }
                }
            }
            if (tabcEtiquetas.SelectedTab == tabpMYMDY)
            {
                if (dtvEtiquetaMY.Rows.Count == 0)
                {
                    MessageBox.Show("Favor de agregar artículos.", "Error al imprimir", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    TamanoImpresionMY tamanoimpresionMY = new TamanoImpresionMY();
                    tamanoimpresionMY.ShowDialog();
                    opcion = tamanoimpresionMY.ParametroRetorno;
                    if (opcion == 1)
                    {
                        pasarDatosEtiquetasMY();
                    }
                    else if (opcion == 3)
                    {
                        pasarDatosEtiquetasMY();
                    }
                    else if (opcion == 4)
                    {
                        pasarDatosEtiquetasMY();
                    }
                    else
                    {
                    }
                }
            }
        }
        private void Etiquetas_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //---------------------------PESTANA GENERAL---------------------------------------------
        private void btnMostrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtmInicio.Value > dtmFinal.Value)
                {
                    MessageBox.Show("Fecha Inicio mas reciente que fecha Final.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dtvEtiquetas.Rows.Clear();
                    Metodos.CargarTablaPrecios(dtvEtiquetas, dtmInicio, dtmFinal);
                    Metodos.TotalArticulos(lbTotalArt, dtvEtiquetas);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void chkVoucher_Click(object sender, EventArgs e)
        {
            try
            {
                chkVoucherChico.Checked = false;
                chkVoucher.Checked = true;
                opcionGeneral = 4;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void chkVoucherChico_Click(object sender, EventArgs e)
        {
            try
            {
                chkVoucher.Checked = false;
                chkVoucherChico.Checked = true;
                opcionGeneral = 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------PESTANA INDIVIDUAL--------------------------------------
        //ACCIONES DE TECLADO
        private void cbClave_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Metodos.CargarTablaPreciosIndividual(dtvEtiquetasIndividual, cbClaveIndividual.Text);
                    Metodos.TotalArticulos(lbTotalArtIndividual, dtvEtiquetasIndividual);
                    cbClaveIndividual.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbArticulo_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Metodos.AgregarBotonIndividual(cbClaveIndividual, cbArticuloIndividual, dtvEtiquetasIndividual);
                    Metodos.TotalArticulos(lbTotalArtIndividual, dtvEtiquetasIndividual);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // ACCIONES BOTONES
        private void btnAgregarIndividual_Click(object sender, EventArgs e)
        {
            try
            {
                Metodos.AgregarBotonIndividual(cbClaveIndividual, cbArticuloIndividual, dtvEtiquetasIndividual);
                Metodos.TotalArticulos(lbTotalArtIndividual, dtvEtiquetasIndividual);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEliminarIndividual_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtvEtiquetasIndividual.CurrentRow == null)
                {
                    MessageBox.Show("No hay artículos agregados.", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dtvEtiquetasIndividual.Rows.Remove(dtvEtiquetasIndividual.CurrentRow);
                    Metodos.TotalArticulos(lbTotalArtIndividual, dtvEtiquetasIndividual);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbArticuloIndividual_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbArticuloIndividual.SelectedValue.ToString() == "System.Data.DataRowView")
                {
                }
                else
                {
                    String articulo_id = cbArticuloIndividual.SelectedValue.ToString();
                    cbClaveIndividual.Text = Metodos.ObtenerClave(articulo_id, 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-------------------------------PESTANA GRAMAGE----------------------------------------
        //ACCIONES DE TECLADO       
        private void cbArticuloGramage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    Metodos.agregarGramage(cbClaveGramage, cbArticuloGramage, dtvEtiquetasGramage, precio, gramage);
                    Metodos.TotalArticulos(lbTotalArtGramage, dtvEtiquetasGramage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbClaveGramage_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                string articulo_id;
                if (e.KeyCode == Keys.Enter)
                {
                    articulo_id = Metodos.ObtenerArticuloIdKg(cbClaveGramage.Text);
                    if (articulo_id == "")
                    {
                        MessageBox.Show("Favor de escribir un codigo de gramage", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Metodos.CargarTablaPreciosGramage(dtvEtiquetasGramage, cbClaveGramage.Text, precio, gramage);
                        Metodos.TotalArticulos(lbTotalArtGramage, dtvEtiquetasGramage);
                        cbClaveGramage.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //CHECK BOX
        private void chkKG_Click(object sender, EventArgs e)
        {
            try
            {
                chk100gr.Checked = false;
                chk250gr.Checked = false;
                chk500gr.Checked = false;
                chkKG.Checked = true;

                precio = 1;
                gramage = "KG.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void chk500gr_Click(object sender, EventArgs e)
        {
            try
            {
                chk100gr.Checked = false;
                chkKG.Checked = false;
                chk250gr.Checked = false;
                chk500gr.Checked = true;
                precio = 0.5;
                gramage = "500 Grs.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void chk100gr_Click(object sender, EventArgs e)
        {
            try
            {
                chk250gr.Checked = false;
                chkKG.Checked = false;
                chk500gr.Checked = false;
                chk100gr.Checked = true;
                precio = 0.1;
                gramage = "100 Grs.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void chk250gr_Click(object sender, EventArgs e)
        {
            try
            {
                chk100gr.Checked = false;
                chkKG.Checked = false;
                chk500gr.Checked = false;
                chk250gr.Checked = true;
                precio = 0.250;
                gramage = "250 Grs.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //ACCIONES BOTONES
        private void btnEliminarGramage_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtvEtiquetasGramage.CurrentRow == null)
                {
                    MessageBox.Show("No hay artículos agregados", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dtvEtiquetasGramage.Rows.Remove(dtvEtiquetasGramage.CurrentRow);
                    Metodos.TotalArticulos(lbTotalArtGramage, dtvEtiquetasGramage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //CODIGO INHABILITADO PARA NO MOSTRAR PESTANA GRAMAGE
        private void cbArticuloGramage_SelectedValueChanged(object sender, EventArgs e)
        {
            //CODIGO INHABILITADO PARA NO MOSTRAR PESTANA GRAMAGE
            /* try
             {
                 if (cbArticuloGramage.SelectedValue.ToString() == "System.Data.DataRowView")
                 {
                 }
                 else
                 {
                     String articulo_id = cbArticuloGramage.SelectedValue.ToString();
                     cbClaveGramage.Text = Metodos.ObtenerClave(articulo_id, 0);
                 }
             }
             catch (Exception ex)
             {
                 MessageBox.Show(ex.Message);
             }*/
        }
        private void btnAgregarGramage_Click(object sender, EventArgs e)
        {
            try
            {
                Metodos.agregarGramage(cbClaveGramage, cbArticuloGramage, dtvEtiquetasGramage, precio, gramage);
                Metodos.TotalArticulos(lbTotalArtGramage, dtvEtiquetasGramage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //--------------------------------PESTANA MAYOREO Y MEDIO MAYOREO---------------------------------------------               
        //ACCIONES AL MOMENTO DE SELECCIONAR UN VALOR DEL COMBO BOX
        private void cbArticuloMY_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (cbArticuloMY.SelectedValue.ToString() == "System.Data.DataRowView")
                {
                }
                else
                {
                    Metodos.CargarComboBoxArticulosMY(cbArticuloMY, txtClaveMY, cbNombreMY, cbNombreMDY);
                    Metodos.CargarComboBoxPrecioLista(cbArticuloMY, txtClaveMY, cbNombreLista);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cbNombreLista_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbNombreLista.SelectedValue.ToString() == "System.Data.DataRowView")
            {

            }
            else
            {
                Metodos.CargarPreciosListas(txtClaveMY, cbNombreLista, txtPrecioLista, txtUnidadesLista, txtUVLista);
            }
        }
        private void cbNombreMY_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbNombreMY.SelectedValue.ToString() == "System.Data.DataRowView")
            {

            }
            else
            {
                Metodos.CargarPreciosListas(txtClaveMY, cbNombreMY, txtPrecioMayoreo, txtUnidadesMayoreo, txtUVMayoreo);
            }
        }
        private void cbNombreMDY_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbNombreMDY.SelectedValue.ToString() == "System.Data.DataRowView")
            {
            }
            else
            {
                Metodos.CargarPreciosListas(txtClaveMY, cbNombreMDY, txtPrecioMedioMayoreo, txtUnidadesMedMayoreo, txtUVMedMayoreo);
            }
        }
        //ACCIONES DE BOTONES
        private void btnAgregarMY_Click(object sender, EventArgs e)
        {
            String articulo_id;
            String clave_articulo;
            if (txtClaveMY.Text == "" && cbArticuloMY.Text == "")
            {
                MessageBox.Show("Favor de llenar los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtClaveMY.Text == "" && cbArticuloMY.Text != "")
            {
                if (cbArticuloMY.SelectedValue == null)
                {
                    MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    articulo_id = cbArticuloMY.SelectedValue.ToString();
                    txtClaveMY.Text = Metodos.ObtenerClave(articulo_id, 0);
                }
            }
            else if (txtClaveMY.Text != "" && cbArticuloMY.Text != "")
            {
                if (cbArticuloMY.SelectedValue == null)
                {
                    MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    articulo_id = cbArticuloMY.SelectedValue.ToString();
                    clave_articulo = Metodos.ObtenerClave(articulo_id, 0);
                    if (txtClaveMY.Text != clave_articulo)
                    {
                        txtClaveMY.Text = clave_articulo;
                    }
                    else
                    {
                        try
                        {
                            dtvEtiquetaMY.Rows.Add(1);
                            int rowTabla = dtvEtiquetaMY.Rows.Count - 1;
                            dtvEtiquetaMY.Rows[rowTabla].Cells[0].Value = txtClaveMY.Text;
                            dtvEtiquetaMY.Rows[rowTabla].Cells[1].Value = cbArticuloMY.Text;
                            dtvEtiquetaMY.Rows[rowTabla].Cells[2].Value = txtPrecioLista.Text;
                            dtvEtiquetaMY.Rows[rowTabla].Cells[3].Value = txtUnidadesLista.Text;
                            dtvEtiquetaMY.Rows[rowTabla].Cells[4].Value = txtPrecioMayoreo.Text;
                            dtvEtiquetaMY.Rows[rowTabla].Cells[5].Value = txtUnidadesMayoreo.Text;
                            dtvEtiquetaMY.Rows[rowTabla].Cells[6].Value = txtPrecioMedioMayoreo.Text;
                            dtvEtiquetaMY.Rows[rowTabla].Cells[7].Value = txtUnidadesMedMayoreo.Text;
                            dtvEtiquetaMY.Rows[rowTabla].Cells[8].Value = txtUVLista.Text;
                            dtvEtiquetaMY.FirstDisplayedScrollingRowIndex = dtvEtiquetaMY.RowCount - 1;
                            Metodos.TotalArticulos(lbTotalArtMY, dtvEtiquetaMY);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                }
            }
            else if (cbArticuloMY.Text == "" && txtClaveMY.Text != "")
            {
                string cadena = Metodos.ObtenerArticuloId(txtClaveMY.Text);
                if (txtClaveMY.Text == cadena)
                {
                    MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtClaveMY.Text = "";
                }
                else
                {
                    cbArticuloMY.Text = cadena;
                    try
                    {
                        dtvEtiquetaMY.Rows.Add(1);
                        int rowTabla = dtvEtiquetaMY.Rows.Count - 1;
                        dtvEtiquetaMY.Rows[rowTabla].Cells[0].Value = txtClaveMY.Text;
                        dtvEtiquetaMY.Rows[rowTabla].Cells[1].Value = cbArticuloMY.Text;
                        dtvEtiquetaMY.Rows[rowTabla].Cells[2].Value = txtPrecioLista.Text;
                        dtvEtiquetaMY.Rows[rowTabla].Cells[3].Value = txtUnidadesLista.Text;
                        dtvEtiquetaMY.Rows[rowTabla].Cells[4].Value = txtPrecioMayoreo.Text;
                        dtvEtiquetaMY.Rows[rowTabla].Cells[5].Value = txtUnidadesMayoreo.Text;
                        dtvEtiquetaMY.Rows[rowTabla].Cells[6].Value = txtPrecioMedioMayoreo.Text;
                        dtvEtiquetaMY.Rows[rowTabla].Cells[7].Value = txtUnidadesMedMayoreo.Text;
                        dtvEtiquetaMY.Rows[rowTabla].Cells[8].Value = txtUVLista.Text;
                        Metodos.TotalArticulos(lbTotalArtMY, dtvEtiquetaMY);
                        dtvEtiquetaMY.FirstDisplayedScrollingRowIndex = dtvEtiquetaMY.RowCount - 1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }
        private void txtClaveMY_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string cadena = Metodos.ObtenerArticuloId(txtClaveMY.Text);
                    if (txtClaveMY.Text == cadena)
                    {
                        MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtClaveMY.Text = "";
                    }
                    else
                    {
                        cbArticuloMY.Text = cadena;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnEliminarMY_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtvEtiquetaMY.CurrentRow == null)
                {
                    MessageBox.Show("No hay artículos agregados", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    dtvEtiquetaMY.Rows.Remove(dtvEtiquetaMY.CurrentRow);
                    Metodos.TotalArticulos(lbTotalArtMY, dtvEtiquetaMY);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //-----------------------------------MENSAJES DE AYUDA ------------------------------------------------
        private void dtmInicio_MouseHover(object sender, EventArgs e)
        {
            ttipFechaInicio.SetToolTip(dtmInicio, "Selecciona la fecha inicial.");
            ttipFechaInicio.ToolTipTitle = "Información";
            ttipFechaInicio.ToolTipIcon = ToolTipIcon.Info;
        }
        private void dtmFinal_MouseHover(object sender, EventArgs e)
        {
            ttipFechaFinal.SetToolTip(dtmFinal, "Selecciona la fecha final.");
            ttipFechaFinal.ToolTipTitle = "Información";
            ttipFechaFinal.ToolTipIcon = ToolTipIcon.Info;
        }
        private void btnMostrar_MouseHover(object sender, EventArgs e)
        {
            ttipBtnMostrar.SetToolTip(btnMostrar, "Muestra los cambios de precios del rango de fecha seleccionada.");
            ttipBtnMostrar.ToolTipTitle = "Información";
            ttipBtnMostrar.ToolTipIcon = ToolTipIcon.Info;
        }
        private void cbClaveIndividual_MouseHover(object sender, EventArgs e)
        {
            ttipCbClaveIndividual.SetToolTip(cbClaveIndividual, "Ingresa la clave del artículo.");
            ttipCbClaveIndividual.ToolTipTitle = "Información";
            ttipCbClaveIndividual.ToolTipIcon = ToolTipIcon.Info;
        }
        private void cbArticuloIndividual_MouseHover(object sender, EventArgs e)
        {
            ttipCbArticuloIndividual.SetToolTip(cbArticuloIndividual, "Ingresa el nombre del artículo.");
            ttipCbArticuloIndividual.ToolTipTitle = "Información";
            ttipCbArticuloIndividual.ToolTipIcon = ToolTipIcon.Info;
        }
        private void btnAgregarIndividual_MouseHover(object sender, EventArgs e)
        {
            ttipBtnAgregarIndividual.SetToolTip(btnAgregarIndividual, "Agrega el artículo a la tabla.");
            ttipBtnAgregarIndividual.ToolTipTitle = "Información";
            ttipBtnAgregarIndividual.ToolTipIcon = ToolTipIcon.Info;
        }
        private void btnEliminarIndividual_MouseHover(object sender, EventArgs e)
        {
            ttipBtnEliminarIndividual.SetToolTip(btnEliminarIndividual, "Elimina el artículo seleccionado de la tabla.");
            ttipBtnEliminarIndividual.ToolTipTitle = "Información";
            ttipBtnEliminarIndividual.ToolTipIcon = ToolTipIcon.Info;
        }
        private void cbClaveGramage_MouseHover(object sender, EventArgs e)
        {
            ttipCbClaveGramage.SetToolTip(cbClaveGramage, "Ingresa la clave del artículo.");
            ttipCbClaveGramage.ToolTipTitle = "Información";
            ttipCbClaveGramage.ToolTipIcon = ToolTipIcon.Info;
        }
        private void cbArticuloGramage_MouseHover(object sender, EventArgs e)
        {
            ttipCbArticuloGramage.SetToolTip(cbArticuloGramage, "Ingresa el nombre del artículo.");
            ttipCbArticuloGramage.ToolTipTitle = "Información";
            ttipCbArticuloGramage.ToolTipIcon = ToolTipIcon.Info;
        }
        private void btnAgregarGramage_MouseHover(object sender, EventArgs e)
        {
            ttipBtnAgregarGramage.SetToolTip(btnAgregarGramage, "Agrega el artículo a la tabla.");
            ttipBtnAgregarGramage.ToolTipTitle = "Información";
            ttipBtnAgregarGramage.ToolTipIcon = ToolTipIcon.Info;
        }
        private void btnEliminarGramage_MouseHover(object sender, EventArgs e)
        {
            ttipBtnEliminarGramage.SetToolTip(btnEliminarGramage, "Elimina el artículo seleccionado de la tabla.");
            ttipBtnEliminarGramage.ToolTipTitle = "Información";
            ttipBtnEliminarGramage.ToolTipIcon = ToolTipIcon.Info;
        }
        private void gbGramage_MouseHover(object sender, EventArgs e)
        {
            ttipGbGramage.SetToolTip(gbGramage, "Selecciona un gramage.");
            ttipGbGramage.ToolTipTitle = "Información";
            ttipGbGramage.ToolTipIcon = ToolTipIcon.Info;
        }
        private void txtClaveMY_MouseHover(object sender, EventArgs e)
        {
            ttiptxtClaveMY.SetToolTip(txtClaveMY, "Ingresa la clave del artículo.");
            ttiptxtClaveMY.ToolTipTitle = "Información";
            ttiptxtClaveMY.ToolTipIcon = ToolTipIcon.Info;
        }
        private void cbArticuloMY_MouseHover(object sender, EventArgs e)
        {
            ttipCbArticuloMY.SetToolTip(cbArticuloMY, "Ingresa el nombre del artículo.");
            ttipCbArticuloMY.ToolTipTitle = "Información";
            ttipCbArticuloMY.ToolTipIcon = ToolTipIcon.Info;
        }
        private void cbNombreMY_MouseHover(object sender, EventArgs e)
        {
            ttipCbNombreMY.SetToolTip(cbNombreMY, "Selecciona un precio de lista.");
            ttipCbNombreMY.ToolTipTitle = "Información";
            ttipCbNombreMY.ToolTipIcon = ToolTipIcon.Info;
        }
        private void cbNombreMDY_MouseHover(object sender, EventArgs e)
        {
            ttipCbNombreMedMY.SetToolTip(cbNombreMDY, "Selecciona un precio de lista.");
            ttipCbNombreMedMY.ToolTipTitle = "Información";
            ttipCbNombreMedMY.ToolTipIcon = ToolTipIcon.Info;
        }
        private void btnAgregarMY_MouseHover(object sender, EventArgs e)
        {
            ttipBtnAgregarMY.SetToolTip(btnAgregarMY, "Agrega el artículo a la tabla.");
            ttipBtnAgregarMY.ToolTipTitle = "Información";
            ttipBtnAgregarMY.ToolTipIcon = ToolTipIcon.Info;
        }
        private void btnEliminarMY_MouseHover(object sender, EventArgs e)
        {
            ttipBtnEliminarMY.SetToolTip(btnEliminarMY, "Elimina el artículo seleccionado de la tabla.");
            ttipBtnEliminarMY.ToolTipTitle = "Información";
            ttipBtnEliminarMY.ToolTipIcon = ToolTipIcon.Info;
        }

        private void cerrarAltF4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void acercaDeEtiquetasMCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Ayuda ayuda = new Ayuda();
            ayuda.ShowDialog();
        }

        private void chkManual_Click(object sender, EventArgs e)
        {
            chkAuto.Checked = false;
            dtmInicio.Enabled = true;
            dtmFinal.Enabled = true;
            cbRol.Enabled = false;
            chkManual.Checked = true;
            dtmInicio.Value = DateTime.Now.AddDays(-1);
            dtmInicio.CustomFormat = "dd.MM.yyyy 00:00:00";
            dtmFinal.Value = DateTime.Now;
            dtmFinal.CustomFormat = "dd.MM.yyyy 23:59:59";
            lbFecha.Text = "Del día " + dtmInicio.Text + " al " + dtmFinal.Text;
        }

        private void chkAuto_Click(object sender, EventArgs e)
        {
            chkManual.Checked = false;
            dtmInicio.Enabled = false;
            dtmFinal.Enabled = false;
            cbRol.Enabled = true;
            cbRol.SelectedIndex = 0;
            Metodos.rolHorario(0, dtmInicio, dtmFinal);
            lbFecha.Text = "Del día " + dtmInicio.Text + " al " + dtmFinal.Text;

        }

        private void cbRol_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRol.SelectedIndex == 0)
            {
                Metodos.rolHorario(0, dtmInicio, dtmFinal);
                lbFecha.Text = "Del día " + dtmInicio.Text + " al " + dtmFinal.Text;

            }
            else if (cbRol.SelectedIndex == 1)
            {
                Metodos.rolHorario(1, dtmInicio, dtmFinal);
                lbFecha.Text = "Del día " + dtmInicio.Text + " al " + dtmFinal.Text;

            }
            else if(cbRol.SelectedIndex == 2)
            {
                Metodos.rolHorario(2, dtmInicio, dtmFinal);
                lbFecha.Text = "Del día " + dtmInicio.Text + " al " + dtmFinal.Text;

            }

        }

        private void dtmInicio_ValueChanged(object sender, EventArgs e)
        {
            lbFecha.Text = "Del día " + dtmInicio.Text + " al " + dtmFinal.Text;
        }

        private void dtmFinal_ValueChanged(object sender, EventArgs e)
        {
            lbFecha.Text = "Del día " + dtmInicio.Text + " al " + dtmFinal.Text;
        }
    }
}

