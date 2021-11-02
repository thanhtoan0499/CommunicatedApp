namespace DatingApp.ViewModel.CommentViewModel
{
    public class CommentDto
    {
        public string Content { get; set; }
        public double Rating { get; set; } = 2.5;
        //
        public int BookId { get; set; }
        //
        public int UserId { get; set; }
    }
}
