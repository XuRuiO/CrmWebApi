using CRM.IRepository.IRepositorys;
using CRM.IRepository.IUnitOfWork;
using CRM.Model.Models;
using CRM.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.Repository.Repositorys
{
    public class PermissionRepository : BaseRepository<Permission>, IPermissionRepository
    {
        public PermissionRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
    }
}

