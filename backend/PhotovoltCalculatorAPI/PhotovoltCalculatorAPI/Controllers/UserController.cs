using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;
using PhotovoltCalculatorAPI.Models.UserModels;


namespace PhotovoltCalculatorAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthenticationService _authenticationService;
        private readonly IMapper _mapper;
        public UserController(IUnitOfWork unitOfWork, IMapper mapper, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Gets User details by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/user/id
        [HttpGet("{id}")]
        public ActionResult<UserDetails> Get(Guid id)
        {
            var user = _unitOfWork.User.GetUser(id);
            if (user == null)
            {
                return BadRequest(new { Message = "User not found" });
            }
            var response = _mapper.Map<UserDetails>(user);
            return Ok(response);
        }
        /// <summary>
        /// Creates a new User by Registering in the application
        /// </summary>
        /// <param name="registerUser"></param>
        /// <returns></returns>
        // POST api/user
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Post([FromBody] RegisterUser registerUser)
        {
            var user = _unitOfWork.User.GetByUserName(registerUser.Email);
            if (user != null)
                return BadRequest(new { Message = $"User with email {registerUser.Email} already exist, please try with a different one." });
            var userEntity = _mapper.Map<SystemUser>(registerUser);
            _unitOfWork.User.CreateUser(userEntity, Request.Headers["origin"]);
            await _unitOfWork.SaveAsync();
            return Ok(new { Message = "Please check your email for verification" });
        }

        /// <summary>
        /// Verifies the email of a registered user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST api/user
        [AllowAnonymous]
        [HttpPost]
        [Route("verify-email")]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailRequest model)
        {
            _unitOfWork.User.VerifyEmail(model.Token);
            await _unitOfWork.SaveAsync();
            return Ok(new { Message = "Verification completed, please login to the system now" });
        }

        /// <summary>
        /// Authenticating user
        /// </summary>
        /// <param name="authenticateUser"></param>
        /// <returns></returns>
        // POST api/user
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<AuthenticateUserResponse>> Post([FromBody] AuthenticateUserRequest authenticateUser)
        {
            var response = await _authenticationService.Authenticate(authenticateUser);
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        /// <summary>
        /// Generates a new jwt access token by fetching the refresh token from cookie
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthenticateUserResponse>> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _authenticationService.RefreshToken(refreshToken);
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        /// <summary>
        /// Updates the User entity based on the data provided
        /// </summary>
        /// <param name="updateUser"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT api/user
        [HttpPut()]
        public async Task<IActionResult> Put(UpdateUser updateUser)
        {
            var user = _unitOfWork.User.GetUser(updateUser.Id);
            if (user == null)
                return BadRequest(new { Message = "User not found" });
            var userEntity = _mapper.Map<SystemUser>(updateUser);
            _unitOfWork.User.UpdateUser(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        /// <summary>
        /// Deletes a used based on the data provided
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/user
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = _unitOfWork.User.GetUser(id);
            if (user == null)
                return BadRequest(new { Message = "User not found" });
            _unitOfWork.User.DeleteUser(user);
            await _unitOfWork.SaveAsync();
            return Ok();
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
