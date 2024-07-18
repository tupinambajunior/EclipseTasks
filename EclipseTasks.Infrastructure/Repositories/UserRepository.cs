﻿using EclipseTasks.Core.Entities;
using EclipseTasks.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EclipseTasks.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly PgContext _dbContext;
        public UserRepository(PgContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}