using Microsoft.Extensions.Logging;

namespace MovieTrackerTest
{
	public class HomeControllerTest
	{
		[Fact]
		public void HomeController_Index()
		{
			var controller = GetHomeController();

			var actionResult = controller.Index();
			Assert.IsType<ViewResult>(actionResult);
		}

		[Fact]
		public void HomeController_Privacy()
		{
			var controller = GetHomeController();

			var actionResult = controller.Privacy();
			Assert.IsType<ViewResult>(actionResult);
		}

		[Fact]
		public void HomeController_PageNotFound()
		{
			var controller = GetHomeController();

			var actionResult = controller.PageNotFound();
			Assert.IsType<ViewResult>(actionResult);
		}

		private static HomeController GetHomeController() =>
			new(new Logger<HomeController>(new LoggerFactory()));
	}
}
