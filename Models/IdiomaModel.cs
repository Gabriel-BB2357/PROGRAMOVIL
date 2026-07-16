using Plugin.Firebase.Firestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRadventure.Models
{
    class IdiomaModel
    {
        [FirestoreDocumentId]
        public string Id { get; set; } = string.Empty;

        [FirestoreProperty("nombre")]
        public string Nombre { get; set; } = string.Empty;
    }
}
