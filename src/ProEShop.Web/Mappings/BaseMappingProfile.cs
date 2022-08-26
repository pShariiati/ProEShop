using AutoMapper;

namespace ProEShop.Web.Mappings;

public class BaseMappingProfile : Profile
{
    public long UserId { get; set; }

    public long ConsignmentId { get; set; }

    public DateTime Now { get; set; }
}
