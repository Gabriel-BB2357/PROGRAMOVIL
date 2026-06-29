using Javax.Security.Auth;
using Plugin.Firebase;
using Plugin.Firebase.Auth;
namespace CRadventure;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    //Funcion del boton de registrarse (Envia a la pestana register)
    private async void Registrar_Clicked(object sender, EventArgs e)
    {
       // await Navigation.PushAsync(new RegisterPage());
    }

    //Metodo de validaciones 
    private bool ValidarCampos()
    {
        if (string.IsNullOrWhiteSpace(txtCorreo.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            DisplayAlert("Error", "Por favor completa todos los campos.", "OK");
            return false;
        }
        return true;
    }

    private async void Login_Clicked(object sender, EventArgs e)
    {
        //Se llama al metodo validacion
        if (!ValidarCampos()) return;

        try
        {
            var auth = CrossFirebaseAuth.Current;
            var user = await auth.SignInWithEmailAndPasswordAsync(
                txtCorreo.Text.Trim(),
                txtPassword.Text);

            await Navigation.PushAsync(new DashboardPage(user.Email));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "Correo o contraseña incorrectos", "OK");
        }
    }
}