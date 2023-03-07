namespace ProEShop.ViewModels.Addresses;

public class ShowAddressInProfileViewModel
{
    public long Id { get; set; }

    public string AddressLine { get; set; }

    public string FullName { get; set; }

    public string CityTitle { get; set; }

    /// <summary>
    /// کد پستی
    /// Post office box
    /// </summary>
    public string Pob { get; set; }

    public string PhoneNumber { get; set; }
}