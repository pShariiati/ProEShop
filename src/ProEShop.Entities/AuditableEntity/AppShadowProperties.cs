using System;

namespace ProEShop.Entities.AuditableEntity
{
    public class AppShadowProperties
    {
        public string UserAgent { set; get; }
        public string UserIp { set; get; }
        public DateTime Now { set; get; }
        public long? UserId { set; get; }
    }
}