namespace Epam.AzureWorkShop.Dal.Implementations
{
	public interface IServiceBusQueue
	{
		void Add<T>(T value);

		T Get<T>();
	}
}