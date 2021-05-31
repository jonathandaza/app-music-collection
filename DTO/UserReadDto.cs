namespace DTO
{
    public class UserReadDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public byte Age { get; set; }
        public GenreReadDto Genre { get; set; }

        public static implicit operator UserCreateDto(UserReadDto userReadDto)
        {
            return new UserCreateDto
            {
                Name = userReadDto.Name,
                LastName = userReadDto.LastName,
                Age = userReadDto.Age,
                GenreId = userReadDto.Genre.Id,
            };
        }

        public static implicit operator UserUpdateDto(UserReadDto userReadDto)
        {
            return new UserUpdateDto
            {
                Name = userReadDto.Name,
                LastName = userReadDto.LastName,
                Age = userReadDto.Age,
                GenreId = userReadDto.Genre.Id,
            };
        }
    }
}
