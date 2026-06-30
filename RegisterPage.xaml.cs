namespace CRadventure;

using Plugin.Firebase.Auth;
using CRadventure.Models;
using CRadventure.Services;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
	}

    //Boton regresar
    private async void Volver_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    //Validaciones
    private bool ValidarCampos()
    {
        if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
            string.IsNullOrWhiteSpace(txtApellidos.Text) ||
            string.IsNullOrWhiteSpace(txtCorreo.Text) ||
            string.IsNullOrWhiteSpace(txtTelefono.Text) ||
            string.IsNullOrWhiteSpace(txtPassword.Text) ||
            string.IsNullOrWhiteSpace(txtConfirmPassword.Text))
        {
            DisplayAlert("Error", "Por favor completa todos los campos", "OK");
            return false;
        }

        if (txtPassword.Text != txtConfirmPassword.Text)
        {
            DisplayAlert("Error", "Las contraseñas no coinciden", "OK");
            return false;
        }

        if (txtPassword.Text.Length < 6)
        {
            DisplayAlert("Error", "La contraseña debe tener al menos 6 caracteres", "OK");
            return false;
        }

        return true;
    }

    private async void Registrar_Clicked(object sender, EventArgs e)
    {
        if (!ValidarCampos())
            return;

        try
        {
            var auth = CrossFirebaseAuth.Current;

            var authResult = await auth.CreateUserAsync(txtCorreo.Text, txtPassword.Text);

            var nuevoUsuario = new UsuarioModel
            {
                Uid = authResult.Uid,
                Nombre = txtNombre.Text,
                Apellidos = txtApellidos.Text,
                Email = txtCorreo.Text,
                Telefono = txtTelefono.Text,
                Rol = "cliente",
                Activo = true,
                EsExtranjero = switchExtranjero.IsToggled,
                FechaRegistro = DateTime.UtcNow
            };

            var service = new UsuarioService();
            await service.GuardarUsuarioAsync(nuevoUsuario);

            await DisplayAlert("Éxito", "Cuenta creada correctamente", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", "No se pudo crear la cuenta: " + ex.Message, "OK");
        }
    }
}