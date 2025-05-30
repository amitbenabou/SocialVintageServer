﻿using SocialVintageServer.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SocialVintageServer.DTO
{
    public class ItemDto
    {
        public int ItemId { get; set; }

        public string Size { get; set; } = null!;

        public string Brand { get; set; } = null!;

        public string Color { get; set; } = null!;

        public string Price { get; set; } = null!;

        public int StoreId { get; set; }

        public string ItemInfo { get; set; } = null!;
        public bool IsAvailable { get; set; }

        public virtual ICollection<ItemsImageDto>? ItemsImages { get; set; } = new List<ItemsImageDto>();

        public virtual StoreDto? Store { get; set; } = null!;

        public ItemDto() { }
        public ItemDto(Models.Item item, string folderPath)
        {
            ItemId = item.ItemId;
            Size = item.Size;
            IsAvailable = item.IsAvailable;    
            Brand = item.Brand;
            Color = item.Color;
            Price = item.Price;
            StoreId = item.StoreId;
            ItemInfo = item.ItemInfo;
            ItemsImages = new List<ItemsImageDto>();
            if (item.ItemsImages != null)
            {
                foreach (var image in item.ItemsImages)
                {
                    ItemsImageDto imageDto = new ItemsImageDto(image);
                    imageDto.ImagePath = GetItemImagePath(folderPath, imageDto.Id);
                    ItemsImages.Add(imageDto);
                }
            }
            if (item.Store != null)
                Store = new StoreDto(item.Store);
        }
        //this method create a virtual path to each image
        private string GetItemImagePath(string folderPath, int id)
        {
            string virtualPath = $"/itemImages/{id}";
            string path = $"{folderPath}\\itemImages\\{id}.png";
            if (System.IO.File.Exists(path))
            {
                virtualPath += ".png";
            }
            else
            {
                virtualPath += ".jpg";
            }

            return virtualPath;
        }

        public Models.Item GetModel()
        {
            Models. Item I = new Models.Item();
            I.ItemId = this.ItemId;
            I.Size = this.Size;
            I.Brand = this.Brand;
            I.Color = this.Color;
            I.Price = this.Price;
            I.StoreId = this.StoreId;
            I.ItemInfo = this.ItemInfo;
            I.IsAvailable = this.IsAvailable;
          //  I.ItemsImages = this.ItemsImages;
            return I;
        }
    }
}
