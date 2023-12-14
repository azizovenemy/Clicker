using CodeBase.Data;
using CodeBase.Infrastructure.Services.PersistentProgress;
using CodeBase.Infrastructure.Services.SaveLoad;
using CodeBase.StaticData;

namespace CodeBase.Infrastructure.States
{
    public class DataLoadState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public DataLoadState(GameStateMachine stateMachine, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
            LoadProgressOrInitNew();

            _stateMachine.Enter<LoadLevelState, string>(Constants.MainSceneName);
        }

        public void Exit() { }

        private void LoadProgressOrInitNew() =>
            _progressService.Progress = _saveLoadService.LoadProgress() ?? NewProgress();

        private PlayerProgress NewProgress() =>
            new()
            {
                currentEnemyData =
                {
                    index = 1
                },
                playerData =
                {
                    balance = 10,
                    damage = 1,
                    upgradesData =
                    {
                        new UpgradesData(EUpgradeTypeId.AutoDamageIncrease, 0),
                        new UpgradesData(EUpgradeTypeId.PlayerDamageIncrease, 0),
                        new UpgradesData(EUpgradeTypeId.MoneyRewardIncrease, 0)
                    }
                }
            };
    }
}