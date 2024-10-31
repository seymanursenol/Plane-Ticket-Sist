using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ng_server.ApplicationContext;
using ng_server.Data.Abstract;
using ng_server.Entity;

namespace ng_server.Data.Concrete
{
    #nullable disable
    public class CartRepository : Repository<Cart, IdentityContext>, ICartRepository
    {
        public void AddToCart(string userId, int planeId)
        {
            using(var context = new IdentityContext())
            {
                if (string.IsNullOrEmpty(userId))
                {
                    throw new ArgumentException("User ID boş", nameof(userId));
                }
                var cart = GetByUserId(userId);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userId,
                        CartItems = new List<CartItem>()
                    };

                    context.Carts.Add(cart);
                    context.SaveChanges(); 
                }
                else
                {
                    Console.WriteLine("Kaydedilen userId: " + userId + " kaydedilen cartId: " + cart.Id);
                }

                var newCartItem = new CartItem()
                {
                    PlaneId = planeId,
                    CartId = cart.Id
                };

                context.CartItems.Add(newCartItem);

                context.SaveChanges();
                Console.WriteLine("CartItem a eklenen cartId: " + cart.Id);
            }
        }

        public override void Update(Cart entity)
        {
            using(var context= new IdentityContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }

        public IEnumerable<Cart> GetAllPlane()
        {
            using(var context= new IdentityContext())
            {
                return context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.IPlane).ToList();
            }
           
        }

        public Cart GetByUserId(string userId)
        {
            using(var context= new IdentityContext())
            {
                return context.Carts
                    .Include(c => c.CartItems) // CartItems'ı dahil et
                    .ThenInclude(c => c.IPlane) // Her bir CartItem için Planes'ı dahil et
                    .FirstOrDefault(c => c.UserId == userId);
            }
        }
        public Cart GetCart(string userId)
        {
            using(var context= new IdentityContext())
            {
                return GetByUserId(userId);
            }
        }

        public Cart GetCartById(int cartId)
        {
            using(var context= new IdentityContext())
            {
                 return context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(i => i.IPlane)
                    .FirstOrDefault(c => c.Id == cartId);
            }
            
        }

        public void InitializeCart(string userId)
        {
            using(var context= new IdentityContext())
            {
                context.Carts.Add(new Cart { UserId = userId });
                context.SaveChanges();
            }
        }

        public void DeleteCartItems(CartItem cartItemId)
        {
            using(var context= new IdentityContext())
            {
                context.Set<CartItem>().Remove(cartItemId);
                context.SaveChanges();
            }
        }
    
        public CartItem GetByCartItemId(int id)
        {
            using(var context= new IdentityContext())
            {
                return context.Set<CartItem>().Find(id);
            }
        }
    }
}