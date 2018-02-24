using System;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Hotel
{
    public partial class AgregarListaNegra : Form
    {
        public AgregarListaNegra()
        {
            InitializeComponent();
        }

        Msg msg;

        public string cedula = ""; // Se carga en Cliente

        private void btnAgregar_Click(object sender, System.EventArgs e)
        {
            if (btnAgregar.Text == "Modificar")
            {
                using (msg = new Msg())
                {
                    msg.lblMsg.Text = "¿Está seguro de que desea modificar el registro?";
                    DialogResult dlgres = msg.ShowDialog();
                    {
                        if (dlgres == DialogResult.Yes)
                        {
                            ModificarMotivoListaNegra();
                            Close();
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }

        private void ModificarMotivoListaNegra()
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection(ConexionBD.connstring))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("UPDATE Baneados SET Motivo=@motivo WHERE Cliente_Cedula=@cedula", conn))
                    {
                        cmd.Parameters.AddWithValue("@motivo", txtMotivo.Text.Trim());
                        cmd.Parameters.AddWithValue("@cedula", cedula);

                        conn.Open();

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se presentó un error.\n\n>> " + ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
