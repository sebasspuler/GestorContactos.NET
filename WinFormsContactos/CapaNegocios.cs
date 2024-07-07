using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsContactos
{
    public class CapaNegocios
    {
        private CapaAccesoADatos _capaAccesoADatos;

        public CapaNegocios()
        {
            _capaAccesoADatos = new CapaAccesoADatos();
        }
        public Contacto GuardarContacto(Contacto contacto)
        {
            if (contacto.Id == 0)
                _capaAccesoADatos.InsertarContacto(contacto);
            else
                _capaAccesoADatos.ActualizarContacto(contacto);

            return contacto;
        }

        public List<Contacto> GetContactos(string searchText = null)
        {
            return _capaAccesoADatos.GetContactos(searchText);
        }

        public void BorrarContacto(int id)
        {
            _capaAccesoADatos.BorrarContacto(id);
        }
    }
}
