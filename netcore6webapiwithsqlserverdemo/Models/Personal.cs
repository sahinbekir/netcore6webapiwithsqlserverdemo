namespace netcore6webapiwithsqlserverdemo.Models
{
    public class Personal
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime Born { get; set; }
        public int Department { get; set; }
        public int Job { get; set; }
        public Sex Sex { get; set; }
        public string? Img { get; set; }
    }

    public enum Sex
    {
        Male, Female
    }
}
