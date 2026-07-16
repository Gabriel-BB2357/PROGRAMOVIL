using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Plugin.Firebase.Firestore;

namespace CRadventure.Models
{
    class TourModel
    {
        [FirestoreDocumentId]
        public string Id { get; set; } = string.Empty;

        // Cambiado de "nombre_lugar" a "titulo"
        [FirestoreProperty("titulo")]
        public string NombreLugar { get; set; } = string.Empty;

        // Cambiado de "descripcion_corta" a "descripcionCorta"
        [FirestoreProperty("descripcionCorta")]
        public string DescripcionCorta { get; set; } = string.Empty;

        [FirestoreProperty("imagen_url")]
        public string ImagenUrl { get; set; } = string.Empty;

        [FirestoreProperty("precioNacional")]
        public double PrecioNacional { get; set; }

        [FirestoreProperty("precioExtranjero")]
        public double PrecioExtranjero { get; set; }

        [FirestoreProperty("duracionMinutos")]
        public int DuracionMinutos { get; set; }

        public string PrecioVisual { get; set; } = string.Empty;

        public void AplicarTarifa(bool esExtranjero)
        {
            double precioFinal = esExtranjero ? PrecioExtranjero : PrecioNacional;
            PrecioVisual = esExtranjero
                ? $"${precioFinal:F2} USD"
                : $"₡{precioFinal:N0} CRC";
        }

        [FirestoreProperty("idiomas_ids")]
        public List<string> IdiomasIds { get; set; } = new List<string>();

        public string IdiomasTexto { get; set; } = "Cargando...";
        public double Calificacion { get; set; } = 4.5; // Valor estático PRUEBA

    }
}
