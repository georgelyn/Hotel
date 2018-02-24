using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Hotel
{
    public partial class ListaNegra : Form
    {
        public ListaNegra()
        {
            InitializeComponent();

            CargarListView("");
            txtBuscar.Select();
        }

        Msg msg;
        ListView lst;
        AgregarListaNegra confirmar;

        Font font_verdana = new Font("Verdana", 12);
        private ListViewColumnSorter lvwColumnSorter;

        internal string nombre;
        string cedula;
        string motivo = "";


        private void CrearListView()
        {
            lst = new ListView();

            lst.View = View.Details;
            lst.FullRowSelect = true;
            lst.GridLines = true;
            lst.MultiSelect = false;
            lst.ShowItemToolTips = true;
            lst.Dock = DockStyle.Fill;
            lst.Font = font_verdana;

            lst.Columns.Add("ID", 0, HorizontalAlignment.Left);
            lst.Columns.Add("Nro.", 50, HorizontalAlignment.Center);
            lst.Columns.Add("Cliente", 310, HorizontalAlignment.Left);
            lst.Columns.Add("Cédula", 100, HorizontalAlignment.Left);
            //lst.Columns.Add("En lista desde", 126, HorizontalAlignment.Left);

            panelLista.Controls.Add(lst);

            lst.BringToFront();

            lst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lst_MouseDoubleClick);
            lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);

            //lst.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lst_ColumnClick);

            lvwColumnSorter = new ListViewColumnSorter();
            this.lst.ListViewItemSorter = lvwColumnSorter;
        }

        private void CargarListView(string opcion)
        {
            string query = "";

            if (panelLista.Contains(lst))
            {
                panelLista.Controls.Remove(lst);
            }

            CrearListView();

            if (opcion == "buscar")
            {
                query = "SELECT ID, Baneado_ID, Nombre, Cedula FROM Clientes WHERE Nombre LIKE '%" + txtBuscar.Text.Trim() + "%' OR Cedula LIKE '%" + txtBuscar.Text.Trim().Replace(".", "").Replace(",", "").Replace("-", "") + "%' GROUP BY Nombre";
                //lblListaClientes.Visible = false;
            }
            else
            {
                query = "SELECT Clientes.ID, Nombre, Cedula FROM Clientes INNER JOIN Baneados b WHERE b.Cliente_Cedula = Cedula";
                //lblListaClientes.Visible = true;
            }

            try
            {
                int numero = 1;

                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        conn.Open();

                        lst.Items.Clear();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ListViewItem item = new ListViewItem(dr["ID"].ToString());

                                    item.SubItems.Add(numero.ToString());
                                    item.SubItems.Add(dr["Nombre"].ToString());
                                    item.SubItems.Add(dr["Cedula"].ToString());

                                    if (opcion == "buscar")
                                    {
                                        if (dr["Baneado_ID"] != DBNull.Value)
                                        {
                                            item.SubItems[0].BackColor = Color.LightGray;
                                        }

                                        lblListaClientes.Text = "Resultado de la búsqueda:";
                                        lblListaClientes.Tag = 3;
                                    }
                                    else
                                    {
                                        lblListaClientes.Text = "Clientes en lista negra:";
                                        lblListaClientes.Tag = 2;
                                    }

                                    lst.Items.Add(item);
                                    numero++;
                                }
                            }
                            else
                            {
                                lblListaClientes.Text = "No hay ningún cliente en lista negra.";
                                lblListaClientes.Tag = 1;
                                txtBuscar.Select();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void AgregarLista()
        {
            string query = "INSERT INTO Baneados (Cliente_Cedula, Fecha, Motivo) VALUES (@cedula, @fecha, @motivo);" +
                "UPDATE Clientes SET Baneado_ID=last_insert_rowid() WHERE Cedula = @cedula";

            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cedula", cedula);
                        cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmd.Parameters.AddWithValue("@motivo", motivo);

                        conn.Open();
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cliente agregado a la lista negra.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
            {
                CargarListView("buscar");
            }
            else
            {
                if (lblListaClientes.Tag.Equals(3))
                {
                    CargarListView("");
                    btnAgregar.Visible = false;
                }
                txtBuscar.Select();
            }
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13) // Enter
            {
                if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
                {
                    btnBuscar_Click(null, null);
                }
                else
                {
                    if (lblListaClientes.Tag.Equals(3))
                    {
                        CargarListView("");
                        btnAgregar.Visible = false;
                    }
                    txtBuscar.Select();
                }
            }

        }

        private void lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            throw new NotImplementedException();

            // Ver ficha cliente

            //f1.ActivarTimerEspera();

            /*

            if (opcion == "cliente" || opcion == "actual" || opcion == "buscar")
            {
                btnModificar_Click(null, null);
            }
            else // Habitacion
            {
                reservacion = new Reservacion();

                reservacion.CargarReservacion(int.Parse(lst.SelectedItems[0].SubItems[2].Text.ToString()), "ocupada");
                //this.Hide();
                reservacion.ShowDialog();
                CargarListView("habitacion");
                //this.Show();

                //MessageBox.Show("De momento da error el método de Actualizar Colores porque Form1 no está activa. Verificar si sigue siendo el caso una vez que cambie Application.Run a Form1 nuevamente.");
            }*/
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lblListaClientes.Tag.Equals(3))
            {
                if (lst.SelectedItems.Count > 0)
                {
                    if (lst.SelectedItems[0].BackColor != Color.LightGray) // Si NO está en la lista (color gris)
                    {
                        btnAgregar.Visible = true;
                        nombre = lst.SelectedItems[0].SubItems[2].Text.ToString();
                        cedula = lst.SelectedItems[0].SubItems[3].Text.ToString();
                    }
                        //item.SubItems[0].BackColor = Color.LightGray;
                }
                else
                {
                    btnAgregar.Visible = false;
                }
            }

        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (lst.SelectedItems.Count > 0)
            {
                msg = new Msg();

                msg.lblMsg.Text = $"¿Está seguro de que desea agregar al cliente a la lista negra? \n\nCliente: \"{lst.SelectedItems[0].SubItems[2].Text}\" \nCédula: \"{lst.SelectedItems[0].SubItems[3].Text}\".";
                DialogResult dlgres = msg.ShowDialog();
                {
                    if (dlgres == DialogResult.Yes)
                    {
                        confirmar = new AgregarListaNegra();
                        Reservacion reserva = new Reservacion();
                        confirmar.lblCliente.Text = nombre;
                        DialogResult agregarResult = confirmar.ShowDialog();

                        if (agregarResult == DialogResult.Yes)
                        {
                            motivo = confirmar.txtMotivo.Text;
                            AgregarLista();
                            if (reserva.TieneReserva(cedula))
                            {
                                reserva.EliminarReservacion(cedula);
                            }

                            CargarListView("");
                            txtBuscar.Clear();
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }
                }

            }
        }
    }
}
