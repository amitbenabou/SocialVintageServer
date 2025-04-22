using SocialVintageServer.Models;
using System.Drawing;

namespace SocialVintageServer.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateOnly OrderDate { get; set; }

        public OrderDto() { }
        public OrderDto(Models.Order order)
        {
            OrderId = order.OrderId;
            UserId = order.UserId;
            OrderDate = order.OrderDate;
        }
        

        public Models.Order GetModel()
        {
            Models.Order o = new Models.Order();
            o.OrderId = this.OrderId;
            o.UserId = this.UserId;
            o.OrderDate = this.OrderDate;
            return o;
        }
    }
}
