using System.ComponentModel.DataAnnotations;

namespace SocialVintageServer.DTO
{
    public class StoreDto
    {
        public StoreDto()
        {

        }
        public int StoreId { get; set; }

        [StringLength(50)]
        public string StoreName { get; set; } = null!;

        [StringLength(50)]
        public string Adress { get; set; } = null!;

        public int OptionId { get; set; }

        public string ProfileImagePath { get; set; } = "";

        public int CatagoryId { get; set; }

        public StoreDto(Models.Store modelStore)
        {
            this.StoreId = modelStore.StoreId;
            this.StoreName = modelStore.StoreName;
            this.Adress = modelStore.Adress;
            this.OptionId = modelStore.OptionId;
            this.CatagoryId = modelStore.CatagoryId;
        }

        public Models.Store GetModel()
        {
            Models.Store s = new Models.Store();
            s.StoreId = this.StoreId;
            s.StoreName = this.StoreName;
            s.Adress = this.Adress;
            s.OptionId = this.OptionId;
            s.CatagoryId = this.CatagoryId;
            return s;
        }
    }
}
