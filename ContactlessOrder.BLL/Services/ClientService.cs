using AutoMapper;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Clients;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactlessOrder.Common.Dto.Common;
using System;
using ContactlessOrder.DAL.Entities.Companies;
using ContactlessOrder.Common.Dto.Orders;
using ContactlessOrder.DAL.Entities.Orders;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.BLL.HubConnections.HubClients;
using ContactlessOrder.BLL.HubConnections.Hubs;
using Microsoft.AspNetCore.SignalR;
using ContactlessOrder.Common.Dto.Caterings;

namespace ContactlessOrder.BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly ICommonService _commonService;
        private readonly INotificationService _notificationService;
        private readonly IClientRepository _clientRepository;
        private readonly ICateringRepository _cateringRepository;
        private readonly IMapper _mapper;
        public readonly IHubContext<OrdersHub, IOrdersHubClient> _ordersHub;

        public ClientService(IClientRepository clientRepository, IMapper mapper, ICateringRepository cateringRepository,
            IHubContext<OrdersHub, IOrdersHubClient> ordersHub, INotificationService notificationService, ICommonService commonService)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _cateringRepository = cateringRepository;
            _ordersHub = ordersHub;
            _notificationService = notificationService;
            _commonService = commonService;
        }

        public async Task<IEnumerable<ClientCateringDto>> GetCaterings(GetCateringsDto dto, string search)
        {
            var caterings = await _clientRepository.GetCateringsByCoordinates(dto.From, dto.To);
            
            if (!string.IsNullOrEmpty(search))
            {
                caterings = DoFilter(caterings, search);
            }

            var dtos = _mapper.Map<IEnumerable<ClientCateringDto>>(caterings);

            return dtos;
        }

        public async Task<IEnumerable<ClientMenuPositionDto>> GetCateringMenu(int cateringId)
        {
            var menu = await _cateringRepository.GetMenu(cateringId);
            var modifications = await _commonService.GetCateringModifications(cateringId);

            var dtos = menu.GroupBy(e => e.MenuOption.MenuItemId)
                .Select(e => e.ToList())
                .Select(e => new ClientMenuPositionDto()
                { 
                    Name = e.FirstOrDefault().MenuOption.MenuItem.Name,
                    Description = e.FirstOrDefault().MenuOption.MenuItem.Description,
                    FirstPictureId = e.FirstOrDefault().MenuOption.MenuItem.Pictures.FirstOrDefault()?.Id,
                    Options = e.Select(option => new ClientMenuOptionDto()
                    {
                        Id = option.MenuOption.Id,
                        Available = option.Available,
                        Price = option.Price ?? option.MenuOption.Price,
                        Name = option.MenuOption.Name
                    }).OrderBy(e => e.Price),
                    Modifications = modifications.Where(m =>
                        e.SelectMany(e => e.MenuOption.MenuItem.MenuItemModifications)
                        .Select(e => e.ModificationId)
                        .Contains(m.Id))
                        .OrderBy(e => e.Price),
                }).OrderBy(e => e.Name);

            return dtos;
        }

        public async Task<IEnumerable<CartOptionDto>> GetCartData(IEnumerable<GetCartDto> items)
        {
            if (items != null && items.Any())
            {
                var dtos = new List<CartOptionDto>();

                foreach (var item in items.GroupBy(e => e.CateringId))
                {
                    var options = await _cateringRepository.GetMenuOptions(item.Key, item.Select(e => e.Id));
                    var modifications = await _commonService.GetCateringModifications(item.Key);

                    var newDtos = _mapper.Map<IEnumerable<CartOptionDto>>(options);

                    foreach (var dto in newDtos)
                    {
                        var option = options.FirstOrDefault(e => e.MenuOption.Id == dto.Id);
                        dto.Modifications = modifications.Where(m =>
                            option.MenuOption.MenuItem.MenuItemModifications
                            .Select(e => e.ModificationId)
                            .Contains(m.Id))
                            .OrderBy(e => e.Price);
                    }

                    dtos.AddRange(newDtos);
                }

                return dtos;
            }
            else return Array.Empty<CartOptionDto>();
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(int userId)
        {
            var orders = await _clientRepository.GetOrders(userId);

            var dtos = _mapper.Map<IEnumerable<OrderDto>>(orders.OrderByDescending(e => e.CreatedDate));

            foreach (var item in dtos)
            {
                item.TotalPrice = await GetOrderTotalPrice(item.Id, userId);
            }

            return dtos;
        }

        public async Task<IEnumerable<IdNameValueDto>> GetPaymentMethods()
        {
            var items = await _clientRepository.GetAll<PaymentMethod>();
            var dtos = _mapper.Map<IEnumerable<IdNameValueDto>>(items);
            return dtos;
        }

        public async Task<IEnumerable<AttachmentDto>> GetMenuPictures(int id)
        {
            var pictures = await _cateringRepository.GetMenuItemPictures(id);

            var dtos = _mapper.Map<IEnumerable<AttachmentDto>>(pictures);

            return dtos;
        }

        public async Task<ResponseDto<int>> CreateOrder(int userId, CreateOrderDto dto)
        {
            var notFinishedOrders = await _clientRepository.GetNotFinishedOrders(userId);

            if (notFinishedOrders.Count() >= AppConstants.NotFinishedThreshold)
            {
                return new ResponseDto<int>() { ErrorMessage = "Досягнутий ліміт незавершених замовлень" };
            }

            var status = await _clientRepository.Get<OrderStatus>(e => e.Value == OrderStatuses.CreatedStatusValue);
            var payment = await _clientRepository.Get<PaymentMethod>(e => e.Value == dto.PaymentMethodValue);

            Order order = new ()
            {
                Comment = dto.Comment,
                StatusId = status.Id,
                UserId = userId,
                CreatedDate = DateTime.Now,
                PaymentMethodId = payment.Id,
                Positions = dto.Positions.Select(p => new OrderPosition()
                {
                    Quantity = p.Quantity,
                    OptionId = p.OptionId,
                    Modifications = p.ModificationIds.Select(m => new OrderPositionModification()
                    {
                        ModificationId = m,
                    }).ToList()
                }).ToList()
            };

            await _clientRepository.Add(order);
            await _clientRepository.SaveChanges();

            if (dto.PaymentMethodValue == PaymentMethods.Cash)
            {
                await OrderPaid(new IdNameDto() { Id = order.Id, Name = null });
            }

            return new ResponseDto<int>() { Response = order.Id };
        }

        public async Task OrderPaid(IdNameDto dto)
        {
            var order = await _clientRepository.GetOrder(dto.Id);

            if (order != null)
            {
                var status = await _clientRepository.Get<OrderStatus>(e => e.Value == OrderStatuses.PendingStartStatusValue);
                order.StatusId = status.Id;
                order.PaymentNumber = dto.Name;
                order.ModifiedDate = DateTime.Now;

                var cateringId = order.Positions.First().Option.CateringId;
                var modifications = await _commonService.GetCateringModifications(cateringId);

                foreach (var position in order.Positions)
                {
                    position.OptionName = $"{position.Option.MenuOption.MenuItem.Name} ({ position.Option.MenuOption.Name})";
                    position.InMomentPrice = position.Option.InheritPrice
                        ? position.Option.MenuOption.Price
                        : position.Option.Price.Value;

                    foreach (var modification in position.Modifications)
                    {
                        modification.InMomentPrice = modifications.First(m => m.Id == modification.ModificationId).Price;
                        modification.ModificationName = modification.Modification.Name;
                    }
                }

                await _clientRepository.SaveChanges();

                await _notificationService.NotifyOrderPaid(dto.Id, await _commonService.GetOrderTotalPrice(dto.Id, AppConstants.ViewAll));
            }
        }

        public async Task RejectOrder(int id, int userId)
        {
            var order = await _cateringRepository.Get<Order>(id);

            if (order == null || order.UserId != userId)
            {
                return;
            }

            var status = await _cateringRepository.Get<OrderStatus>(e => e.Value == OrderStatuses.RejectedStatusValue);
            order.StatusId = status.Id;
            order.ModifiedDate = DateTime.Now;

            await _cateringRepository.SaveChanges();

            await _notificationService.NotifyOrderRejected(id, await _commonService.GetOrderTotalPrice(id, AppConstants.ViewAll));
        }

        public async Task CompleteOrder(int id, int userId)
        {
            var order = await _cateringRepository.Get<Order>(id);

            if (order == null || order.UserId != userId)
            {
                return;
            }

            var status = await _cateringRepository.Get<OrderStatus>(e => e.Value == OrderStatuses.DoneStatusValue);
            order.StatusId = status.Id;
            order.ModifiedDate = DateTime.Now;

            await _cateringRepository.SaveChanges();

            await _notificationService.NotifyOrderCompleted(id, await _commonService.GetOrderTotalPrice(id, AppConstants.ViewAll));
        }

        public Task<int> GetOrderTotalPrice(int id, int userId)
        {
            return _commonService.GetOrderTotalPrice(id, userId);
        }

        public async Task<CateringDto> GetCatering(int orderId)
        {
            var order = await _clientRepository.GetOrder(orderId);
            var catering = await _clientRepository.GetCatering(order.Positions.First().Option.CateringId);
            return _mapper.Map<CateringDto>(catering);
        }

        private IEnumerable<Catering> DoFilter(IEnumerable<Catering> caterings, string search)
        {
            var lowerSearch = search.ToLower();
            return caterings.Where(catering => (catering.Company.Name ?? string.Empty).ToLower().Contains(lowerSearch)
                || (catering.Company.Description ?? string.Empty).ToLower().Contains(lowerSearch)
                || (catering.Name ?? string.Empty).ToLower().Contains(lowerSearch)
                || (catering.Services ?? string.Empty).ToLower().Contains(lowerSearch)
                || catering.MenuOptions.Select(e => e.MenuOption.MenuItem.Name?.ToLower()).Any(e => e.Contains(lowerSearch)))
                .ToList();
        }
    }
}
