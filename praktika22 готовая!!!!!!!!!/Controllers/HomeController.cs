using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace praktika22.Controllers
{
	public class HomeController: Controller
	{
		public RedirectResult Index() {
			return Redirect("/Items/List");
		}
	}
}
