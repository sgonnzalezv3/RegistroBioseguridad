using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RegistroBioseguridad
{
    public partial class Consulta : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            ServiceReference1.CrudBioseguridad1SoapClient Mostrar = new ServiceReference1.CrudBioseguridad1SoapClient();
            GridView1.DataSource = Mostrar.ConsultarVisitante();
            GridView1.DataBind();

        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}