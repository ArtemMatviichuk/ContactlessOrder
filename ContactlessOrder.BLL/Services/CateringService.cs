using AutoMapper;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Caterings;
using ContactlessOrder.Common.Dto.Common;
using ContactlessOrder.Common.Dto.Orders;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.DAL.Entities.Orders;
using ContactlessOrder.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class CateringService : ICateringService
    {
        private readonly INotificationService _notificationService;
        private readonly ICommonService _commonService;
        private readonly ICateringRepository _cateringRepository;
        private readonly IMapper _mapper;

        public CateringService(ICateringRepository cateringRepository, IMapper mapper, ICommonService commonService, INotificationService notificationService)
        {
            _cateringRepository = cateringRepository;
            _mapper = mapper;
            _commonService = commonService;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<CateringMenuOptionDto>> GetMenu(int cateringId)
        {
            var menu = await _cateringRepository.GetMenu(cateringId);

            var dtos = _mapper.Map<IEnumerable<CateringMenuOptionDto>>(menu);

            return dtos.OrderBy(e => e.Name);
        }

        public async Task<string> UpdateMenuOption(int id, UpdateCateringMenuOptionDto dto)
        {
            var option = await _cateringRepository.GetMenuOption(id);

            if (option != null)
            {
                option.Available = dto.Available;
                option.InheritPrice = dto.InheritPrice;
                option.Price = dto.InheritPrice ? null : dto.Price;

                await _cateringRepository.SaveChanges();
                return string.Empty;
            }

            return "Опція не знайдена";
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(int cateringId)
        {
            var orders = await _cateringRepository.GetOrders(cateringId);

            return await MapOrders(orders.Where(e => e.Status.Value != OrderStatuses.CreatedStatusValue
                    && e.Status.Value != OrderStatuses.RejectedStatusValue
                    && e.Status.Value != OrderStatuses.DoneStatusValue));
        }

        public async Task<IEnumerable<OrderDto>> GetEndedOrders(int cateringId)
        {
            var orders = await _cateringRepository.GetOrders(cateringId);

            return await MapOrders(orders.Where(e =>
                    e.Status.Value == OrderStatuses.RejectedStatusValue
                    || e.Status.Value == OrderStatuses.DoneStatusValue));
        }

        public async Task<IEnumerable<CateringModificationDto>> GetModifications(int cateringId)
        {
            return await _commonService.GetCateringModifications(cateringId);
        }

        public async Task UpdateModification(int id, int cateringId, UpdateCateringMenuOptionDto dto)
        {
            var modification = await _cateringRepository.Get<CateringModification>(e => e.CateringId == cateringId && e.ModificationId == id);

            if (modification != null)
            {
                modification.Available = dto.Available;
                modification.InheritPrice = dto.InheritPrice;
                modification.Price = dto.InheritPrice ? null : dto.Price;
            }
            else
            {
                modification = new CateringModification()
                {
                    Price = dto.Price,
                    Available = dto.Available,
                    InheritPrice = dto.InheritPrice,
                    CateringId = cateringId,
                    ModificationId = id,
                };

                await _cateringRepository.Add(modification);
            }

            await _cateringRepository.SaveChanges();
        }

        public async Task<IEnumerable<IdNameValueDto>> GetOrderStatuses()
        {
            var statuses = await _cateringRepository.GetAll<OrderStatus>();
            var dtos = _mapper.Map<IEnumerable<IdNameValueDto>>(statuses);
            return dtos;
        }

        public async Task UpdateOrderStatus(int orderId, int statusId)
        {
            var order = await _cateringRepository.Get<Order>(orderId);

            if (order != null)
            {
                var status = await _cateringRepository.Get<OrderStatus>(statusId);
                if (status != null)
                {
                    order.StatusId = statusId;
                    order.ModifiedDate = DateTime.Now;

                    await _cateringRepository.SaveChanges();

                    await _notificationService.NotifyOrderUpdated(orderId, await _commonService.GetOrderTotalPrice(orderId, AppConstants.ViewAll));

                    if (status.Value == OrderStatuses.ReadyStatusValue)
                    {
                        await _notificationService.NotifyOrderReady(orderId);
                    }
                }
            }
        }

        private async Task<IEnumerable<OrderDto>> MapOrders(IEnumerable<Order> orders)
        {
            var dtos = _mapper.Map<IEnumerable<OrderDto>>(orders);

            foreach (var item in dtos)
            {
                item.TotalPrice = await _commonService.GetOrderTotalPrice(item.Id, AppConstants.ViewAll);
            }

            return dtos.OrderBy(e => e.StatusValue == OrderStatuses.ReadyStatusValue)
                .ThenBy(e => e.StatusValue == OrderStatuses.OnHoldStatusValue)
                .ThenBy(e => e.StatusValue == OrderStatuses.InProgressStatusValue)
                .ThenBy(e => e.StatusValue == OrderStatuses.PendingStartStatusValue)
                .ThenByDescending(e => e.CreatedDate);
        }
    }
}
