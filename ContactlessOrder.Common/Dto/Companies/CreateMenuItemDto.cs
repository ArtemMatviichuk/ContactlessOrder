using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ContactlessOrder.Common.Dto.Companies
{
    public class CreateMenuItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public IEnumerable<IFormFile> Pictures { get; set; }
        public ICollection<MenuItemOptionDto> Options { get; set; }
        public ICollection<MenuModificationDto> Modifications { get; set; }
    }
}
