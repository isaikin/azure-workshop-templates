using Ninject.Web.Common.WebHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Epam.AzureWorkShop.Entities;
using Epam.AzureWorkShop.Labs.Models;
using Epam.AzureWorkShop.Labs.Models.Interfaces;
using Epam.AzureWorkShop.Labs.Models.Repositories;
using Ninject;

namespace Epam.AzureWorkShop.Labs
{
    public class MvcApplication : NinjectHttpApplication
	{
		public override void Init()
		{
			base.Init();
		}

		protected override IKernel CreateKernel()
		{
			var kernel = new StandardKernel();
			Registration(kernel);

			return kernel;

		}

		private void Registration(IKernel kernel)
		{
			kernel.Bind<IRepository<Note>>().To<FakeRepository<Note>>().InSingletonScope();
			kernel.Bind<IRepository<Image>>().To<FakeRepository<Image>>().InSingletonScope();
			kernel.Bind<IImageModels>().To<ImageModels>();
			kernel.Bind<INoteModels>().To<NoteModels>();
		}

		protected override void OnApplicationStarted()
		{
			base.OnApplicationStarted();

			AreaRegistration.RegisterAllAreas();
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}
}
