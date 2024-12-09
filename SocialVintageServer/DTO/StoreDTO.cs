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

        [StringLength(5)]
        public string LogoExt { get; set; } = null!;

        public int CatagoryId { get; set; }

        public StoreDto(Models.Store modelStore)
        {
            this.StoreId = modelStore.StoreId;
            this.StoreName = modelStore.StoreName;
            this.Adress = modelStore.Adress;
            this.OptionId = modelStore.OptionId;
            this.LogoExt = modelStore.LogoExt;
            this.CatagoryId = modelStore.CatagoryId;
        }

        public Models.Store GetModel()
        {
            Models.Store s = new Models.Store();
            s.StoreId = this.StoreId;
            s.StoreName = this.StoreName;
            s.Adress = this.Adress;
            s.OptionId = this.OptionId;
            s.LogoExt = this.LogoExt;
            s.CatagoryId = this.CatagoryId;
            return s;
        }
    }
}
