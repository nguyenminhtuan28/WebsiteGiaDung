using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks; 

namespace WEBPROJECT.Repository.Components
{
	public class CategoriesViewComponent : ViewComponent
	{
		private readonly Datacontext _datacontext;

		public CategoriesViewComponent(Datacontext context)
		{
			_datacontext = context;
		}
		public async Task<IViewComponentResult> InvokeAsync() => View(await _datacontext.Categories.ToListAsync());
		
	}
}
