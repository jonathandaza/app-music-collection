namespace API.Dtos
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public GenreReadDto Genre { get; set; }
    }
}
