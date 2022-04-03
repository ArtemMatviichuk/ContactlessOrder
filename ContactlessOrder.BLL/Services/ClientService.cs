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

        public async Task<IEnumerable<CartOptionDto>> GetCartData(IEnumerable<int> itemIds)
        {
            if (itemIds != null && itemIds.Any())
            {
                var options = await _cateringRepository.GetMenuOptions(itemIds);

                var dtos = _mapper.Map<IEnumerable<CartOptionDto>>(options);

                return dtos;
            }
            else return Array.Empty<CartOptionDto>();
        }

        public async Task<IEnumerable<AttachmentDto>> GetMenuPictures(int id)
        {
            var pictures = await _cateringRepository.GetMenuItemPictures(id);

            var dtos = _mapper.Map<IEnumerable<AttachmentDto>>(pictures);

            return dtos;
        }
    }
}
