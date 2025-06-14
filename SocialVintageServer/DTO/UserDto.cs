﻿

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
        public string PhoneNumber { get; set; } = null!;

        public List<ItemDto> WishListItems { get; set; }

        public UserDto(Models.User modelUser, string folderPath)
        {
            this.UserId = modelUser.UserId;
            this.UserName = modelUser.UserName;
            this.UserMail = modelUser.UserMail;
            this.UserAdress = modelUser.UserAdress;
            this.HasStore = modelUser.HasStore;
            this.Pswrd = modelUser.Pswrd;
            this.PhoneNumber = modelUser.PhoneNumber;
            WishListItems = new List<ItemDto>();
            if (modelUser.WishListItems != null)
            {
                foreach (var item in modelUser.WishListItems) 
                {
                    ItemDto itemDto = new ItemDto(item.Item, folderPath);
                    WishListItems.Add(itemDto);
                }
            }
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
            u.PhoneNumber = this.PhoneNumber;

            return u;
        }
    }
}
