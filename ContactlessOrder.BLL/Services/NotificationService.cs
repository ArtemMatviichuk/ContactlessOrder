﻿using AutoMapper;
using ContactlessOrder.BLL.HubConnections.HubClients;
using ContactlessOrder.BLL.HubConnections.Hubs;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Constants;
using ContactlessOrder.Common.Dto.Auth;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.Common.Dto.Orders;
using ContactlessOrder.DAL.Entities.Users;
using ContactlessOrder.DAL.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ISupportRepository _supportRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<OrdersHub, IOrdersHubClient> _ordersHub;
        private readonly IHubContext<SupportHub, ISupportHubClient> _supportHub;
        private readonly IHubContext<AdminHub, IAdminHubClient> _adminHub;

        public NotificationService(IClientRepository clientRepository, IMapper mapper, ISupportRepository supportRepository,
            IHubContext<OrdersHub, IOrdersHubClient> ordersHub, IHubContext<SupportHub, ISupportHubClient> supportHub,
            IHubContext<AdminHub, IAdminHubClient> adminHub, ICompanyRepository companyRepository)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _ordersHub = ordersHub;
            _supportRepository = supportRepository;
            _supportHub = supportHub;
            _adminHub = adminHub;
            _companyRepository = companyRepository;
        }

        public async Task NotifyOrderPaid(int id, int totalPrice)
        {
            var order = await _clientRepository.GetOrder(id);
            var dto = _mapper.Map<OrderDto>(order);
            dto.TotalPrice = totalPrice;

            await _ordersHub.Clients.User($"{UserRoles.ClientName}.{order.UserId}").OrderUpdated(dto);
            await _ordersHub.Clients.User($"{UserRoles.CateringName}.{order.Positions.First().Option.Catering.UserId}").OrderPaid(dto);
        }

        public async Task NotifyOrderUpdated(int id, int totalPrice)
        {
            var order = await _clientRepository.GetOrder(id);
            var dto = _mapper.Map<OrderDto>(order);
            dto.TotalPrice = totalPrice;

            await _ordersHub.Clients.User($"{UserRoles.ClientName}.{order.UserId}").OrderUpdated(dto);
            await _ordersHub.Clients.User($"{UserRoles.CateringName}.{order.Positions.First().Option.Catering.UserId}").OrderUpdated(dto);
        }

        public async Task NotifyOrderReady(int id)
        {
            var order = await _clientRepository.GetOrder(id);
            await _ordersHub.Clients.User($"{UserRoles.ClientName}.{order.UserId}").OrderReady($"#{order.Id:d16}");
        }

        public async Task NotifyOrderRejected(int id, int totalPrice)
        {
            var order = await _clientRepository.GetOrder(id);
            var dto = _mapper.Map<OrderDto>(order);
            dto.TotalPrice = totalPrice;

            await _ordersHub.Clients.User($"{UserRoles.ClientName}.{order.UserId}").OrderUpdated(dto);
            await _ordersHub.Clients.User($"{UserRoles.CateringName}.{order.Positions.First().Option.Catering.UserId}").OrderRejected(dto);
        }

        public async Task NotifyOrderCompleted(int id, int totalPrice)
        {
            var order = await _clientRepository.GetOrder(id);
            var dto = _mapper.Map<OrderDto>(order);
            dto.TotalPrice = totalPrice;

            await _ordersHub.Clients.User($"{UserRoles.ClientName}.{order.UserId}").OrderUpdated(dto);
            await _ordersHub.Clients.User($"{UserRoles.CateringName}.{order.Positions.First().Option.Catering.UserId}").OrderCompleted(dto);
        }

        public async Task NotifyComplainAdded(int id)
        {
            var complain = await _supportRepository.GetComplain(id);
            var dto = _mapper.Map<ComplainDto>(complain);
            await _supportHub.Clients.All.ComplainAdded(dto);
        }

        public async Task NotifyComplainUpdated(int id)
        {
            var complain = await _supportRepository.GetComplain(id);
            var dto = _mapper.Map<ComplainDto>(complain);
            await _supportHub.Clients.All.ComplainUpdated(dto);
        }

        public async Task NotifyCompanyAdded(int userId)
        {
            var company = await _companyRepository.GetCompany(userId);
            var dto = _mapper.Map<CompanyDto>(company);
            await _adminHub.Clients.All.CompanyCreated(dto);
        }

        public async Task NotifyCompanyUpdated(int userId)
        {
            var company = await _companyRepository.GetCompany(userId);
            var dto = _mapper.Map<CompanyDto>(company);
            await _adminHub.Clients.All.CompanyUpdated(dto);
        }

        public async Task NotifyUserRegistered(int id)
        {
            var user = await _supportRepository.GetUser(id);
            var dto = _mapper.Map<UserDto>(user);
            await _adminHub.Clients.All.UserRegistered(dto);
        }

        public async Task NotifyUserUpdated(int id)
        {
            var user = await _supportRepository.GetUser(id);
            var dto = _mapper.Map<UserDto>(user);
        }

        public async Task NotifyUserDeleted(int id)
        {
            await _adminHub.Clients.All.UserDeleted(id);
        }
    }
}