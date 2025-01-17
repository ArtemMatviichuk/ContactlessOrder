﻿using System;
using System.Collections.Generic;

namespace ContactlessOrder.Common.Dto.Orders
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public int TotalPrice { get; set; }
        public string Comment { get; set; }
        public string UserPhoneNumber { get; set; }

        public int StatusId { get; set; }
        public string StatusName { get; set; }
        public int StatusValue { get; set; }
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public int PaymentMethodValue { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public IEnumerable<OrderPositionWithModificationsDto> Positions { get; set; }
    }
}
