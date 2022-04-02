﻿using AutoMapper;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Clients;
using ContactlessOrder.Common.Dto.Companies;
using ContactlessOrder.DAL.Interfaces;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ContactlessOrder.Common.Dto.Common;

namespace ContactlessOrder.BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ICateringRepository _cateringRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper, ICateringRepository cateringRepository)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _cateringRepository = cateringRepository;
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

        public async Task GetCartData(IEnumerable<CartItem> items)
        {
            var options = await _cateringRepository.GetMenuOptions(items.Select(e => e.Id));
        }
    }
}
