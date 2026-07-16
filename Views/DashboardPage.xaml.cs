
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
            var listaTours = await _tourService.ObtenerTodosLosToursAsync();

            foreach (var tour in listaTours)
            {
                tour.AplicarTarifa(_usuarioActual.EsExtranjero);

                if (tour.IdiomasIds != null && tour.IdiomasIds.Count > 0)
                {
                    try
                    {
                        var nombresIdiomas = new List<string>();

                        foreach (var idiomaId in tour.IdiomasIds)
                        {
                            var snapshot = await CrossFirebaseFirestore.Current
                                .GetCollection("idiomas")
                                .GetDocument(idiomaId)
                                .GetDocumentSnapshotAsync<IdiomaModel>();

                            if (snapshot != null && snapshot.Data != null)
                            {
                                nombresIdiomas.Add(snapshot.Data.Nombre);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error cargando idioma: {ex.Message}");
                    }
                }
                else
                {
                    tour.IdiomasTexto = "Sin idioma";
                }
            } 

            this.cvTours.ItemsSource = listaTours; 
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
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