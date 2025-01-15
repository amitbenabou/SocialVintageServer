

namespace SocialVintageServer.DTO
{
    public class UserDto
    {
        public UserDto()
        {

        }

        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string UserMail { get; set; } = null!;

        public string? UserAdress { get; set; } = null;

        public bool HasStore { get; set; }

        public string Pswrd { get; set; } = null!;
        public string ProfileImagePath { get; set; } = "";



        public UserDto(Models.User modelUser)
        {
            this.UserId = modelUser.UserId;
            this.UserName = modelUser.UserName;
            this.UserMail = modelUser.UserMail;
            this.UserAdress = modelUser.UserAdress;
            this.HasStore = modelUser.HasStore;
            this.Pswrd = modelUser.Pswrd;
        }

        public Models.User GetModel()
        {
            Models.User u = new Models.User();
            u.UserId = this.UserId;
            u.UserName = this.UserName;
            u.UserMail = this.UserMail;
            u.UserAdress = this.UserAdress;
            u.HasStore = this.HasStore;
            u.Pswrd = this.Pswrd;
            return u;
        }
    }
}
