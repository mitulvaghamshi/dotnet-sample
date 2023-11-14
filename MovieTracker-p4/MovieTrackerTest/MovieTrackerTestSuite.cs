namespace MovieTrackerTest
{
	public class MovieTrackerTestSuite
	{
		[Fact]
		public async Task Index_NoInput_ReturnMovie()
		{
			var context = CreateContext("Index");
			var controller = new MoviesController(context);

			var actionResult = await controller.Index();
			Assert.IsType<ViewResult>(actionResult);

			var viewResult = (ViewResult)actionResult;
			Assert.IsType<List<Movie>>(viewResult.Model);

			var movieList = (List<Movie>)viewResult.Model;
			Assert.Equal(3, movieList.Count);
			Assert.Equal(1, movieList.First().Id);
		}

		[Fact]
		public async Task Details_MovieId_ReturnMovie()
		{
			var context = CreateContext("Details");
			var controller = new MoviesController(context);

			// No ID provided
			var actionResult = await controller.Details(null);
			Assert.IsType<ViewResult>(actionResult);

			var viewResult = (ViewResult)actionResult;
			Assert.IsType<ErrorViewModel>(viewResult.Model);

			var model = (ErrorViewModel)viewResult.Model;
			Assert.False(model.ShowRequestId);
			Assert.Null(model.RequestId);
			Assert.Equal("Invalid Movie ID", model.Description);

			// Out of range ID provided
			actionResult = await controller.Details(101);
			Assert.IsType<ViewResult>(actionResult);

			viewResult = (ViewResult)actionResult;
			Assert.IsType<ErrorViewModel>(viewResult.Model);

			var errorModel = (ErrorViewModel)viewResult.Model;
			Assert.True(errorModel.ShowRequestId);
			Assert.NotNull(errorModel.RequestId);
			Assert.Contains("Unable to find", errorModel.Description);

			// Valid ID
			actionResult = await controller.Details(3);
			Assert.IsType<ViewResult>(actionResult);

			viewResult = (ViewResult)actionResult;
			Assert.IsType<Movie>(viewResult.Model);

			var movie = (Movie)viewResult.Model;
			Assert.Contains("3", movie.Id.ToString());
			Assert.Equal("Hamilton", movie.Title);
		}

		[Fact]
		public async Task Create_Movie_RedirectsToIndex()
		{
			var context = CreateContext("Create");
			var controller = new MoviesController(context);

			var movie = new Movie { Title = "Avengers - End Game", DateSeen = DateTime.Now.AddDays(-5), Genre = "Sci-Fi", Rating = 10 };

			var actionResult = await controller.Create(movie);
			Assert.IsType<RedirectToActionResult>(actionResult);

			var redirectActionResult = (RedirectToActionResult)actionResult;
			Assert.Equal("Index", redirectActionResult.ActionName);

			actionResult = await controller.Index();
			Assert.IsType<ViewResult>(actionResult);

			var viewResult = (ViewResult)actionResult;
			var movies = (List<Movie>)viewResult.Model!;

			Assert.Equal(4, movies.Count);
		}

		[Fact]
		public async Task Edit_Movie_RedirectsToIndex()
		{
			var context = CreateContext("Edit");
			var controller = new MoviesController(context);

			var actionResult = await controller.Edit(2);
			Assert.IsType<ViewResult>(actionResult);

			var viewResult = (ViewResult)actionResult;

			var movie = (Movie)viewResult.Model!;
			Assert.IsType<Movie>(movie);

			movie.Rating = 10;
			movie.Genre = "Novel";

			actionResult = await controller.Edit(2, movie);
			Assert.IsType<RedirectToActionResult>(actionResult);

			var redirectActionResult = (RedirectToActionResult)actionResult;
			Assert.Equal("Index", redirectActionResult.ActionName);

			actionResult = await controller.Index();
			Assert.IsType<ViewResult>(actionResult);

			viewResult = (ViewResult)actionResult;
			var movies = (List<Movie>)viewResult.Model!;

			movie = movies.Find(x => x.Id == 2)!;

			Assert.Equal(10, movie.Rating);
			Assert.Equal("Novel", movie.Genre);
		}

		[Fact]
		public async Task Delete_Movie_RedirectsToIndex()
		{
			var context = CreateContext("Delete");
			var controller = new MoviesController(context);

			var actionResult = await controller.Delete(3);
			Assert.IsType<ViewResult>(actionResult);

			var viewResult = (ViewResult)actionResult;

			var movie = (Movie)viewResult.Model!;
			Assert.IsType<Movie>(movie);

			actionResult = await controller.DeleteConfirmed(movie.Id);
			Assert.IsType<RedirectToActionResult>(actionResult);

			var redirectActionResult = (RedirectToActionResult)actionResult;
			Assert.Equal("Index", redirectActionResult.ActionName);

			actionResult = await controller.Index();
			Assert.IsType<ViewResult>(actionResult);

			viewResult = (ViewResult)actionResult;
			var movies = (List<Movie>)viewResult.Model!;
			Assert.Equal(2, movies.Count);
			Assert.False(movies.Exists(x => x.Title == "Hamilton"));
		}

		private static MovieTrackerContext CreateContext(string dbname)
		{
			var options = new DbContextOptionsBuilder<MovieTrackerContext>()
				.UseInMemoryDatabase(dbname)
				.Options;

			var context = new MovieTrackerContext(options);

			context.Movie.AddRange(
				new Movie { Id = 1, Title = "Birds of Prey", DateSeen = DateTime.Now.AddDays(-50), Genre = "Action", Rating = 6, },
				new Movie { Id = 2, Title = "Palm Springs", DateSeen = DateTime.Now.AddDays(-25), Rating = 7, },
				new Movie { Id = 3, Title = "Hamilton", Genre = "Drama", }
			);

			context.SaveChanges();

			return context;
		}
	}
}
