using BlazorApp1.Data;

namespace BlazorApp1.Services {
	public interface IPokeAPIService {
		Task<int> GetPokemonCount();
		Task<Pokemon> GetPokemon(int id);
	}
}
