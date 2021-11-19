using AutoMapper;
using Portfolio.Core.Interfaces;
using Portfolio.Core.Interfaces.Common;
using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Core.Services
{
    public class AboutMeService : IAboutMeService
    {
        #region Fields

        private readonly IBaseRepository<AboutMe> _aboutMeRepository;

        #endregion

        #region Constructor

        public AboutMeService(IBaseRepository<AboutMe> aboutMeRepository)
        {
            _aboutMeRepository = aboutMeRepository;
        }

        #endregion

        #region Methods

        public Task<AboutMe> GetAboutMe()
        {
            return _aboutMeRepository.FirstAsync();
        }

        public async Task SaveAboutMe(AboutMe model)
        {
            var aboutMe = await GetAboutMe();
            if(aboutMe == null)
            {
                await _aboutMeRepository.InsertAsync(model);
                return;
            }

            await Update(model, aboutMe);
        }

        #region Utils

        private Task Update(AboutMe model, AboutMe original)
        {
            original.Content = model.Content;
            original.Title = model.Title;

            return _aboutMeRepository.UpdateAsync(original);
        }

        #endregion

        #endregion

    }
}
