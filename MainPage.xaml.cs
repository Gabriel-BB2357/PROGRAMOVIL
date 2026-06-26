using Plugin.Firebase;
using Plugin.Firebase.Auth;
namespace CRadventure;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }





    private async void Registrar_Clicked(
        object sender,
        EventArgs e)
    {
        try
        {
            var auth = CrossFirebaseAuth.Current;

            var result =
                await auth.CreateUserAsync(
                    txtCorreo.Text,
                    txtPassword.Text);

            await DisplayAlert(
                "Éxito",
                $"Usuario creado: {result.Email}",
                "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error",
                ex.Message,
                "OK");
        }
    }

    private async void Login_Clicked(
        object sender,
        EventArgs e)
    {
        try
        {
            var auth = CrossFirebaseAuth.Current;

            var user =
                await auth.SignInWithEmailAndPasswordAsync(
                    txtCorreo.Text,
                    txtPassword.Text);

            await Navigation.PushAsync(
                new DashboardPage(user.Email));
        }
        catch (Exception ex)
        {
            await DisplayAlert(
                "Error Login",
                ex.Message,
                "OK");
        }
    }
}