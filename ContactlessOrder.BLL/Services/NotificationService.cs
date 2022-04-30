﻿﻿using AutoMapper;
using ContactlessOrder.BLL.HubConnections.HubClients;
using ContactlessOrder.BLL.HubConnections.Hubs;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Orders;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;
        public readonly IHubContext<OrdersHub, IOrdersHubClient> _ordersHub;

        public NotificationService(IClientRepository clientRepository, IMapper mapper, IHubContext<OrdersHub, IOrdersHubClient> ordersHub)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _ordersHub = ordersHub;
        }

        public async Task NotifyOrderPaid(int id, int totalPrice)
        {
            var order = await _clientRepository.GetOrder(id);
            var dto = _mapper.Map<OrderDto>(order);
            dto.TotalPrice = totalPrice;

            await _ordersHub.Clients.User($"{NotificationConstants.UserPrefix}{order.UserId}").OrderUpdated(dto);
            await _ordersHub.Clients.User($"{NotificationConstants.CateringPrefix}{order.Positions.First().Option.CateringId}").OrderPaid(dto);
        }

        public async Task NotifyOrderUpdated(int id, int totalPrice)
        {
            var order = await _clientRepository.GetOrder(id);
            var dto = _mapper.Map<OrderDto>(order);
            dto.TotalPrice = totalPrice;

            await _ordersHub.Clients.User($"{NotificationConstants.UserPrefix}{order.UserId}").OrderUpdated(dto);
            await _ordersHub.Clients.User($"{NotificationConstants.CateringPrefix}{order.Positions.First().Option.CateringId}").OrderUpdated(dto);
        }

        public async Task NotifyOrderReady(int id)
        {
            var order = await _clientRepository.GetOrder(id);
            await _ordersHub.Clients.User($"{NotificationConstants.UserPrefix}{order.UserId}").OrderReady($"#{order.Id:d16}");
        }

        public async Task NotifyOrderRejected(int id, int totalPrice)
        {
            var order = await _clientRepository.GetOrder(id);
            var dto = _mapper.Map<OrderDto>(order);
            dto.TotalPrice = totalPrice;

            await _ordersHub.Clients.User($"{NotificationConstants.UserPrefix}{order.UserId}").OrderUpdated(dto);
            await _ordersHub.Clients.User($"{NotificationConstants.CateringPrefix}{order.Positions.First().Option.CateringId}").OrderRejected(dto);
        }

        public async Task NotifyOrderCompleted(int id, int totalPrice)
        {
            var order = await _clientRepository.GetOrder(id);
            var dto = _mapper.Map<OrderDto>(order);
            dto.TotalPrice = totalPrice;

            await _ordersHub.Clients.User($"{NotificationConstants.UserPrefix}{order.UserId}").OrderUpdated(dto);
            await _ordersHub.Clients.User($"{NotificationConstants.CateringPrefix}{order.Positions.First().Option.CateringId}").OrderCompleted(dto);
        }
    }
}