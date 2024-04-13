using System.Text.Json;
using BlazorApp1.Data;

namespace BlazorApp1.Services {
	public class PokeAPIService : IPokeAPIService {
		
		private HttpClient httpClient;

		public PokeAPIService()
		{
			httpClient = new HttpClient();
		}

		public async Task<int> GetPokemonCount() {
			var apiCountResponse = await httpClient.GetFromJsonAsync<JsonElement>("https://pokeapi.co/api/v2/pokemon-species?limit=0");
			return apiCountResponse.GetProperty("count").GetInt32();
		}

        public async Task<Pokemon> GetPokemon(int id)
        {
            var apiResponse = await httpClient.GetFromJsonAsync<JsonElement>($"https://pokeapi.co/api/v2/pokemon/{id}");

            List<PokemonType> types = new List<PokemonType>();
            var typesArray = apiResponse.GetProperty("types").EnumerateArray();
            foreach (var typeElement in typesArray)
            {
                var typeName = typeElement.GetProperty("type").GetProperty("name").GetString();
                types.Add(new PokemonType { Name = typeName });
            }

            return new Pokemon
            {
                Id = id,
                Name = apiResponse.GetProperty("name").GetString(),
                Types = types 
            };
        }
    }
	
}
