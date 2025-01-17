﻿using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.DAL.EF;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Orders;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessOrder.DAL.Repositories
{
    public class ClientRepository : RepositoryBase, IClientRepository
    {
        public ClientRepository(ContactlessOrderContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Catering>> GetCateringsByCoordinates(CoordinateDto from, CoordinateDto to)
        {
            return await Context.Set<Catering>()
                .Include(e => e.Coordinates)
                .Include(e => e.Company)
                .Include(e => e.MenuOptions)
                .ThenInclude(e => e.MenuOption.MenuItem)
                .Where(e => e.Company.ApprovedDate.HasValue
                    && (e.FullDay || e.OpenTime.Value < DateTime.Now.TimeOfDay && e.CloseTime.Value > DateTime.Now.TimeOfDay)
                    && e.Coordinates.Lat >= from.Lat && e.Coordinates.Lat <= to.Lat
                    && e.Coordinates.Lng >= from.Lng && e.Coordinates.Lng <= to.Lng)
                .ToListAsync();
        }

        public async Task<Order> GetOrder(int id)
        {
            return await Context.Set<Order>()
                .Include(e => e.Status)
                .Include(e => e.User)
                .Include(e => e.PaymentMethod)
                .Include(e => e.Positions)
                .ThenInclude(e => e.Option.MenuOption.MenuItem.Pictures)
                .Include(e => e.Positions)
                .ThenInclude(e => e.Option.Catering.User)
                .Include(e => e.Positions)
                .ThenInclude(e => e.Modifications)
                .ThenInclude(e => e.Modification)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrders(int userId)
        {
            return await Context.Set<Order>()
                .Include(e => e.Status)
                .Include(e => e.User)
                .Include(e => e.PaymentMethod)
                .Include(e => e.Positions)
                .ThenInclude(e => e.Option.MenuOption.MenuItem.Pictures)
                .Include(e => e.Positions)
                .ThenInclude(e => e.Option.Catering.User)
                .Include(e => e.Positions)
                .ThenInclude(e => e.Modifications)
                .ThenInclude(e => e.Modification)
                .Where(e => e.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetNotFinishedOrders(int userId)
        {
            return await Context.Set<Order>()
                .Include(e => e.Status)
                .Where(e => e.UserId == userId
                    && e.Status.Value != OrderStatuses.DoneStatusValue
                    && e.Status.Value != OrderStatuses.RejectedStatusValue)
                .ToListAsync();
        }

        public async Task<Catering> GetCatering(int id)
        {
            return await Context.Set<Catering>()
                .Include(e => e.Company)
                .Include(e => e.Coordinates)
                .Include(e => e.MenuOptions)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}
