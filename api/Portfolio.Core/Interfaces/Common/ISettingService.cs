using Portfolio.Domain.Models.Common;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces.Common
{
    public interface ISettingService<T> where T : BaseEntity, ISetting
    {
        Task<T> Get();

        Task Save(T settings);
    }
}
