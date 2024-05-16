using Newtonsoft.Json;
using System.Net;
using System.Text;
namespace semana7.Vistas;

public partial class actualizarEliminar : ContentPage
{
	public actualizarEliminar(semana7.Vistas.inicio.usuario datos)
	{
		InitializeComponent();
        txtCodigo.Text = datos.codigo.ToString();
        txtNombres.Text = datos.nombre.ToString();
        txtApellidos.Text = datos.apellido.ToString();
        txtDireccion.Text = datos.direccion.ToString();
        txtTelefono.Text = datos.telefono.ToString();
        txtEmail.Text = datos.email.ToString();
        txtContrasena.Text = datos.contrasena.ToString();    

	}

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        try
        {
            WebClient cliente = new WebClient();
            var parametros = new System.Collections.Specialized.NameValueCollection();
            parametros.Add("codigo", txtCodigo.Text); 
            parametros.Add("nombre", txtNombres.Text);
            parametros.Add("apellido", txtApellidos.Text);
            parametros.Add("direccion", txtDireccion.Text);
            parametros.Add("telefono", txtTelefono.Text);
            parametros.Add("email", txtEmail.Text);
            parametros.Add("contrasena", txtContrasena.Text);
            // Agregar otros parámetros necesarios para la actualización
            cliente.UploadValues("http://172.22.0.1/segentrega/update_usu.php", "POST", parametros);
            await Navigation.PushAsync(new Vistas.inicio());
        }

        catch (Exception ex)
        {
            DisplayAlert("Alerta", ex.Message, "Cerrar");
        }
    }


    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Confirmar", "¿Deseas eliminar este usuario?", "Sí", "No");
        if (answer)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var requestUri = $"http://172.22.0.1/segentrega/post.php?codigo={txtCodigo.Text}";
                    var response = await client.DeleteAsync(requestUri);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Éxito", "Usuario eliminado correctamente.", "OK");
                    }
                    else
                    {
                        // Muestra el contenido de la respuesta en caso de error
                        await DisplayAlert("Error", "No se pudo eliminar el usuario: " + responseContent, "Cerrar");
                    }
                }
                await Navigation.PushAsync(new Vistas.inicio());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alerta", ex.Message, "Cerrar");
            }
        }
    }
}