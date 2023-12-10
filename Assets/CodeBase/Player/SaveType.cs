using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.Services.SaveLoad;

namespace CodeBase.Player
{
    public class SaveType
    {
        private readonly ISaveLoadService _saveLoadService;

        public SaveType()
        {
            _saveLoadService = ServiceLocator.Container.Single<ISaveLoadService>();
        }

        public void SaveProgress()
        {
            _saveLoadService.SaveProgress();
        }
    }
}