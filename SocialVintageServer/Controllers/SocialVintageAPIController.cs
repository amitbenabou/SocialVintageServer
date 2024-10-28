using Microsoft.AspNetCore.Mvc;
using SocialVintageServer.Models;



[Route("api")]
[ApiController]
public class SocialVintageAPIController : ControllerBase
{
    //a variable to hold a reference to the db context!
    private SocialVintageDbContext context;
    //a variable that hold a reference to web hosting interface (that provide information like the folder on which the server runs etc...)
    private IWebHostEnvironment webHostEnvironment;
    //Use dependency injection to get the db context and web host into the constructor
    public SocialVintageAPIController(SocialVintageDbContext context, IWebHostEnvironment env)
    {
        this.context = context;
        this.webHostEnvironment = env;
    }

    [HttpGet]
    [Route("TestServer")]
    public ActionResult<string> TestServer()
    {
        return Ok("Server Responded Successfully");
    }


    [HttpPost("login")]
    public IActionResult Login([FromBody] SocialVintageServer.DTO.LoginDto loginDto)
    {
        try
        {
            HttpContext.Session.Clear(); 

            //Get model user class from DB with matching email. 
            SocialVintageServer.Models.User? modelsUser = context.GetUser(loginDto.UserMail);

            //Check if user exist for this email and if password match, if not return Access Denied (Error 403) 
            if (modelsUser == null || modelsUser.Pswrd != loginDto.Pswrd)
            {
                return Unauthorized();
            }

            HttpContext.Session.SetString("loggedInUser", modelsUser.UserMail);

            SocialVintageServer.DTO.UserDto dtoUser = new SocialVintageServer.DTO.UserDto(modelsUser);
            //dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.UserId);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }
}

