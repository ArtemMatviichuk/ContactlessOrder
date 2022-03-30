using System.Collections.Generic;

namespace ContactlessOrder.Common.Dto.Companies
{
    public class UpdateMenuItemDto : CreateMenuItemDto
    {
        public IEnumerable<int> DeletedPictureIds { get; set; }
    }
}
