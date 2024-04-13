namespace BlazorApp1.Data {
    public class Pokemon {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PokemonType> Types { get; set; }

        public override string ToString() {
            return $"{Name[0].ToString().ToUpper()}{new string(Name.Skip(1).ToArray())}";
        }
    }
}
