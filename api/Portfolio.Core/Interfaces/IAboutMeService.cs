using Portfolio.Domain.Models;
using System.Threading.Tasks;

namespace Portfolio.Core.Interfaces
{
    public interface IAboutMeService
    {
        Task<AboutMe> GetAboutMe();

        Task SaveAboutMe(AboutMe model);
    }
}
