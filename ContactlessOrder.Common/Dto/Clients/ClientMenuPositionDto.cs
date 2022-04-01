using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactlessOrder.Common.Dto.Clients
{
    public class ClientMenuPositionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int? FirstPictureId { get; set; }
        public IEnumerable<ClientMenuOptionDto> Options { get; set; }
    }
}
