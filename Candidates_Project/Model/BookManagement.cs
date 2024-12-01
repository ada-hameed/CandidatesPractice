namespace Candidates_Project.Model
{
    public class BookManagement
    {
        public int Book_Id {  get; set; }
        public string Title { get; set; }
        public string Author {  get; set; }
        public string Category {  get; set; }
        public int Quantity {  get; set; }
        public DateOnly PublicationDate {  get; set; }

        public string Created_By { get; set; }
        public DateTime? Created_On { get; set; }
        public string Updated_By { get; set; }
        public DateTime? Updated_On { get; set; }

    }
}
