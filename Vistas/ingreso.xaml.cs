using System.Net;

namespace semana7.Vistas;

public partial class ingreso : ContentPage
{
	public ingreso()
	{
		InitializeComponent();
	}

    private void btnRegistro_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("nombre", txtNombres.Text);
            parametros.Add("apellido", txtApellidos.Text);
            parametros.Add("direccion", txtDireccion.Text);
            parametros.Add("telefono", txtTelefono.Text);
            parametros.Add("email", txtEmail.Text);
            parametros.Add("contrasena", txtContrasena.Text);
            cliente.UploadValues("http://172.22.0.1/segentrega/post.php", "POST", parametros);
            Navigation.PushAsync(new Vistas.inicio());
        }
        catch (Exception ex) 
        {
            DisplayAlert("Alerta", ex.Message, "Cerrar");
        
        }
    }
}