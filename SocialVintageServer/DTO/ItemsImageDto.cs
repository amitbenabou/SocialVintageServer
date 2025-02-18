using SocialVintageServer.Models;

namespace SocialVintageServer.DTO
{
    public class ItemsImageDto
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public string ImagePath
        {
            get; set;
        }

        public ItemsImageDto() { }

        public ItemsImageDto(Models.ItemsImage item) 
        {
            Id = item.Id;
            ItemId = item.ItemId;
        }

        public Models.ItemsImage GetModel()
        {
            return new Models.ItemsImage()
            {
                Id = this.Id,
                ItemId = this.ItemId
            };
        }


    }
}
