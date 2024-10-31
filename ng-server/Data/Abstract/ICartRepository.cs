using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ng_server.Entity;

namespace ng_server.Data.Abstract
{
    public interface ICartRepository: IRepository<Cart>
    {
        void InitializeCart(string userId);
        Cart GetCart(string userId);
        Cart GetByUserId(string userId);
        void AddToCart(string userId, int planeId);
        IEnumerable<Cart> GetAllPlane();
        Cart GetCartById(int cartId);
        void DeleteCartItems (CartItem cartItemId);
        CartItem GetByCartItemId(int id);

    }
}