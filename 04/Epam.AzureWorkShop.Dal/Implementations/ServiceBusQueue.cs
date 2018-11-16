using Epam.AzureWorkShop.Entities;
using Microsoft.Azure.ServiceBus;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Epam.AzureWorkShop.Dal.Implementations
{
	public class ServiceBusQueue: IServiceBusQueue, IDisposable
	{
		private readonly QueueClient queueClient;

		public ServiceBusQueue()
		{
			this.queueClient = new QueueClient(ConfigurationManager.ConnectionStrings["ServiceBusQueue"].ConnectionString, ConfigurationManager.AppSettings["NameQueue"]);
		}

		public void Add<T>(T value)
		{
			var json = Newtonsoft.Json.JsonConvert.SerializeObject(value);
			var message = new Message(Encoding.UTF8.GetBytes(json));

			queueClient.SendAsync(message).Wait();
		}

		public void Dispose()
		{
			queueClient.CloseAsync().Start();
		}

		public T Get<T>()
		{
			var messageHandlerOptions = new MessageHandlerOptions(ExceptionReceivedHandler)
			{
				MaxConcurrentCalls = 1,

				AutoComplete = false
			};
			Message currentMessage = null;
			queueClient.RegisterMessageHandler((m,c) =>
			{
				currentMessage = m;
				return queueClient.CompleteAsync(m.SystemProperties.LockToken);
			}, messageHandlerOptions);

			return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(currentMessage.Body));
		}

		static Task ExceptionReceivedHandler(ExceptionReceivedEventArgs exceptionReceivedEventArgs)
		{
			
			var context = exceptionReceivedEventArgs.ExceptionReceivedContext;
			
			return Task.CompletedTask;
		}
	}
}
