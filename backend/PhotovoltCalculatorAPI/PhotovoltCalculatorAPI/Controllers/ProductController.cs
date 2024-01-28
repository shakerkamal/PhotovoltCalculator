using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;
using PhotovoltCalculatorAPI.Models.ProductModels;

namespace PhotovoltCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/product
        [HttpGet]
        public IEnumerable<ProductIndex> Get()
        {
            var products = _unitOfWork.Product.GetAllProducts();
            var response = _mapper.Map<IEnumerable<ProductIndex>>(products);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/product/5
        [HttpGet("{id}")]
        public ActionResult<ProductDetails> Get(Guid id)
        {
            var product = _unitOfWork.Product.GetProduct(id);
            if (product == null)
                return BadRequest(new { Message = "Product not found" });
            var response = _mapper.Map<ProductDetails>(product);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        // POST api/product
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProduct product)
        {
            //var product = _unitOfWork.Product.ProductExists(product.Name);
            if (product == null)
                return BadRequest(new { Message = "Product can not be empty" });
            var productEntity = _mapper.Map<Product>(product);
            _unitOfWork.Product.CreateProduct(productEntity);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        // PUT api/product
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProduct product)
        {
            if (product == null)
                return BadRequest(new { Message = "Product can not be empty" });
            var prod = _unitOfWork.Product.GetProduct(product.Id);
            if (prod == null)
                return BadRequest(new { Message = "Product not found" });
            var productEntity = _mapper.Map<Product>(product);
            _unitOfWork.Product.UpdateProduct(productEntity);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/product/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var product = _unitOfWork.Product.GetProduct(id);
            if (product == null)
                return BadRequest(new { Message = "Product not found" });
            _unitOfWork.Product.DeleteProduct(product);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
