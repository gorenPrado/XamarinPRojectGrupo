using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XamarinCalendarProject.Models;

namespace XamarinCalendarProject.Repositorio
{
    public class RepositorioCalendario
    {
        private String url;
        private MediaTypeWithQualityHeaderValue header;

        public RepositorioCalendario()
        {
            this.url = "https://projectclasecoregpp.azurewebsites.net/";
            this.header = new MediaTypeWithQualityHeaderValue("application/json");
        }
        public async Task<String> GetToken(string username, string password)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                Login login = new Login();
                login.Username = username;
                login.Password = password;
                String json = JsonConvert.SerializeObject(login);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                String request = "api/Auth/Login";
                HttpResponseMessage response = await client.PostAsync(request, content);
                if (response.IsSuccessStatusCode)
                {
                    String data = await response.Content.ReadAsStringAsync();
                    JObject jobject = JObject.Parse(data);
                    String token = jobject.GetValue("response").ToString();
                    return token;
                }
                else
                {
                    return null;
                }
            }
        }
        private async Task<T> CallApiAsync<T>(String request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    T data = JsonConvert.DeserializeObject<T>(json);
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        private async Task<T> CallApiGetTokenAsync<T>(String request, String token)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "bearer " + token);
                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    String json = await response.Content.ReadAsStringAsync();
                    T data = JsonConvert.DeserializeObject<T>(json);
                    return (T)Convert.ChangeType(data, typeof(T));
                }
                else
                {
                    return default(T);
                }
            }
        }
        public async Task<Usuarios> Login(String usuario, String password)
        {
            String token = await GetToken(usuario, password);
            if (token != null)
            {
                if (Barrel.Current.IsExpired(key: "calendarioxamarin"))
                {
                    Barrel.Current.Add("calendarioxamarin", token, TimeSpan.FromMinutes(10));
                }
                else
                {
                    Barrel.Current.EmptyAll();
                    Barrel.Current.Add("calendarioxamarin", token, TimeSpan.FromMinutes(10));
                }
                return await Perfil(token);
            }
            else
            {
                return null;
            }
        }
        
        public async Task<Usuarios> Perfil(String token)
        {
            Usuarios usuario = await CallApiGetTokenAsync<Usuarios>("/api/Project/Perfil", token);
            return usuario;
        }
        public async Task<List<Eventos>> EventoUsuario(int idusuario)
        {
            String request = "api/Project/EventoUsuarioSinToken/" + idusuario;
            List<Eventos> eventos = await CallApiAsync<List<Eventos>>(request);
            return eventos;
        }
        public async Task RegistrarUsuario(Usuarios usuario)
        {
            String json = JsonConvert.SerializeObject(usuario);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                String request = "/api/Project/CrearUsuario";
                Uri uri = new Uri(url + request);
                var x = await client.PostAsync(uri, content);
            }
        }
    }
}
