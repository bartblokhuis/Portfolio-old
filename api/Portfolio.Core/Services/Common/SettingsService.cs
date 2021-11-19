using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models.Common;
using System.Threading.Tasks;

namespace Portfolio.Core.Services.Common
{
    public class SettingsService<T> : ISettingService<T> where T: BaseEntity, ISetting
    {
        #region Fields

        private readonly IBaseRepository<T> _repository;

        #endregion

        #region Constructor

        public SettingsService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Methods

        public Task<T> Get()
        {
            return _repository.FirstAsync();
        }

        public Task Save(T settings)
        {
            return (settings.Id == 0) ?
                _repository.InsertAsync(settings) :
                _repository.UpdateAsync(settings);
        }

        #endregion

    }
}
