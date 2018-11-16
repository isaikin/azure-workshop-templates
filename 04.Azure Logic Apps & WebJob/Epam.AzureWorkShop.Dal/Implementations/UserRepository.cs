using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Epam.AzureWorkShop.Dal.Interfaces;
using Epam.AzureWorkShop.Entities;

namespace Epam.AzureWorkShop.Dal.Implementations
{
    public class UserRepository : IRepository<UserCredentials>
    {
        public UserCredentials Add(UserCredentials item)
        {
            item.Id = Guid.NewGuid();
            using (var context = new SqlContext())
            {
                context.Users.Add(item);
                context.SaveChanges();
            }

            return item;
        }

        public IEnumerable<UserCredentials> GetAll()
        {
            using (var context = new SqlContext())
            {
                return context.Users.ToList();
            }
        }

        public void Delete(Guid id)
        {
            using (var context = new SqlContext())
            {
                var user = new UserCredentials
                {
                    Id = id,
                };

                context.Users.Attach(user);
                context.Users.Remove(user);

                context.SaveChanges();
            }
        }

        public UserCredentials GetById(Guid id)
        {
            using (var context = new SqlContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Id == id);

                return user;
            }
        }
    }
}