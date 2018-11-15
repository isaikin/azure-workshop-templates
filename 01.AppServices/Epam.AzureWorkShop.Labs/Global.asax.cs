using Ninject.Web.Common.WebHost;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Epam.AzureWorkShop.Bll.Implementations;
using Epam.AzureWorkShop.Bll.Interfaces;
using Epam.AzureWorkShop.Dal.Implementations;
using Epam.AzureWorkShop.Dal.Interfaces;
using Epam.AzureWorkShop.Entities;
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
			kernel.Bind<IRepository<UserCredentials>>().To<UserRepository>().InSingletonScope();
			kernel.Bind<IRepository<ImageMetadata>>().To<MetadataRepository>().InSingletonScope();
			kernel.Bind<IRepository<Note>>().To<NotesRepository>().InSingletonScope();
			kernel.Bind<IRepository<Image>>().To<ImageRepository>().InSingletonScope();
			
			kernel.Bind<IImageLogic>().To<ImageLogic>();
			kernel.Bind<INoteLogic>().To<NoteLogic>();
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
