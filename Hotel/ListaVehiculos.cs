using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Hotel
{
    public partial class ListaVehiculos : Form
    {
        public ListaVehiculos()
        {
            InitializeComponent();

            CargarVehiculos(false);
        }

        VerVehiculo verVehiculo;
        public ListView lst;
        Font font_verdana = new Font("Verdana", 12);

        private ListViewColumnSorter lvwColumnSorter;


        private void CrearListView()
        {
            lst = new ListView();

            lst.View = View.Details;
            lst.FullRowSelect = true;
            lst.GridLines = true;
            lst.MultiSelect = false;
            lst.ShowItemToolTips = true;
            //lst.Alignment = ListViewAlignment.SnapToGrid;
            lst.Dock = DockStyle.Fill;
            lst.Font = font_verdana;

            lst.Columns.Add("IDVehiculo", 0, HorizontalAlignment.Left);
            lst.Columns.Add("Cedula", 0, HorizontalAlignment.Left);
            lst.Columns.Add("Nro.", 50, HorizontalAlignment.Left);
            lst.Columns.Add("Cliente", 250, HorizontalAlignment.Left);
            lst.Columns.Add("Marca", 100, HorizontalAlignment.Left);
            lst.Columns.Add("Modelo", 250, HorizontalAlignment.Left);
            lst.Columns.Add("Placa", 122, HorizontalAlignment.Left);
            lst.Columns.Add("Camión", 100, HorizontalAlignment.Center);

            panelListView.Controls.Add(lst);

            lst.BringToFront();

            lst.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lst_MouseDoubleClick);
            lst.SelectedIndexChanged += new System.EventHandler(this.lst_SelectedIndexChanged);

            lst.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lst_ColumnClick);
            lvwColumnSorter = new ListViewColumnSorter();
            this.lst.ListViewItemSorter = lvwColumnSorter;
        }

        public void CargarVehiculos(bool buscar)
        {
            if (panelListView.Contains(lst))
            {
                panelListView.Controls.Remove(lst);
            }

            if (btnVerVehiculo.Visible)
            {
                btnVerVehiculo.Visible = false;
            }

            CrearListView();

            string query = "SELECT *, Nombre FROM Vehiculos INNER JOIN Clientes ON Cliente_Cedula=Cedula";

            if (buscar)
            {
                query = "SELECT *, Nombre FROM Vehiculos INNER JOIN Clientes ON Cliente_Cedula=Cedula WHERE Cliente_Cedula LIKE @criterioBusqueda OR Marca LIKE @criterioBusqueda OR Modelo LIKE @criterioBusqueda OR Placa LIKE @criterioBusqueda OR Nombre LIKE '%" + txtBuscar.Text.Trim() + "%'";
            }

            try
            {
                int index = 1;

                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        if (buscar)
                        {
                            cmd.Parameters.AddWithValue("@criterioBusqueda", txtBuscar.Text.Trim());
                        }
                        conn.Open();

                        lst.Items.Clear();

                        using (SQLiteDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    ListViewItem item = new ListViewItem(dr["ID"].ToString());
                                    item.SubItems.Add(dr["Cliente_Cedula"].ToString());
                                    item.SubItems.Add(index.ToString());
                                    item.SubItems.Add(dr["Nombre"].ToString());
                                    item.SubItems.Add(dr["Marca"].ToString());
                                    item.SubItems.Add(dr["Modelo"].ToString());
                                    item.SubItems.Add(dr["Placa"].ToString());
                                    if (Convert.ToBoolean(dr["EsCamion"]) == true)
                                    {
                                        item.SubItems.Add("Sí");
                                    }
                                    else
                                    {
                                        item.SubItems.Add("No");
                                    }

                                    lst.Items.Add(item);
                                    index++;
                                }
                            }
                            //else
                            //{

                            //}
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha presentado un problema.\nDetalles:\n\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lst_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lst.Sort();
        }

        private void lst_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            verVehiculo = new VerVehiculo();
            verVehiculo.CargarVehiculosPorCedula(lst.SelectedItems[0].SubItems[1].Text.ToString(), null);
            verVehiculo.comboVehiculo.Text = lst.SelectedItems[0].SubItems[4].Text.ToString() + " - " + lst.SelectedItems[0].SubItems[5].Text.ToString();
            verVehiculo.ShowDialog();
        }

        private void lst_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst.SelectedItems.Count > 0)
            {
                btnVerVehiculo.Visible = true;
            }
            else
            {
                btnVerVehiculo.Visible = false;
            }
        }

        private void btnVerVehiculo_Click(object sender, EventArgs e)
        {
            lst_MouseDoubleClick(null, null);
            lst.Focus();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtBuscar.Text.Trim()))
            {
                CargarVehiculos(true);
            }
            else
            {
                txtBuscar.Select();
            }
        }

        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            lblBusqueda.Visible = true;
            CargarVehiculos(false);
        }

        private void txtBuscar_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblBusqueda.Visible = false;

            if (e.KeyChar == 13) // Enter
            {
                btnBuscar_Click(null, null);
            }
        }

        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBuscar.Text.Trim()))
            {
                lblBusqueda.Visible = true;
            }
        }

        private void lblBusqueda_Click(object sender, EventArgs e)
        {
            txtBuscar.Select();
        }

        //private void ListaVehiculos_Activated(object sender, EventArgs e)
        //{
        //    CargarVehiculos(false);
        //}
    }
}
