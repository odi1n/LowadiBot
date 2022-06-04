using System.Threading.Tasks;
using System.Windows;
using Lowadi;
using Lowadi.Models;
using Lowadi.Models.Type;
using Newtonsoft;
using Newtonsoft.Json;

namespace LowadiBot.Models
{
    internal class Account
    {
        [JsonProperty("login")] public string Login { get; set; }
        [JsonProperty("password")] public string Password { get; set; }
        [JsonProperty("is_save")] public bool IsSave { get; set; }
        [JsonProperty("is_proxy")] public bool IsProxy { get; set; }
        [JsonProperty("proxy")] public string Proxy { get; set; } = string.Empty;
        [JsonProperty("proxy_login")] public string ProxyLogin { get; set; } = string.Empty;
        [JsonProperty("proxy_pass")] public string ProxyPassword { get; set; } = string.Empty;
        [JsonProperty("server")] public ServerType Server { get; set; }
        [JsonIgnore] public bool IsSelected { get; set; }
        [JsonIgnore] public LowadiApi LowadiApi { get; private set; }

        public async Task<bool> Auth()
        {
            LowadiApi lowadiApi = new LowadiApi(this.Server);
            ErrorModels errorModels = await lowadiApi.Login(this.Login, this.Password);
            if (errorModels.Errors.Count > 0)
            {
                MessageBox.Show("Не правильный логин или пароль.", "Проверьте данные", MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return false;
            }

            LowadiApi = lowadiApi;
            return true;
        }
    }
}