using System.ComponentModel.DataAnnotations;

namespace UserPostApi.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        
        public List<Post> Posts { get; set; } = new List<Post>();

        public List<Comment> Comments { get; set; } = new List<Comment>();


    }
}
