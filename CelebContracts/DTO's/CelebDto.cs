namespace CelebContracts
{
    public enum Gender { None, Male, Female }
    public class CelebDto
    {
        public string Name { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Role { get; set; }
        public string ImgUrl { get; set; }
    }
}