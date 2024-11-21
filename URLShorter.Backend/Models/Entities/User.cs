namespace URLShorter.Backend.Models.Entities;

public class User
{
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string? Email{ get; set; }
        public string Password { get; set; }
        
        public int RoleId { get; set; }
        
        public List<Url> Urls { get; set; } = [];
}