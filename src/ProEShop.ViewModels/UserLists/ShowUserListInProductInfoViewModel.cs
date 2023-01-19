using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using ProEShop.Common.Constants;

namespace ProEShop.ViewModels.UserLists;

/// <summary>
/// ویوو مدل لیست های کاربر
/// </summary>
public class ShowUserListInProductInfoViewModel
{
    [HiddenInput]
    public long ProductId { get; set; }

    public List<UserListItemForProductInfoViewModel> Items { get; set; }

    public AddUserListViewModel AddUserList { get; set; }
}

public class UserListItemForProductInfoViewModel
{
    public long Id { get; set; }

    public string Title { get; set; }

    /// <summary>
    /// برای مثال داخل صفحه محصول اول هستیم اگر این محصول
    /// در داخل این لیست وجود داره باید تیکش رو فعال کنیم
    /// </summary>
    public bool IsChecked { get; set; }
}

public class AddUserListViewModel
{
    [PageRemote(PageHandler = "CheckForTitle",
        HttpMethod = "GET",
        ErrorMessage = AttributesErrorMessages.RemoteMessage)]
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = AttributesErrorMessages.RequiredMessage)]
    [MaxLength(100, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string Title { get; set; }

    [Display(Name = "توضیحات")]
    [MaxLength(500, ErrorMessage = AttributesErrorMessages.MaxLengthMessage)]
    public string Description { get; set; }
}