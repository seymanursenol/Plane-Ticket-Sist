using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ng_server.Entity;
using ng_server.Models;

namespace Business.Abstract
{
    public interface ICartService
    {
        Task<CartModel> GetUserCart(string userId);
        Task<List<CartModel>> GetAllCartPlane();
        Task<CartModel> GetCartDetails(int cartId,string userId);
        Cart GetCartById(int cartId);     
        void CancelPlane(int cartItemId);  
    }
}