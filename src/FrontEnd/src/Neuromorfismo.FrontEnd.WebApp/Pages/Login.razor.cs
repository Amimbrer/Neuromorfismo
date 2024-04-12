using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using Neuromorfismo.FrontEnd.Service;
using Neuromorfismo.FrontEnd.ServiceDependencies;
using Neuromorfismo.Shared.Dto.UserAccount;
using Neuromorfismo.Shared.Dto.Usuarios;

namespace Neuromorfismo.FrontEnd.WebApp.Pages
{
    public partial class Login {
        // INJECCIONES
        [Inject] JWTAuthenticationProvider _jwtAuthenticationProvider { get; set; } = default!;
        [Inject] IRedirigirManager redirigirManager { get; set; } = default!;
        [Inject] ICrearHttpClient _crearHttpClient { get; set; } = default!;


        private UserLoginDto userLogin = new();
        private bool mostrado { get; set; } = false;
        private bool cargando { get; set; } = false;
        private string iconoPassword { get; set; } = Icons.Material.Filled.VisibilityOff;
        private string mensajeErrorLogin { get; set; } = String.Empty;


        // Tipo de input para la contraseña
        private InputType tipoInputPass { get; set; } = InputType.Password;
   
        private HttpClient Http { get; set; } = null!;

        protected override void OnInitialized() {
            Http = _crearHttpClient.CrearHttpApi();
        }

        private void clickPassword() {
            if(mostrado)
            {
                mostrado = false;
                iconoPassword = Icons.Material.Filled.VisibilityOff;
                tipoInputPass = InputType.Password;
            }
            else {
                mostrado = true;
                iconoPassword = Icons.Material.Filled.Visibility;
                tipoInputPass = InputType.Text;
            }
        }

        private async Task HacerLogin() {
            try {
                HttpResponseMessage respuesta;

                // validamos el formulario
                cargando = true;
                respuesta = await Http.PostAsJsonAsync("cuentas/autenticarusuario", userLogin);
                if (respuesta.IsSuccessStatusCode) {
                    Tokens? token = await respuesta.Content.ReadFromJsonAsync<Tokens>();
                    if (token is not null) {
                        await _jwtAuthenticationProvider.Login(token);
                        await redirigirManager.RedirigirDefault();
                    }
                } else {
                    if(respuesta.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                        mensajeErrorLogin = "Usuario o contraseña no válidos";
                    } else {
                        mensajeErrorLogin = "Actualmente no es posible realizar login, intémtelo de nuevo más tarde";
                    }
                    cargando = false;
                }
            } catch (Exception) {
                cargando = false;
                mensajeErrorLogin = "Error inesperado de autenticación, inténtelo de nuevo";
            }
        }
    }
}
