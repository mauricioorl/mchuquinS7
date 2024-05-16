using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace semana7.Vistas;

public partial class inicio : ContentPage
{
    private const string Url = "http://172.22.0.1/segentrega/post.php";
    private readonly HttpClient cliente = new HttpClient();
    private ObservableCollection<usuario> usu;
     public inicio()
	{
		InitializeComponent();
        Obtener();
    }
    public async void Obtener()
    {
        try
        {
            var content = await cliente.GetStringAsync(Url);
            Console.WriteLine(content);
            List<usuario> mostrarUsu = JsonConvert.DeserializeObject<List<usuario>>(content);
            usu = new ObservableCollection<usuario>(mostrarUsu);
            listaEstudiantes.ItemsSource = usu;

            if (usu.Count > 0)
            {



            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al obtener datos: {ex.Message}");
            // Considera mostrar un mensaje al usuario aquí
        }
    }



    public class usuario
    {
        public int codigo { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string direccion { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string contrasena { get; set; }
    }

    private void btnIngreso_Clicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Vistas.ingreso());
    }

    private void btnActElimin_Clicked(object sender, EventArgs e)
    {
        
    }

    private void listaEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var objetousuario = (usuario)e.SelectedItem;
        Navigation.PushAsync(new Vistas.actualizarEliminar(objetousuario));
    }
}