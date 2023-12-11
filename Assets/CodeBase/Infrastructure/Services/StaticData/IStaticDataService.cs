using CodeBase.StaticData;

namespace CodeBase.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadEnemies();
        EnemyData ForEnemy(EEnemyTypeId typeId);
        LevelData ForLevel(string sceneKey);
    }
}