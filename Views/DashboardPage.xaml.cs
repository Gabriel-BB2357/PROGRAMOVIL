
using CRadventure.Models;
using CRadventure.Services;
using Plugin.Firebase.Auth;
using Plugin.Firebase.Firestore;


namespace CRadventure;

public partial class DashboardPage : ContentPage
{

    private readonly TourService _tourService = new TourService();
    private readonly UsuarioModel _usuarioActual;
    public DashboardPage(UsuarioModel usuario)
    {
        InitializeComponent();
        _usuarioActual = usuario;
        CargarDashboard();
    }

    private async void CargarDashboard()
    {
        try
        {
            // 1. Obtenemos la lista de tours desde el servicio
            var listaTours = await _tourService.ObtenerTodosLosToursAsync();

            foreach (var tour in listaTours)
            {
                // Lógica de precios
                tour.AplicarTarifa(_usuarioActual.EsExtranjero);

                // Se valida que Idiomas no esté vacío
                if (!string.IsNullOrEmpty(tour.Idiomas))
                {
                    tour.IdiomasTexto = tour.Idiomas;
                }
                else
                {
                    tour.IdiomasTexto = "Sin idioma";
                }
            }

            // 4. Asignamos la lista a la vista después de haber procesado los idiomas
            this.cvTours.ItemsSource = listaTours;
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo cargar la información: {ex.Message}", "OK");
        }
    }

    private async void OnAgregarTourClicked(object sender, EventArgs e)
    {
        if (_usuarioActual.Rol == "guia" || _usuarioActual.Rol == "admin")
            await DisplayAlert("Próximamente", "Ir a Agregar Tour Page", "OK");
        else
            await DisplayAlert("Acceso", "Solo los guías pueden agregar tours", "OK");
    }

    private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is TourModel tourSeleccionado)
        {
            await DisplayAlert("Próximamente", "Ir a Tour Detalle Page", "OK");
            cvTours.SelectedItem = null;
        }
    }
}