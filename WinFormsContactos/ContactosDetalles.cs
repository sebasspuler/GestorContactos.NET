using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsContactos
{
    public partial class ContactosDetalles : Form
    {
        private CapaNegocios _capaNegocios;
        private Contacto _contacto;
        public ContactosDetalles()
        {
            InitializeComponent();
            _capaNegocios = new CapaNegocios();
        }

        #region EVENTOS
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            GuardarContacto();
            this.Close();
            ((Main)this.Owner).MostrarContactos();
        }

        #endregion

        #region Métodos Privados
        private void GuardarContacto()
        {
            Contacto contacto = new Contacto();
            contacto.Nombre = txtNombre.Text;
            contacto.Apellido = txtApellido.Text;
            contacto.Telefono = txtTelefono.Text;
            contacto.Direccion = txtDireccion.Text;

            contacto.Id = _contacto != null ? _contacto.Id : 0;

            _capaNegocios.GuardarContacto(contacto);
        }

        private void LimpiarFormulario()
        {
            txtNombre.Text = string.Empty;
            txtApellido.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtDireccion.Text = string.Empty;
        }

        #endregion

        #region Métodos Publicos
        public void LoadContacto(Contacto contacto)
        {
            _contacto = contacto;
            if (contacto != null)
            {
                LimpiarFormulario();

                txtNombre.Text = contacto.Nombre;
                txtApellido.Text = contacto.Apellido;
                txtTelefono.Text = contacto.Telefono;
                txtDireccion.Text = contacto.Direccion;
            }
        }

        #endregion

    }
}
