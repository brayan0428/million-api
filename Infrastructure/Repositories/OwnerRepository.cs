﻿using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class OwnerRepository : BaseRepository<Owner> , IOwnerRepository
    {
        public OwnerRepository(MillionDbContext context) : base(context)
        {
        }
    }
}
