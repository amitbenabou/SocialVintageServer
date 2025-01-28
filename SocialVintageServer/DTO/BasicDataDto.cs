using System.ComponentModel.DataAnnotations;

namespace SocialVintageServer.DTO
{
    public class ShippingDto
    {
        public ShippingDto() { }
        public int OptionId { get; set; }

        public string OptionName { get; set; } = null!;

        public ShippingDto(Models.Shipping shipping) 
        {
            OptionId = shipping.OptionId;
            OptionName = shipping.OptionName;
        }

    }

    public class StatusDto
    {
        public StatusDto() { }
        public int StatusId { get; set; }

        public string StatusName { get; set; } = null!;

        public StatusDto(Models.Status Status)
        {
            StatusId = Status.StatusId;
            StatusName = Status.StatusName;
        }

    }

    public class CatagoryDto
    {
        public CatagoryDto() { }
        public int CatagoryId { get; set; }

        public string CatagoryName { get; set; } = null!;

        public CatagoryDto(Models.Catagory Catagory)
        {
            CatagoryId = Catagory.CatagoryId;
            CatagoryName = Catagory.CatagoryName;
        }

    }


    public class BasicDataDto
    {
        public List<CatagoryDto> Catagories { get; set; }
        public BasicDataDto() { }
        public List<StatusDto> Statuss { get; set; }    
        public List<ShippingDto> Shippings { get; set; }
    }
}
