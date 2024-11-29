public class Admin_User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsAdmin { get; set; }
    public string Created_By { get; set; }
    public DateTime? Created_On { get; set; }  
    public string Updated_By { get; set; }
    public DateTime? Updated_On { get; set; } 
}
