using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Bll.Implementations
{
	public interface IAppUserLogic
	{
		void Add(UserCredentials userCredentials);

		bool IsAuthenticate(UserCredentials userCredentials);
	}
}