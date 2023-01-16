using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProEShop.ViewModels.DiscountNotices;

/// <summary>
/// ویوو مدل ایجاد اطلاع رسانی شگفت انگیز
/// </summary>
public class AddDiscountNoticeViewModel
{
    [HiddenInput]
    public long ProductId { get; set; }

    public bool NoticeViaEmail { get; set; }

    public bool NoticeViaPhoneNumber { get; set; }

    public bool NoticeViaChat { get; set; }

    public string Email { get; set; }

    public string PhoneNumber { get; set; }
}