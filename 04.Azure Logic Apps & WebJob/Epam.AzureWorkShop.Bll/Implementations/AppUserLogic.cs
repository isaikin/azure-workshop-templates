using Epam.AzureWorkShop.Dal.Interfaces;
using Epam.AzureWorkShop.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Epam.AzureWorkShop.Bll.Implementations
{
	public class AppUserLogic : IAppUserLogic
	{
		private readonly IRepository<UserCredentials> _userRepository;

		public AppUserLogic(IRepository<UserCredentials> userRepository)
		{
			_userRepository = userRepository;
		}

		public void Add(UserCredentials userCredentials)
		{
			userCredentials.Id = Guid.NewGuid();
			_userRepository.Add(userCredentials);
		}

		public bool IsAuthenticate(UserCredentials userCredentials)
		{
			userCredentials.Id = Guid.NewGuid();
			var currentUser = _userRepository.GetAll().FirstOrDefault(item => item.Username.ToUpper() == userCredentials.Username.ToUpper());

			if (currentUser != null)
			{
				return userCredentials.Password == currentUser.Password;
			}

			return false;
		}
	}
}
