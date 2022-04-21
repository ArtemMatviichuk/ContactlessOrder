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

namespace ContactlessOrder.BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly INotificationService _notificationService;
        private readonly IClientRepository _clientRepository;
        private readonly ICateringRepository _cateringRepository;
        private readonly IMapper _mapper;
        public readonly IHubContext<OrdersHub, IOrdersHubClient> _ordersHub;

        public ClientService(IClientRepository clientRepository, IMapper mapper, ICateringRepository cateringRepository,
            IHubContext<OrdersHub, IOrdersHubClient> ordersHub, INotificationService notificationService)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _cateringRepository = cateringRepository;
            _ordersHub = ordersHub;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<ClientCateringDto>> GetCaterings(GetCateringsDto dto)
        {
            var caterings = await _clientRepository.GetCateringsByCoordinates(dto.From, dto.To);
            var dtos = _mapper.Map<IEnumerable<ClientCateringDto>>(caterings);

            return dtos;
        }

        public async Task<IEnumerable<ClientMenuPositionDto>> GetCateringMenu(int cateringId)
        {
            var menu = await _cateringRepository.GetMenu(cateringId);
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
                    }),
                });

            return dtos;
        }

        public async Task<IEnumerable<CartOptionDto>> GetCartData(IEnumerable<IdValueDto<int>> itemIds)
        {
            if (itemIds != null && itemIds.Any())
            {
                var options = new List<CateringMenuOption>();

                foreach (var item in itemIds.GroupBy(e => e.Id))
                {
                    var newOptions = await _cateringRepository.GetMenuOptions(item.Key, item.Select(e => e.Value));
                    options.AddRange(newOptions);
                }

                var dtos = _mapper.Map<IEnumerable<CartOptionDto>>(options);

                return dtos;
            }
            else return Array.Empty<CartOptionDto>();
        }

        public async Task<IEnumerable<OrderDto>> GetOrders(int userId)
        {
            var orders = await _clientRepository.GetOrders(userId);

            var dtos = _mapper.Map<IEnumerable<OrderDto>>(orders.OrderByDescending(e => e.CreatedDate));

            return dtos;
        }

        public async Task<IEnumerable<AttachmentDto>> GetMenuPictures(int id)
        {
            var pictures = await _cateringRepository.GetMenuItemPictures(id);

            var dtos = _mapper.Map<IEnumerable<AttachmentDto>>(pictures);

            return dtos;
        }

        public async Task<int> CreateOrder(int userId, CreateOrderDto dto)
        {
            var status = await _clientRepository.Get<OrderStatus>(e => e.Value == OrderStatuses.CreatedStatusValue);

            Order order = new ()
            {
                Comment = dto.Comment,
                StatusId = status.Id,
                UserId = userId,
                CreatedDate = DateTime.Now,
                Positions = dto.Positions.Select(p => new OrderPosition()
                {
                    Quantity = p.Quantity,
                    OptionId = p.OptionId,
                }).ToList()
            };

            await _clientRepository.Add(order);
            await _clientRepository.SaveChanges();

            return order.Id;
        }

        public async Task OrderPaid(IdNameDto dto)
        {
            var order = await _clientRepository.Get<Order>(dto.Id);

            if (order != null)
            {
                var status = await _clientRepository.Get<OrderStatus>(e => e.Value == OrderStatuses.PaidStatusValue);
                order.StatusId = status.Id;
                order.PaymentNumber = dto.Name;
                order.ModifiedDate = DateTime.Now;

                await _clientRepository.SaveChanges();

                await _notificationService.NotifyOrderUpdated(dto.Id);
            }
        }

        public async Task<int> GetOrderTotalPrice(int id, int userId)
        {
            var order = await _clientRepository.GetOrder(id);

            if (order != null && order.UserId == userId)
            {
                return order.Positions.Select(e => (e.Option.InheritPrice ? e.Option.MenuOption.Price : e.Option.Price.Value) * e.Quantity).Sum();
            }

            return -1;
        }
    }
}
