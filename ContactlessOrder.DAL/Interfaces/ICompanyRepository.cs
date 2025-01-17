﻿using ContactlessOrder.DAL.Entities.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Interfaces
{
    public interface ICompanyRepository : IRepositoryBase
    {
        Task<Company> GetCompany(int userId);
        Task<IEnumerable<Catering>> GetCaterings(int userId);
        Task<Catering> GetCatering(int id);
        Task<IEnumerable<MenuItem>> GetMenuItems(int userId);
    }
}
