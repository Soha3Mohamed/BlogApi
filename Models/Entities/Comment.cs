namespace UserPostApi.Models.Entities
{
    public class Comment
    {
        //user can have many comments on himself and other posts and post can have many comments from different users
        //let's keep validation logic in DTOs to apply single responsibility and avoid tight coupling
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }


    }
}
