using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;
using PhotovoltCalculatorAPI.Models.ProductModels;
using PhotovoltCalculatorAPI.Models.ProjectModels;
using System.Security.Claims;

namespace PhotovoltCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProjectController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: api/project
        [HttpGet]
        public IEnumerable<ProjectIndex> Get()
        {
            var userId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var projects = _unitOfWork.Project.GetAllProjects(userId);
            var response = _mapper.Map<IEnumerable<ProjectIndex>>(projects);
            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/project/id
        [HttpGet("{id}")]
        public ActionResult<ProjectDetails> Get(Guid id)
        {
            var project = _unitOfWork.Project.GetProject(id);
            if (project == null)
                return BadRequest(new { Message = "Project not found" });
            var products = _unitOfWork.ProjectProduct.GetProductsForProject(projectId: project.Id);
            var productsResponse = _mapper.Map<IEnumerable<ProductIndex>>(products);
            var response = _mapper.Map<ProjectDetails>(project);
            response.Products = productsResponse.ToList();
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        // POST api/project
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateProject project)
        {
            if (project == null)
                return BadRequest(new { Message = "Project can not be empty" });
            var userId = Guid.Parse(HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var projectEntity = _mapper.Map<Project>(project);
            projectEntity.SystemUserId = userId;
            _unitOfWork.Project.CreateProject(projectEntity);
            await _unitOfWork.SaveAsync();
            return StatusCode(201);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        // PUT api/project
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateProject project)
        {
            if (project == null)
                return BadRequest(new { Message = "Project can not be empty" });
            var proj = _unitOfWork.Project.GetProject(project.Id);
            if (proj == null)
                return BadRequest(new { Message = "Project not found" });
            var projectEntity = _mapper.Map<Project>(project);
            _unitOfWork.Project.UpdateProject(projectEntity);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/project/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var project = _unitOfWork.Project.GetProject(id);
            if (project == null)
                return BadRequest(new { Message = "Project not found" });
            _unitOfWork.Project.DeleteProject(project);
            await _unitOfWork.SaveAsync();
            return Ok();
        }
    }
}
