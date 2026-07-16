using Plugin.Firebase;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Firestore;
namespace CRadventure;
using CRadventure.Models;
using CRadventure.Services;
//using Firebase.Firestore.Auth;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    //Navega a la pagina de registro
    private async void Registrar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    //Metodo para validar que no hay campos vacios
    private bool ValidarCampos()
    {
        if (string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            DisplayAlert("Error", "Por favor completa todos los campos", "OK");
            return false;
        }

        return true;
    }

    //Metodo del boton de login
    private async void Login_Clicked(object sender, EventArgs e)
    {
        if (!ValidarCampos())
            return;

        try
        {
            var auth = CrossFirebaseAuth.Current;

            var user = await auth.SignInWithEmailAndPasswordAsync(txtCorreo.Text, txtPassword.Text);

            // Obtener el rol del usuario desde Firestore
            var service = new UsuarioService();
            var usuario = await service.ObtenerUsuarioPorUidAsync(user.Uid);

            if (usuario == null)
            {
                await DisplayAlert("Error", "Usuario no encontrado en la base de datos", "OK");
                return;
            }

            // Navegar según el rol
            if (usuario.Rol == "admin")
                await Navigation.PushAsync(new DashboardPage(user.Email));
            else if (usuario.Rol == "guia")
                await Navigation.PushAsync(new DashboardPage(user.Email));
            else if (usuario.Rol == "cliente")
                await Navigation.PushAsync(new DashboardPage(user.Email));
            else
                await DisplayAlert("Error", "Rol no reconocido", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error Login", "Correo o contraseña incorrectos", "OK");
        }
    }
}