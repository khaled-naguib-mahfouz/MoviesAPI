namespace Movies.Models
{
	public class Movie
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public int Year { get; set; }
		public double Rate { get; set; }
		public string StoryLine { get; set; }
		public byte[] Poster { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
