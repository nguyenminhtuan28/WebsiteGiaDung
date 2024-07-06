using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WEBPROJECT.Models; 

namespace WEBPROJECT.Repository.Components
{
	public class BrandsViewComponent : ViewComponent
	{
		private readonly Datacontext _datacontext;

		public BrandsViewComponent(Datacontext context)
		{
			_datacontext = context;
		}

		public async Task<IViewComponentResult> InvokeAsync() => View(await _datacontext.Brands.ToListAsync());

	}
}
