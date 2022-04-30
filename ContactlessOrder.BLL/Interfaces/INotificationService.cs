﻿using System.Threading.Tasks;

namespace ContactlessOrder.BLL.Interfaces
{
    public interface INotificationService
    {
        Task NotifyOrderPaid(int id, int totalPrice);
        Task NotifyOrderUpdated(int id, int totalPrice);
        Task NotifyOrderReady(int id); 
        Task NotifyOrderRejected(int id, int totalPrice);
        Task NotifyOrderCompleted(int id, int totalPrice);
    }
}