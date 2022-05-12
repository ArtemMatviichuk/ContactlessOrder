using AutoMapper;
using ContactlessOrder.BLL.Interfaces;
using ContactlessOrder.Common.Dto.Orders;
using ContactlessOrder.DAL.Entities.Users;
using ContactlessOrder.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Services
{
    public class SupportService : ISupportService
    {
        private readonly ISupportRepository _supportRepository;
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public SupportService(ISupportRepository supportRepository, IMapper mapper, INotificationService notificationService)
        {
            _supportRepository = supportRepository;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<ComplainDto>> GetComplains(int statusValue)
        {
            var complains = await _supportRepository.GetComplains(statusValue);

            var dtos = _mapper.Map<IEnumerable<ComplainDto>>(complains);

            return dtos;
        }

        public async Task ChangeComplainStatus(int id, int status, int userId)
        {
            var complain = await _supportRepository.Get<Complain>(id);

            if (complain != null)
            {
                complain.Status = (ComplainStatus)status;
                complain.ModifiedById = userId;
                complain.ModifiedDate = System.DateTime.Now;

                await _supportRepository.SaveChanges();

                await _notificationService.NotifyComplainUpdated(id);
            }
        }
    }
}
