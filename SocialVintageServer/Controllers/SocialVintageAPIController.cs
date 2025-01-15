using Microsoft.AspNetCore.Mvc;
using SocialVintageServer.DTO;
using SocialVintageServer.Models;
using Swashbuckle.AspNetCore.Swagger;



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

            HttpContext.Session.SetString("LoggedInUser", modelsUser.UserMail);

            SocialVintageServer.DTO.UserDto dtoUser = new SocialVintageServer.DTO.UserDto(modelsUser);
            dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.UserId);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] SocialVintageServer.DTO.UserDto userDto)
    {
        try
        {
            HttpContext.Session.Clear(); //Logout any previous login attempt

            //Create model user class
            SocialVintageServer.Models.User modelsUser = userDto.GetModel();

            context.Users.Add(modelsUser);
            context.SaveChanges();

            //User was added!
            SocialVintageServer.DTO.UserDto dtoUser = new SocialVintageServer.DTO.UserDto(modelsUser);
            dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.UserId);
            return Ok(dtoUser);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("AddStore")]
    public IActionResult AddStore([FromBody] SocialVintageServer.DTO.StoreDto storeDto)
    {
        try
        {
            //Check if who is logged in
            string? userEmail = HttpContext.Session.GetString("LoggedInUser");
            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("User is not logged in");
            }
            //Create model user class
            SocialVintageServer.Models.Store modelsStore = storeDto.GetModel();

            context.Stores.Add(modelsStore);
            context.SaveChanges();

            User? u = context.GetUser(userEmail);
            if (u != null)
            {
                u.HasStore = true;
                context.Update(u);
                context.SaveChanges();
            }
            
            //User was added!
            SocialVintageServer.DTO.StoreDto dtoStore = new SocialVintageServer.DTO.StoreDto(modelsStore);
            dtoStore.ProfileImagePath = GetProfileImageVirtualPath(dtoStore.StoreId, true);
            return Ok(dtoStore);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }

    }

    [HttpPost("updateprofile")]
    public async Task<IActionResult> UpdateProfile([FromBody] UserDto userDto)
    {
        if (userDto == null)
        {
            return BadRequest("User data is null");
        }

        // חיפוש המשתמש לפי Id
        var user = await context.Users.FindAsync(userDto.UserId);

        if (user == null)
        {
            return NotFound($"User with ID {userDto.UserId} not found");
        }

        // עדכון השדות של המשתמש
        user.UserName = userDto.UserName;
        user.UserMail = userDto.UserMail;
        user.UserAdress = userDto.UserAdress;
        user.Pswrd = userDto.Pswrd;


        try
        {
            // שמירת השינויים למסד הנתונים
            await context.SaveChangesAsync();
            return Ok(new { message = "Profile updated successfully" });
        }
        catch (Exception ex)
        {
            // טיפול בשגיאות
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred", error = ex.Message });
        }
    }



    private string GetProfileImageVirtualPath(int userId, bool IsStore = false)
    {
        string virtualPath = $"/profileImages/{userId}";
        string path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.png";
        if (IsStore)
            path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\S{userId}.png";
        if (System.IO.File.Exists(path))
        {
            virtualPath += ".png";
        }
        else
        {
            path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{userId}.jpg";
            if (IsStore)
                path = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\S{userId}.jpg";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".jpg";
            }
            else
            {
                virtualPath = $"/profileImages/default.png";
                if (IsStore)
                    virtualPath = $"/profileImages/storedefault.png";
            }
        }

        return virtualPath;
    }


    [HttpPost("UploadProfileImage")]
    public async Task<IActionResult> UploadProfileImageAsync(IFormFile file, [FromQuery] bool IsStore)
    {
        //Check if who is logged in
        string? userEmail = HttpContext.Session.GetString("LoggedInUser");
        if (string.IsNullOrEmpty(userEmail))
        {
            return Unauthorized("User is not logged in");
        }

        //Get model user class from DB with matching email. 
        SocialVintageServer.Models.User? user = context.GetUser(userEmail);
        SocialVintageServer.Models.Store? store = context.Stores.Where(s => s.StoreId == user.UserId).FirstOrDefault();
        //Clear the tracking of all objects to avoid double tracking
        context.ChangeTracker.Clear();

        if (user == null)
        {
            return Unauthorized("User is not found in the database");
        }


        //Read all files sent
        long imagesSize = 0;

        if (file.Length > 0)
        {
            //Check the file extention!
            string[] allowedExtentions = { ".png", ".jpg" };
            string extention = "";
            if (file.FileName.LastIndexOf(".") > 0)
            {
                extention = file.FileName.Substring(file.FileName.LastIndexOf(".")).ToLower();
            }
            if (!allowedExtentions.Where(e => e == extention).Any())
            {
                //Extention is not supported
                return BadRequest("File sent with non supported extention");
            }

            //Build path in the web root (better to a specific folder under the web root
            string filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\{user.UserId}{extention}";
            if (IsStore)
            {
                filePath = $"{this.webHostEnvironment.WebRootPath}\\profileImages\\S{user.UserId}{extention}";
            }

            using (var stream = System.IO.File.Create(filePath))
            {
                await file.CopyToAsync(stream);

                if (IsImage(stream))
                {
                    imagesSize += stream.Length;
                }
                else
                {
                    //Delete the file if it is not supported!
                    System.IO.File.Delete(filePath);
                }

            }

        }
        if (IsStore)
        {
            SocialVintageServer.DTO.StoreDto dtoStore = new SocialVintageServer.DTO.StoreDto(store);
            dtoStore.ProfileImagePath = GetProfileImageVirtualPath(dtoStore.StoreId, true);
            return Ok(dtoStore);
        }
        else
        {
            SocialVintageServer.DTO.UserDto dtoUser = new SocialVintageServer.DTO.UserDto(user);
            dtoUser.ProfileImagePath = GetProfileImageVirtualPath(dtoUser.UserId);
            return Ok(dtoUser);
        }
        
    }

    //Helper functions

    //this function gets a file stream and check if it is an image
    private static bool IsImage(Stream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);

        List<string> jpg = new List<string> { "FF", "D8" };
        List<string> bmp = new List<string> { "42", "4D" };
        List<string> gif = new List<string> { "47", "49", "46" };
        List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
        List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

        List<string> bytesIterated = new List<string>();

        for (int i = 0; i < 8; i++)
        {
            string bit = stream.ReadByte().ToString("X2");
            bytesIterated.Add(bit);

            bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
            if (isImage)
            {
                return true;
            }
        }

        return false;

    }
}

