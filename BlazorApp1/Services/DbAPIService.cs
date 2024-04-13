using BlazorApp1.Data;
using System.Text.Json;
using System.Text;

namespace BlazorApp1.Services {
    public class DbAPIService : FileAPIService {
        protected override string BASE_ADDR => "http://localhost:8000";
    }        
}
