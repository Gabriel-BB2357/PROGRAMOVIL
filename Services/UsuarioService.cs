using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CRadventure.Models;
using Plugin.Firebase.Firestore;

namespace CRadventure.Services
{
    public class UsuarioService
    {
        private const string ColeccionUsuarios = "usuarios";

        public async Task GuardarUsuarioAsync(UsuarioModel usuario)
        {
            await CrossFirebaseFirestore.Current
                .GetCollection(ColeccionUsuarios)
                .GetDocument(usuario.Uid)
                .SetDataAsync(usuario);
        }

        public async Task<UsuarioModel?> ObtenerUsuarioPorEmailAsync(string email)
        {
                 var documentos = await CrossFirebaseFirestore.Current
                .GetCollection(ColeccionUsuarios)
                .WhereEqualsTo("email", email)
                .GetDocumentsAsync<UsuarioModel>();

            return documentos.Documents.Select(d => d.Data).FirstOrDefault();
        }
    }
}