using Plugin.Firebase.Auth;

namespace CRadventure;

public partial class DashboardPage : ContentPage
{
    public DashboardPage(string correo)
    {
        InitializeComponent();

        lblBienvenida.Text =
            $"Bienvenido\n{correo}";
    }

    private async void CerrarSesion_Clicked(
        object sender,
        EventArgs e)
    {
        await CrossFirebaseAuth.Current.SignOutAsync();

        await Navigation.PopAsync();
    }
}