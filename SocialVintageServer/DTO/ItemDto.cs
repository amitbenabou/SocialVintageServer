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

        public virtual ICollection<string> ItemsImages { get; set; } = new List<string>();

        public virtual StoreDto Store { get; set; } = null!;

        public ItemDto() { }
        public ItemDto(Models.Item item, string folderPath)
        {
            ItemId = item.ItemId;
            Size = item.Size;
                
            Brand = item.Brand;
            Color = item.Color;
            Price = item.Price;
            StoreId = item.StoreId;
            ItemInfo = item.ItemInfo;
            ItemsImages = new List<string>();
            foreach(var image in item.ItemsImages)
            {
                ItemsImages.Add(GetItemImagePath(folderPath, item.ItemId));
            }
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
    }
}
