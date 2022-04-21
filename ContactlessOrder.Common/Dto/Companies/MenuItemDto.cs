using System.Collections.Generic;

namespace ContactlessOrder.Common.Dto.Companies
{
    public class MenuItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CompanyId { get; set; }

        public int? FirstPictureId { get; set; }
        public ICollection<IdNamePriceDto> Options { get; set; }
        public IEnumerable<int> Modifications { get; set; }
    }
}
