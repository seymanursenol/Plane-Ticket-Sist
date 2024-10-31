using System.Numerics;
using System.Security.Claims;
using Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ng_server.ApplicationContext;
using ng_server.Data.Abstract;
using ng_server.Entity;
using ng_server.Models;

namespace ng_server.Controller
{
    [Route("/api/[controller]")]
    [ApiController]
    public class PlanesController : ControllerBase
    {
        private readonly IdentityContext _context;
        private IPlanesRepository _planesRepository;
        private IPlaneService _planeService;
        private UserManager<Users> _userManager;
        private ICartRepository _cartRepository;
        public PlanesController(IdentityContext context, IPlanesRepository planesRepository,UserManager<Users> userManager,ICartRepository cartRepository,IPlaneService planeService)
        {
            _context = context;
            _planesRepository = planesRepository;
            _userManager= userManager;
            _cartRepository= cartRepository;
            _planeService= planeService;
        }


        [HttpPost("Add_Plane")]
        public async Task<IActionResult> Create([FromBody] PlaneModel model)
        {
           _planeService.Create(model);
           return Ok();
        }


        [HttpPut("cancel/{id}")]
        public IActionResult DeletePlane(int id)
        {
            _planeService.Delete(id);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, PlaneModel model)
        {
            _planeService.Update(id,model);
            return Ok();
        }

        [HttpGet]
        public ActionResult<List<Planes>> GetAll()
        {
            var planes = _planeService.GetAll();
            return Ok(planes);
        }

        [HttpGet("{id}")]
        public ActionResult GetPlaneId(int id)
        {
            var plane = _planeService.GetById(id);
            return Ok(plane);
        }


        [HttpGet("filter")]
        public ActionResult<List<Planes>> GetByOutgoingIncoming([FromQuery] string outgoing,[FromQuery] string incoming)
        {
            var planes = _planeService.GetByFilter(outgoing, incoming);
            return Ok(planes);
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

        [HttpPost("AddRezervation")]
        public IActionResult AddToCart([FromBody] AddRezDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            try
            {
                _cartRepository.AddToCart(userId, dto.PlaneID);
                _context.SaveChanges();
                return Ok(new { message = "Rezervasyon başarıyla eklendi",
                 userId = userId, planeId = dto.PlaneID });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }    
    }

}


