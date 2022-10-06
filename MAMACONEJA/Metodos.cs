using FirebirdSql.Data.FirebirdClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace MAMACONEJA
{
    class Metodos
    {
        public static void CargarArticulosComboBox(ComboBox cbArticulo)
        {
            using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
            {
                try
                {
                    connection.Open();
                    FbCommand cmd = new FbCommand("select a.ARTICULO_ID, A.NOMBRE from articulos a left join lineas_articulos b " +
                        "on b.linea_articulo_id = a.linea_articulo_id where a.estatus = 'A' and(a.nombre not like '%RENTA%') " +
                        "AND(a.nombre not like '%SERVICIOS%') AND(a.nombre not like '%MANIOBRAS%')and b.linea_articulo_id <> 220235 " +
                        "and b.linea_articulo_id <> 1301128 and b.linea_articulo_id <> 1300719 ", connection);
                    FbDataAdapter DA = new FbDataAdapter(cmd);
                    DataTable tablaConsulta = new DataTable();
                    DA.Fill(tablaConsulta);
                    cbArticulo.DataSource = tablaConsulta;
                    cbArticulo.ValueMember = "ARTICULO_ID";
                    cbArticulo.DisplayMember = "NOMBRE";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static void CargarArticulosGramageComboBox(ComboBox cbArticuloGramage)
        {
            using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
            {
                try
                {
                    connection.Open();
                    FbCommand cmd = new FbCommand("select a.ARTICULO_ID, A.NOMBRE from articulos a where a.estatus = 'A'  " +
                       "and A.unidad_venta = 'KG.'", connection);
                    FbDataAdapter DA = new FbDataAdapter(cmd);
                    DataTable tablaConsulta = new DataTable();
                    DA.Fill(tablaConsulta);
                    cbArticuloGramage.DataSource = tablaConsulta;
                    cbArticuloGramage.ValueMember = "ARTICULO_ID";
                    cbArticuloGramage.DisplayMember = "NOMBRE";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static void CargarTablaPrecios(DataGridView Etiquetas, DateTimePicker Inicio, DateTimePicker Final)
        {
            try
            {
                using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        DataTable TablaPrecios = new DataTable("General_Etiquetas");
                        FbCommand cmd = new FbCommand("SELECT B.* FROM articulos A LEFT JOIN " +
                            "vicio_cambio_precios(A.articulo_id,'" + Inicio.Text + "','" + Final.Text + "')B " +
                            "ON B.articulo_id = A.articulo_id WHERE B.producto <> 'Null' ORDER BY B.LINEA", connection);
                        FbDataAdapter DA = new FbDataAdapter(cmd);
                        DA.Fill(TablaPrecios);
                        foreach (DataRow r in TablaPrecios.Rows)
                        {
                            Etiquetas.Rows.Add(r.ItemArray);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void CargarTablaPreciosIndividual(DataGridView Etiquetas, String clave)
        {
            try
            {
                using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        DataTable TablaPrecios = new DataTable("Individual_Etiquetas");
                        FbCommand cmd = new FbCommand("SELECT A.* FROM vicio_precio_individual('" + clave + "') A", connection);
                        FbDataAdapter DA = new FbDataAdapter(cmd);
                        DA.Fill(TablaPrecios);
                        foreach (DataRow r in TablaPrecios.Rows)
                        {
                            if (string.IsNullOrEmpty(r.ItemArray[0].ToString()))
                            {
                                MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                Etiquetas.Rows.Add(r.ItemArray);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void CargarTablaPreciosGramage(DataGridView Etiquetas, String clave, double precio, string gramage)
        {
            try
            {
                using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        DataTable TablaPrecios = new DataTable("Individual_Etiquetas");
                        FbCommand cmd = new FbCommand("SELECT A.* FROM vicio_precio_individual('" + clave + "') A", connection);
                        FbDataAdapter DA = new FbDataAdapter(cmd);
                        DA.Fill(TablaPrecios);
                        foreach (DataRow r in TablaPrecios.Rows)
                        {
                            if (string.IsNullOrEmpty(r.ItemArray[0].ToString()))
                            {
                                MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                r.SetField(2, Convert.ToDouble(r.ItemArray[2]) * precio);
                                r.SetField(3, gramage);
                                Etiquetas.Rows.Add(r.ItemArray);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static string ObtenerClave(string Articulo, int bandera)
        {
            try
            {
                string consulta = "";
                if (bandera == 0)
                {
                    consulta = "vicio_clave_articulo";
                }
                if (bandera == 1)
                {
                    consulta = "vicio_clave_articulo_kg";
                }
                using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        FbCommand cmd = new FbCommand("EXECUTE PROCEDURE " + consulta + " (" + Articulo + ")", connection);
                        FbDataReader consu = cmd.ExecuteReader();
                        if (consu.Read() == true)
                        {
                            Articulo = consu["CLAVE_ARTICULO"].ToString();
                        }
                        else
                        {
                            Articulo = String.Empty;
                            MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Articulo;
        }
        public static string ValidarConexion(string con)
        {
            try
            {
                using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        FbCommand cmd = new FbCommand("EXECUTE PROCEDURE CONEXION_VICIO('" + String.Format("{0: dd.MM.yyyy}", DateTime.Now) + "')", connection);
                        FbDataReader consu = cmd.ExecuteReader();
                        if (consu.Read() == true)
                        {
                            con = consu["CONEXION"].ToString();
                        }
                        else
                        {
                            con = string.Empty;
                            MessageBox.Show("Metodo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return con;
        }
        public static string ObtenerArticuloIdKg(String CLAVE)
        {
            try
            {
                using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        FbCommand cmd = new FbCommand("EXECUTE PROCEDURE VICIO_OBTENER_ARTICULO_ID ('" + CLAVE + "')", connection);
                        FbDataReader consu = cmd.ExecuteReader();
                        if (consu.Read() == true)
                        {
                            CLAVE = consu["ARTICULO_ID"].ToString();
                        }
                        else
                        {
                            CLAVE = String.Empty;
                            MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return CLAVE;
        }
        public static string ObtenerArticuloId(String CLAVE)
        {
            try
            {
                using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        DataTable TablaPrecios = new DataTable("ARTICULO_ID");
                        FbCommand cmd = new FbCommand("select a.NOMBRE from ARTICULOS a " +
                            "left join claves_articulos B ON b.articulo_id = a.articulo_id " +
                            "where B.clave_articulo = '" + CLAVE + "'", connection);
                        FbDataAdapter DA = new FbDataAdapter(cmd);
                        DA.Fill(TablaPrecios);
                        foreach (DataRow r in TablaPrecios.Rows)
                        {
                            if (string.IsNullOrEmpty(r.ItemArray[0].ToString()))
                            {
                                MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                CLAVE = r.ItemArray[0].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return CLAVE;
        }
        public static string ObtenerNombreArticulo(String CLAVE)
        {
            try
            {
                using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
                {
                    try
                    {
                        connection.Open();
                        DataTable TablaPrecios = new DataTable("Individual_Etiquetas");
                        FbCommand cmd = new FbCommand("SELECT A.* FROM vicio_obtener_nombre_articulo('" + CLAVE + "') A", connection);
                        FbDataAdapter DA = new FbDataAdapter(cmd);
                        DA.Fill(TablaPrecios);
                        foreach (DataRow r in TablaPrecios.Rows)
                        {
                            if (string.IsNullOrEmpty(r.ItemArray[0].ToString()))
                            {
                                MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                CLAVE = r.ItemArray[0].ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return CLAVE;
        }
        public static void TotalArticulos(Label etiqueta, DataGridView tabla)
        {
            try
            {
                etiqueta.Text = tabla.RowCount.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void AgregarBotonIndividual(ComboBox cbClaveIndividual, ComboBox cbArticuloIndividual, DataGridView dtvEtiquetasIndividual)
        {
            try
            {
                String articulo_id;
                String clave_articulo;
                if (cbClaveIndividual.Text == "" && cbArticuloIndividual.Text == "")
                {
                    MessageBox.Show("Favor de llenar los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (cbClaveIndividual.Text == "" && cbArticuloIndividual.Text != "")
                {
                    if (cbArticuloIndividual.SelectedValue == null)
                    {
                        MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        articulo_id = cbArticuloIndividual.SelectedValue.ToString();
                        cbClaveIndividual.Text = Metodos.ObtenerClave(articulo_id, 0);
                    }
                }
                else if (cbClaveIndividual.Text != "" && cbArticuloIndividual.Text != "")
                {
                    if (cbArticuloIndividual.SelectedValue == null)
                    {
                        MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        articulo_id = cbArticuloIndividual.SelectedValue.ToString();
                        clave_articulo = Metodos.ObtenerClave(articulo_id, 0);
                        if (cbClaveIndividual.Text != clave_articulo)
                        {
                            cbClaveIndividual.Text = clave_articulo;
                        }
                        else
                        {
                            Metodos.CargarTablaPreciosIndividual(dtvEtiquetasIndividual, cbClaveIndividual.Text);
                            dtvEtiquetasIndividual.FirstDisplayedScrollingRowIndex = dtvEtiquetasIndividual.RowCount - 1;
                        }
                    }
                }
                else if (cbArticuloIndividual.Text == "" && cbClaveIndividual.Text != "")
                {
                    Metodos.CargarTablaPreciosIndividual(dtvEtiquetasIndividual, cbClaveIndividual.Text);
                    cbClaveIndividual.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public static void agregarGramage(ComboBox cbClaveGramage, ComboBox cbArticuloGramage, DataGridView dtvEtiquetasGramage, double precio, string gramage)
        {
            try
            {
                String articulo_id;
                String clave_articulo;
                if (cbClaveGramage.Text == "" && cbArticuloGramage.Text == "")
                {
                    MessageBox.Show("Favor de llenar los campos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (cbClaveGramage.Text == "" && cbArticuloGramage.Text != "")
                {

                    if (cbArticuloGramage.SelectedValue == null)
                    {
                        MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        articulo_id = cbArticuloGramage.SelectedValue.ToString();
                        cbClaveGramage.Text = Metodos.ObtenerClave(articulo_id, 1);
                    }
                }
                else if (cbClaveGramage.Text != "" && cbArticuloGramage.Text != "")
                {
                    if (cbArticuloGramage.SelectedValue == null)
                    {
                        MessageBox.Show("Articulo no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        articulo_id = cbArticuloGramage.SelectedValue.ToString();
                        clave_articulo = Metodos.ObtenerClave(articulo_id, 1);
                        if (cbClaveGramage.Text != clave_articulo)
                        {
                            cbClaveGramage.Text = clave_articulo;
                        }
                        else
                        {
                            Metodos.CargarTablaPreciosGramage(dtvEtiquetasGramage, clave_articulo, precio, gramage);
                            dtvEtiquetasGramage.FirstDisplayedScrollingRowIndex = dtvEtiquetasGramage.RowCount - 1;
                        }
                    }
                }
                else if (cbArticuloGramage.Text == "" && cbClaveGramage.Text != "")
                {
                    Metodos.CargarTablaPreciosGramage(dtvEtiquetasGramage, cbClaveGramage.Text, precio, gramage);
                    cbClaveGramage.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public static void CargarComboBoxArticulosMY(ComboBox cbArticuloMY, TextBox txtClaveMY, ComboBox cbNombreMY, ComboBox cbNombreMDY)
        {
            String articulo_id = cbArticuloMY.SelectedValue.ToString();
            txtClaveMY.Text = Metodos.ObtenerClave(articulo_id, 0);
            using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
            {
                try
                {
                    connection.Open();
                    FbCommand cmd = new FbCommand("select c.precio_empresa_id,c.nombre from articulos a " +
                        "left join precios_articulos b on b.articulo_id = a.articulo_id " +
                        "left join precios_empresa c on c.precio_empresa_id = b.precio_empresa_id " +
                        "where a.articulo_id =" + articulo_id, connection);
                    FbDataAdapter DA = new FbDataAdapter(cmd);
                    //Se crea una tabla para almancear los datos de la consulta y posteriormente anexarlos al ComboBox
                    DataTable tablaMayoreo = new DataTable();
                    DataTable tablaMedio = new DataTable();
                    DA.Fill(tablaMayoreo);
                    DA.Fill(tablaMedio);
                    cbNombreMY.DataSource = tablaMayoreo;
                    cbNombreMDY.DataSource = tablaMedio;
                    cbNombreMY.ValueMember = "precio_empresa_id";
                    cbNombreMY.DisplayMember = "nombre";
                    cbNombreMDY.ValueMember = "precio_empresa_id";
                    cbNombreMDY.DisplayMember = "nombre";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static void CargarComboBoxPrecioLista(ComboBox cbArticuloMY, TextBox txtClaveMY, ComboBox cbNombreLista)
        {
            String articulo_id = cbArticuloMY.SelectedValue.ToString();
            txtClaveMY.Text = Metodos.ObtenerClave(articulo_id, 0);
            using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
            {
                try
                {
                    connection.Open();
                    FbCommand cmd = new FbCommand("select c.precio_empresa_id,c.nombre from articulos a " +
                        "left join precios_articulos b on b.articulo_id = a.articulo_id " +
                        "left join precios_empresa c on c.precio_empresa_id = b.precio_empresa_id " +
                        "where a.articulo_id =" + articulo_id + "and b.precio_empresa_id = 42", connection);
                    FbDataAdapter DA = new FbDataAdapter(cmd);
                    //Se crea una tabla para almancear los datos de la consulta y posteriormente anexarlos al ComboBox
                    DataTable tablaLista = new DataTable();
                    DA.Fill(tablaLista);
                    cbNombreLista.DataSource = tablaLista;
                    cbNombreLista.ValueMember = "precio_empresa_id";
                    cbNombreLista.DisplayMember = "nombre";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static void CargarPreciosListas(TextBox txtClaveMY, ComboBox cbNombreLista, TextBox txtPrecioLista, TextBox txtUnidadesLista, TextBox txtUVLista)
        {
            String clave_articulo = txtClaveMY.Text;
            String Empresa_id = cbNombreLista.SelectedValue.ToString();
            using (FbConnection connection = new FbConnection(Conexion.ConnectionString))
            {
                try
                {
                    connection.Open();
                    FbCommand cmd = new FbCommand("select a.precio, A.num_articulos, a.unid_venta from vicio_precios_my_medmy('" + clave_articulo + "'," + Empresa_id + ") a", connection);
                    FbDataReader consu = cmd.ExecuteReader();
                    if (consu.Read() == true)
                    {
                        txtPrecioLista.Text = consu["PRECIO"].ToString();
                        txtUnidadesLista.Text = consu["NUM_ARTICULOS"].ToString();
                        txtUVLista.Text = consu["UNID_VENTA"].ToString();
                    }
                    else
                    {
                        txtPrecioLista.Text = String.Empty;
                        txtUnidadesLista.Text = String.Empty;
                        txtUVLista.Text = String.Empty;
                        MessageBox.Show("Precio no encontrado", "Informativo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public static void rolHorario(int rol, DateTimePicker inicio, DateTimePicker final)
        {
            if (rol == 0)
            {
                inicio.Value = DateTime.Now.AddDays(-1);
                inicio.CustomFormat = "dd.MM.yyyy 16:00:00";
                final.Value = DateTime.Now;
                final.CustomFormat = "dd.MM.yyyy 08:00:00";
            }
            else if (rol == 1)
            {
                inicio.Value = DateTime.Now;
                inicio.CustomFormat = "dd.MM.yyyy 08:00:00";
                final.Value = DateTime.Now;
                final.CustomFormat = "dd.MM.yyyy 12:00:00";
            }
            else if(rol == 2)
            {
                inicio.Value = DateTime.Now;
                inicio.CustomFormat = "dd.MM.yyyy 12:00:00";
                final.Value = DateTime.Now;
                final.CustomFormat = "dd.MM.yyyy 16:00:00";
            }

        }
    }
}
