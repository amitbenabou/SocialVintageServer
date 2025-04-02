using SocialVintageServer.Models;
using System.Drawing;

namespace SocialVintageServer.DTO
{
    public class ShoppingCartItemDto
    {
        public int CartItemId { get; set; }

        public int UserId { get; set; }

        public int ItemId { get; set; }

        public ShoppingCartItemDto() { }
        public ShoppingCartItemDto(Models.ShoppingCartItem cartItem)
        {
            CartItemId = cartItem.ItemId;
            UserId = cartItem.UserId;
            ItemId = cartItem.ItemId;
        }
       
        public Models.ShoppingCartItem GetModel()
        {
            Models.ShoppingCartItem I = new Models.ShoppingCartItem();
            I.CartItemId = CartItemId;
            I.UserId = UserId;
            I.ItemId = this.ItemId;
            return I;
        }
    }
}

    