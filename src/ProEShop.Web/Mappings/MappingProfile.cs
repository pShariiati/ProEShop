﻿using AutoMapper;
using ProEShop.Common.Helpers;
using ProEShop.Entities.Identity;
using ProEShop.ViewModels.Brands;
using ProEShop.ViewModels.Categories;
using ProEShop.ViewModels.CategoryFeatures;
using ProEShop.ViewModels.Consignments;
using ProEShop.ViewModels.FeatureConstantValues;
using ProEShop.ViewModels.Guarantees;
using ProEShop.ViewModels.Products;
using ProEShop.ViewModels.ProductShortLinks;
using ProEShop.ViewModels.ProductStocks;
using ProEShop.ViewModels.ProductVariants;
using ProEShop.ViewModels.Sellers;
using ProEShop.ViewModels.Variants;

namespace ProEShop.Web.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Parameters

        long userId = 0;
        long sellerId = 0;
        long consignmentId = 0;
        DateTime now = default;

        #endregion

        //this.CreateMap<string, string>()
        //    .ConvertUsing(str => str != null ? str.Trim() : null);
        this.CreateMap<User, CreateSellerViewModel>();
        this.CreateMap<CreateSellerViewModel, Entities.Seller>()
            .AddTransform<string>(str => str != null ? str.Trim() : null);
        this.CreateMap<CreateSellerViewModel, Entities.Identity.User>()
            .AddTransform<string>(str => str != null ? str.Trim() : null)
            .ForMember(x => x.BirthDate,
                opt => opt.Ignore());
        this.CreateMap<Entities.Seller, ShowSellerViewModel>()
            .ForMember(dest => dest.ProvinceAndCity,
                options =>
                    options.MapFrom(src => $"{src.Province.Title} - {src.City.Title}"))
            .ForMember(dest => dest.CreatedDateTime,
                options =>
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDate()));
        this.CreateMap<Entities.Seller, SellerDetailsViewModel>()
            .ForMember(dest => dest.CreatedDateTime,
                options =>
                    options.MapFrom(src => src.CreatedDateTime.ToLongPersianDate()));

        this.CreateMap<Entities.Brand, ShowBrandViewModel>();
        this.CreateMap<AddBrandViewModel, Entities.Brand>()
            .AddTransform<string>(str => str != null ? str.Trim() : null);

        this.CreateMap<Entities.Brand, EditBrandViewMode>().ReverseMap()
            .AddTransform<string>(str => str != null ? str.Trim() : null);

        this.CreateMap<AddCategoryViewModel, Entities.Category>()
            .AddTransform<string>(str => str != null ? str.Trim() : null);

        this.CreateMap<Entities.Category, EditCategoryViewModel>()
            .ForMember(x => x.Picture,
                opt => opt.Ignore())
            .ForMember(dest => dest.SelectedPicture,
                options =>
                    options.MapFrom(src => src.Picture))
            .ForMember(dest => dest.CanVariantTypeChange,
                options =>
                    options.MapFrom(src => src.CategoryVariants.Any() ? false : (!src.HasVariant)));

        this.CreateMap<EditCategoryViewModel, Entities.Category>()
            .AddTransform<string>(str => str != null ? str.Trim() : null);

        this.CreateMap<AddBrandBySellerViewModel, Entities.Brand>()
            .AddTransform<string>(str => str != null ? str.Trim() : null);

        this.CreateMap<Entities.Brand, BrandDetailsViewModel>();

        this.CreateMap<Entities.CategoryFeature, CategoryFeatureForCreateProductViewModel>();

        this.CreateMap<Entities.FeatureConstantValue, ShowFeatureConstantValueViewModel>();

        this.CreateMap<AddFeatureConstantValueViewModel, Entities.FeatureConstantValue>()
            .AddTransform<string>(str => str != null ? str.Trim() : null);

        this.CreateMap<Entities.FeatureConstantValue, ShowCategoryFeatureConstantValueViewModel>();
        this.CreateMap<Entities.FeatureConstantValue, EditFeatureConstantValueViewModel>();

        this.CreateMap<EditFeatureConstantValueViewModel, Entities.FeatureConstantValue>()
            .AddTransform<string>(str => str != null ? str.Trim() : null);

        this.CreateMap<AddProductViewModel, Entities.Product>()
            .AddTransform<string>(str => str != null ? str.Trim() : null);

        this.CreateMap<Entities.FeatureConstantValue, FeatureConstantValueForCreateProductViewModel>();

        this.CreateMap<Entities.Product, ShowProductViewModel>()
            .ForMember(dest => dest.MainPicture,
                options =>
                    options.MapFrom(src => src.ProductMedia.First().FileName));

        this.CreateMap<Entities.Product, ShowProductInSellerPanelViewModel>()
            .ForMember(dest => dest.MainPicture,
                options =>
                    options.MapFrom(src => src.ProductMedia.First().FileName));

        this.CreateMap<Entities.Product, ShowAllProductInSellerPanelViewModel>()
            .ForMember(dest => dest.MainPicture,
                options =>
                    options.MapFrom(src => src.ProductMedia.First().FileName));

        this.CreateMap<Entities.Product, ProductDetailsViewModel>();
        this.CreateMap<Entities.ProductFeature, ProductFeatureForProductDetailsViewModel>();

        this.CreateMap<Entities.Variant, ShowVariantViewModel>();

        this.CreateMap<Entities.Guarantee, ShowGuaranteeViewModel>();

        this.CreateMap<Entities.Product, AddVariantViewModel>()
            .ForMember(dest => dest.ProductId,
                options =>
                    options.MapFrom(src => src.Id))
            .ForMember(dest => dest.ProductTitle,
                options =>
                    options.MapFrom(src => src.PersianTitle))
            .ForMember(dest => dest.CommissionPercentage,
                options =>
                    options.MapFrom(
                        src => src.Category.CategoryBrands
                            .Select(x => new
                            {
                                x.BrandId,
                                x.CommissionPercentage
                            })
                            .Single(x => x.BrandId == src.BrandId)
                            .CommissionPercentage
                        ))
            .ForMember(dest => dest.MainPicture,
                options =>
                    options.MapFrom(src => src.ProductMedia.First().FileName))
            .ForMember(dest => dest.Variants,
                options =>
                    options.MapFrom(
                        src => src.Category.CategoryVariants.Where(x => x.Variant.IsConfirmed)
                    ))
            .ForMember(dest => dest.AddedVariantsIds,
            options =>
                options.MapFrom(
                    src => src.ProductVariants
                        .Where(x => x.SellerId == sellerId).Select(x => x.VariantId)
                ));

        this.CreateMap<Entities.CategoryVariant, ShowCategoryVariantInAddVariantViewModel>();

        this.CreateMap<AddVariantViewModel, Entities.ProductVariant>();

        this.CreateMap<Entities.ProductVariant, ShowProductVariantViewModel>()
            .ForMember(dest => dest.StartDateTime,
                options =>
                    options.MapFrom(src =>
                        src.StartDateTime != null
                        ? src.StartDateTime.Value.ToLongPersianDate()
                        : null))
            .ForMember(dest => dest.EndDateTime,
                options =>
                    options.MapFrom(src =>
                        src.EndDateTime != null
                            ? src.EndDateTime.Value.ToLongPersianDate()
                            : null));

        this.CreateMap<Entities.ProductVariant, ShowProductVariantInCreateConsignmentViewModel>();

        this.CreateMap<Entities.ProductVariant, GetProductVariantInCreateConsignmentViewModel>();

        this.CreateMap<Entities.Consignment, ShowConsignmentViewModel>()
            .ForMember(dest => dest.DeliveryDate,
                options =>
                    options.MapFrom(src => src.DeliveryDate.ToLongPersianDate()));

        this.CreateMap<Entities.Consignment, ShowConsignmentDetailsViewModel>()
            .ForMember(dest => dest.DeliveryDate,
                options =>
                    options.MapFrom(src => src.DeliveryDate.ToLongPersianDate()))
            .ForMember(dest => dest.Items,
            options =>
                options.MapFrom(src => src.ConsignmentItems.Where(x => x.ConsignmentId == consignmentId)));

        this.CreateMap<Entities.ConsignmentItem, ShowConsignmentItemViewModel>();

        this.CreateMap<AddProductStockByConsignmentViewModel, Entities.ProductStock>();

        this.CreateMap<Entities.Product, ShowProductInfoViewModel>()
            .ForMember(dest => dest.Score,
                options =>
                        options.MapFrom(src =>
                            src.ProductComments.Any() ?
                            src.ProductComments.Average(pc => pc.Score)
                            : 0
                    ))
            .ForMember(dest => dest.ProductCommentsCount,
                options =>
                        options.MapFrom(src =>
                        src.ProductComments.Where(x => x.IsConfirmed.Value)
                            .LongCount(pc => pc.CommentTitle != null)
                    ))
            .ForMember(dest => dest.ProductQuestionsCount,
                options =>
                    options.MapFrom(src =>
                        src.ProductsQuestionsAndAnswers.Where(x => x.IsConfirmed)
                            .LongCount(pc => pc.ParentId == null)
                    ))
            .ForMember(dest => dest.SuggestCount,
                options =>
                        options.MapFrom(src =>
                        src.ProductComments
                        .Where(x => x.IsBuyer)
                        .LongCount(pc => pc.Suggest == true)
                    ))
        .ForMember(dest => dest.BuyerCount,
                options =>
                        options.MapFrom(src =>
                        src.ProductComments
                        .LongCount(x => x.IsBuyer)
                    ))
            .ForMember(dest => dest.ProductVariants,
                options =>
                    options.MapFrom(src =>
                        src.ProductVariants.Where(x => x.Count > 0)
                    ))
            .ForMember(dest => dest.IsFavorite,
                options =>
                    options.MapFrom(src =>
                        userId != 0 && src.UserProductsFavorites.Any(x => x.UserId == userId)
                    ))
            .ForMember(dest => dest.IsVariantTypeNull,
                options =>
                    options.MapFrom(src =>
                        src.Category.IsVariantColor == null
                    ))
            .ForMember(dest => dest.ProductComments,
                options =>
                    options.MapFrom(src =>
                        src.ProductComments.Where(x => x.IsConfirmed.Value)
                            .Where(x => x.CommentTitle != null)
                            // جدیدترین دیدگاه ها
                            .OrderByDescending(x => x.Id)
                            .Take(2)
                    ))
            .ForMember(dest => dest.ProductsQuestionsAndAnswers,
                options =>
                    options.MapFrom(src =>
                        src.ProductsQuestionsAndAnswers.Where(x => x.IsConfirmed)
                            .Where(x => x.ParentId == null)
                            // جدیدترین پرسش ها
                            .OrderByDescending(x => x.Id)
                            .Take(2)
                    ));
        //.ForMember(dest => dest.ProductCommentsLongCount,
        //        options =>
        //                options.MapFrom(src =>
        //                src.ProductComments.LongCount()
        //            ));

        this.CreateMap<Entities.ProductCategory, ProductCategoryForProductInfoViewModel>();

        this.CreateMap<Entities.ProductFeature, ProductFeatureForProductInfoViewModel>();

        this.CreateMap<Entities.ProductVariant, ProductVariantForProductInfoViewModel>()
            .ForMember(dest => dest.EndDateTime,
                options =>
                    options.MapFrom(src =>
                        src.EndDateTime != null ? src.EndDateTime.Value.ToString("yyyy/MM/dd HH:mm:ss") : null
                    ))
            .ForMember(dest => dest.IsDiscountActive,
                options =>
                    options.MapFrom(src =>
                        src.OffPercentage != null && (src.StartDateTime <= now && src.EndDateTime >= now)
                    ))
            .ForMember(dest => dest.Count,
                options =>
                    options.MapFrom(src =>
                        src.Count > 3 ? (byte)0 : (byte)src.Count
                    ));

        this.CreateMap<Entities.ProductShortLink, ShowProductShortLinkViewModel>();

        this.CreateMap<Entities.ProductVariant, EditProductVariantViewModel>()
            .ForMember(dest => dest.MainPicture,
                options =>
                    options.MapFrom(src => src.Product.ProductMedia.First().FileName))
            .ForMember(dest => dest.ProductTitle,
                options =>
                    options.MapFrom(src => src.Product.PersianTitle))
            .ForMember(dest => dest.IsDiscountActive,
                options =>
                    options.MapFrom(src =>
                            src.OffPercentage != null && (src.StartDateTime <= now && src.EndDateTime >= now)
                        ))
            .ForMember(dest => dest.CommissionPercentage,
                options =>
                    options.MapFrom(
                        src => src.Product.Category.CategoryBrands
                            .Select(x => new
                            {
                                x.BrandId,
                                x.CommissionPercentage
                            })
                            .Single(x => x.BrandId == src.Product.BrandId)
                            .CommissionPercentage
                    ));
        this.CreateMap<Entities.ProductVariant, AddEditDiscountViewModel>()
            .ForMember(dest => dest.MainPicture,
                options =>
                    options.MapFrom(src => src.Product.ProductMedia.First().FileName))
            .ForMember(dest => dest.ProductTitle,
                options =>
                    options.MapFrom(src => src.Product.PersianTitle))
            .ForMember(dest => dest.CommissionPercentage,
                options =>
                    options.MapFrom(
                        src => src.Product.Category.CategoryBrands
                            .Select(x => new
                            {
                                x.BrandId,
                                x.CommissionPercentage
                            })
                            .Single(x => x.BrandId == src.Product.BrandId)
                            .CommissionPercentage
                    ));

        this.CreateMap<AddEditDiscountViewModel, Entities.ProductVariant>()
            .ForMember(x => x.Price,
                opt => opt.Ignore())
            .ForMember(x => x.StartDateTime,
                opt => opt.Ignore())
            .ForMember(x => x.EndDateTime,
                opt => opt.Ignore());

        this.CreateMap<Entities.Variant, ShowVariantInEditCategoryVariantViewModel>();
    }
}