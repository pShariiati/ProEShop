using AutoMapper;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.ProductVariants;

namespace ProEShop.Web.Mappings;

public class ProductVariantMappingProfile : Profile
{
    public ProductVariantMappingProfile()
    {
        this.CreateMap<EditProductVariantViewModel, Entities.ProductVariant>();
    }
}
