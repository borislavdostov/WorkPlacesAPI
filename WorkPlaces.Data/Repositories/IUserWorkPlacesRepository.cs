﻿using System;
using System.Linq;
using System.Threading.Tasks;
using WorkPlaces.Data.Entities;

namespace WorkPlaces.Data.Repositories
{
    public interface IUserWorkplacesRepository : IDisposable
    {
        IQueryable<UserWorkplace> GetAll();

        Task<UserWorkplace> GetAsync(int userWorkplaceId);

        Task AddAsync(UserWorkplace userWorkplace);

        void Update(UserWorkplace userWorkplace);

        void Delete(UserWorkplace userWorkplace);

        Task<bool> ExistsAsync(int userWorkplaceId);

        Task SaveChangesAsync();
    }
}
