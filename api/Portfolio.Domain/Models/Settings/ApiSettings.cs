using Portfolio.Domain.Models.Common;

namespace Portfolio.Domain.Models.Settings;

public class ApiSettings : BaseEntity, ISetting
{
    public string ApiUrl { get; set; }
}
