using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WinFormsContactos
{
    public partial class Main : Form
    {
        private CapaNegocios _capaNegocios;

        public Main()
        {
            InitializeComponent();
            _capaNegocios = new CapaNegocios();
        }

        #region Eventos
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AbrirContactoDetallesDialog();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            MostrarContactos();
        }

        private void gridContactos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verificar que el índice de la fila y la columna sean válidos
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                // Verificar que la celda sea un DataGridViewLinkCell
                if (gridContactos.Rows[e.RowIndex].Cells[e.ColumnIndex] is DataGridViewLinkCell cell)
                {
                    // Verificar que el valor de la celda no sea nulo y sea una cadena
                    if (cell.Value != null && cell.Value is string value)
                    {
                        if (value == "Editar")
                        {
                            ContactosDetalles contactosDetalles = new ContactosDetalles();
                            contactosDetalles.LoadContacto(new Contacto
                            {
                                Id = int.Parse(gridContactos.Rows[e.RowIndex].Cells[0].Value.ToString()),
                                Nombre = gridContactos.Rows[e.RowIndex].Cells[1].Value.ToString(),
                                Apellido = gridContactos.Rows[e.RowIndex].Cells[2].Value.ToString(),
                                Telefono = gridContactos.Rows[e.RowIndex].Cells[3].Value.ToString(),
                                Direccion = gridContactos.Rows[e.RowIndex].Cells[4].Value.ToString(),
                            });
                            contactosDetalles.ShowDialog(this);
                        }
                        else if (value == "Eliminar")
                        {
                            if (MessageBox.Show("¿Quieres eliminar este contacto?", "Confirmación", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            {
                                BorrarContacto(int.Parse(gridContactos.Rows[e.RowIndex].Cells[0].Value.ToString()));
                                MostrarContactos();
                            }
                        }
                    }
                }
            }
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MostrarContactos(txtSearch.Text);
            txtSearch.Text = string.Empty;
        }

        #endregion

        #region Métodos Privados

        private void AbrirContactoDetallesDialog()
        {
            ContactosDetalles contactosDetalles = new ContactosDetalles();
            contactosDetalles.ShowDialog(this);
        }

        private void BorrarContacto(int id)
        {
            _capaNegocios.BorrarContacto(id);
        }

        #endregion

        #region Métodos Públicos

        public void MostrarContactos(string searchText = null)
        {
            List<Contacto> contactos = _capaNegocios.GetContactos(searchText);
            gridContactos.DataSource = contactos;
        }

        #endregion
    }
}

