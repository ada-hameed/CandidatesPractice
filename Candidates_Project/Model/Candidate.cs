namespace Candidates_Project.Model
{
    public class Candidate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public long Phone { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public bool IsAdmin { get; set; }
        public string Created_By { get; set; }
        public DateTime? Created_On { get; set; }
        public string Updated_By { get; set; }
        public DateTime? Updated_On { get; set; }
    }
}
