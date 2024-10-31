using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ng_server.ApplicationContext;
using ng_server.Data.Abstract;
using ng_server.Entity;
using ng_server.Models;

namespace ng_server.Controller
{
    #nullable disable
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        private readonly IRezervationRepository _rezervationRepository;
        private readonly ICartService _cartService;
        public CartController(ICartRepository cartRepository, UserManager<Users> userManager,IRezervationRepository rezervationRepository,ICartService cartService)
        {
            _cartRepository = cartRepository;
            _rezervationRepository= rezervationRepository;
            _cartService = cartService;
        }

        [HttpGet("GetTicket")]
        public async Task<IActionResult> GetTicket(string userId)
        {
            var cartModel = await _cartService.GetUserCart(userId);
            return Ok(cartModel);
        }   

        [HttpGet("GetUserId")]
        public IActionResult GetUserId()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            return Ok(new { UserId = userId });
        }

        [HttpGet]
        public async Task<ActionResult<List<CartModel>>> GetAll()
        {
            var cartModel = await _cartService.GetAllCartPlane();
            return Ok(cartModel);
            
        }

        [HttpGet("GetDetails/{cartId}")]
        public async Task<ActionResult<CartModel>> GetCartDetails(int cartId,string userId)
        {
           var getDetails = await _cartService.GetCartDetails(cartId,userId);
           return Ok(getDetails);
        }
    

        [HttpDelete("cartItemDelete/{cartItemId}")]
        public  async Task<IActionResult> CartDelete(int cartItemId){

            _cartService.CancelPlane(cartItemId);
            return Ok();
        }

        //ÖDEME İŞLEMLERİ İÇİN
        [HttpPost("Checkouts")]
        public IActionResult CheckOut(){
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var cart = _cartRepository.GetByUserId(userId);
            
            var rezervationModel = new RezervationModel();
            rezervationModel.CartModel= new CartModel()
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
            return Ok(rezervationModel);
        }


        [HttpPost("CheckOut")]
        public IActionResult CheckOut(RezervationModel model){
            if(ModelState.IsValid){
                var userId =User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var cart = _cartRepository.GetByUserId(userId);
                Console.WriteLine("userId: " +userId);
                model.CartModel= new CartModel()
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
                var payment = PaymentProcess(model);


                
                    SaveOrder(model, payment,userId);
                    ClearCart(userId);
                    Console.WriteLine("Payment başarılı");
                    return Ok();
                
            }
            return BadRequest(ModelState);
        }

        private void ClearCart(string userId)
        {
            throw new NotImplementedException();
        }

        private void SaveOrder(RezervationModel model, Payment payment, string userId)
        {
            var rezervation = new Rezervation();
            rezervation.RezNumber = new Random().Next(111111,999999).ToString();
            rezervation.PaymentId=  payment.PaymentId;
            rezervation.ConversationId= payment.ConversationId;
            rezervation.UserName = model.UserName;
            rezervation.Phone = model.Phone;
            rezervation.Email = model.Email;
            rezervation.UserId= userId;

            rezervation.RezervationItems = new List<RezervationItem>();

            foreach(var item in model.CartModel.CartItems){
                var rezervationItem = new RezervationItem(){
                    Price = item.Price,
                    PlaneId = item.PlaneId
                };  
                rezervation.RezervationItems.Add(rezervationItem);
            }
            _rezervationRepository.Create(rezervation);
            
        }

        private Payment PaymentProcess(RezervationModel model)
        {
            Options options = new Options();
            options.ApiKey = "sandbox-ABMA5aANaEpwPNLzAOpJMGaaKudZWuXx";
            options.SecretKey = "sandbox-P1YAoUYYX9FnonoE6cpb5RBDNo7J9Q6C";
            options.BaseUrl = "https://sandbox-api.iyzipay.com";
                    
            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = new Random().Next(111111111,999999999).ToString();
            request.Price = model.CartModel.TotalPrice().ToString();
            request.PaidPrice =  model.CartModel.TotalPrice().ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = "B67832";
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = model.CardName;
            paymentCard.CardNumber = model.CardNumber;
            paymentCard.ExpireMonth = model.ExpirationMonth;
            paymentCard.ExpireYear = model.ExpirationYear;
            paymentCard.Cvc = model.Cvc;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            // paymentCard.CardNumber = "5528790000000008";
            // paymentCard.ExpireMonth = "12";
            // paymentCard.ExpireYear = "2030";
            // paymentCard.Cvc = "123";

            Buyer buyer = new Buyer();
            buyer.Id = "BY789";
            buyer.Name = model.UserName;
            buyer.GsmNumber = model.Phone;
            buyer.Email = model.Email;
            buyer.IdentityNumber = "74300864791";
            buyer.LastLoginDate = "2015-10-05 12:43:35";
            buyer.RegistrationDate = "2013-04-21 15:12:09";
            buyer.RegistrationAddress = "Nidakule Göztepe, Merdivenköy Mah. Bora Sok. No:1";
            buyer.Ip = "85.34.78.112";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "34732";
            request.Buyer = buyer;

            List<BasketItem> basketItems = new List<BasketItem>();

            foreach (var item in model.CartModel.CartItems)
            {
                if (item == null)
                    throw new ArgumentNullException(nameof(item));

                var basketItem = new BasketItem
                {
                    Id = item.PlaneId.ToString(),
                    Price = item.Price.ToString("F2", CultureInfo.InvariantCulture),
                    ItemType = BasketItemType.PHYSICAL.ToString()
                };

                basketItems.Add(basketItem);
            }
            request.BasketItems = basketItems;

            return Payment.Create(request, options);
        }
    } 
}
