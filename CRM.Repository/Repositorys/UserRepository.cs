﻿using CRM.IRepository.IRepositorys;
using CRM.IRepository.IUnitOfWork;
using CRM.Model.Models;
using CRM.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Repository.Repositorys
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}
