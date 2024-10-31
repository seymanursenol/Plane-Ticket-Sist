using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Abstract;
using Microsoft.AspNetCore.Identity;
using ng_server.ApplicationContext;
using ng_server.Data.Abstract;
using ng_server.Entity;
using ng_server.Models;

namespace Business.Concrete
{
    #nullable disable
    public class CartManager : ICartService
    {
        private ICartRepository _cartRepository;
         private UserManager<Users> _userManager;
        public CartManager(ICartRepository cartRepository, UserManager<Users> userManager)
        {
            _cartRepository = cartRepository;
            _userManager = userManager;
        }

        public async Task<List<CartModel>> GetAllCartPlane()
        {
            var carts = _cartRepository.GetAllPlane();
            if (carts == null)
            {
                return null;
            }

            var cartModels = new List<CartModel>();
            
            foreach (var cart in carts)
            {
                if (cart.CartItems == null || !cart.CartItems.Any())
                {
                    continue; 
                }
                var user = await _userManager.FindByIdAsync(cart.UserId);
                var cartModel = new CartModel()
                {
                    CartId = cart.Id,
                    UserId = cart.UserId,
                    UserName = user?.UserName, 
                    CartItems = cart.CartItems.Select(i => new CartItemModel()
                    {
                        CartItemId = i.Id,
                        OutGoing = i.IPlane.Outgoing,
                        InComing = i.IPlane.Incoming,
                        Price = i.IPlane.Price,
                        TicketTotal = i.IPlane.TicketTotal,
                        Time = i.IPlane.Time.ToString()
                    }).ToList()
                };

                if (user != null)
                {
                    cartModel.UserItemsModels = new List<UserItemsModel>
                    {
                        new UserItemsModel
                        {
                            UserName = user.UserName 
                        }
                    };
                }
                cartModels.Add(cartModel);
            }
            if (!cartModels.Any())
            {
                return null;
            }
            return cartModels;
        }

        public async Task<CartModel> GetUserCart(string userId){

            var cart = _cartRepository.GetByUserId(userId);
            Console.WriteLine("cartId: " + cart.UserId);
            Console.WriteLine(cart.Id);
            
            if (cart == null)
            {
                return null;
            }

            var cartModel = new CartModel()
            {
                CartId = cart.Id,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    OutGoing = i.IPlane.Outgoing,
                    InComing = i.IPlane.Incoming,
                    Price = i.IPlane.Price,
                    TicketTotal = i.IPlane.TicketTotal,
                    Time = i.IPlane.Time.ToString()
                }).ToList()
            };

            return cartModel;
        }
    
        public async Task<CartModel> GetCartDetails(int cartId,string userId)
        {
            var cart = _cartRepository.GetCartById(cartId);
            Console.WriteLine(cart.CartItems);
            
            if (cart == null)
            {
                return null;
            }
            
            var user = await _userManager.FindByIdAsync(cart.UserId);
            
            var cartModel = new CartModel()
            {
                CartId = cart.Id,
                UserId = cart.UserId,
                UserName = user.UserName,
                CartItems = cart.CartItems.Select(i => new CartItemModel()
                {
                    CartItemId = i.Id,
                    OutGoing = i.IPlane.Outgoing,
                    InComing = i.IPlane.Incoming,
                    Price = i.IPlane.Price,
                    TicketTotal = i.IPlane.TicketTotal,
                    Time = i.IPlane.Time.ToString()
                }).ToList()
                
            };

            if (user != null)
            {
                cartModel.UserItemsModels = new List<UserItemsModel>
                {
                    new UserItemsModel
                    {
                        UserName = user.UserName 
                    }
                };
            }
             return cartModel;
        }

        public Cart GetCartById(int cartId)
        {
            return _cartRepository.GetCartById(cartId);
        }

        public void CancelPlane(int cartItemId)
        {
            var id = _cartRepository.GetByCartItemId(cartItemId);
            if(id!=null){
              _cartRepository.DeleteCartItems(id);
            }
        }
    }
}