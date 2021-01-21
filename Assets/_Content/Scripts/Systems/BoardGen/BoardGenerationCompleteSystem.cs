using Leopotam.Ecs;

namespace Mahjong.ECS.BoardGen
{
    // disable board generation systems once board is generated
    sealed class BoardGenerationCompleteSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Vacant> _filter = default;

        private readonly EcsSystems _systems = default;

        void IEcsRunSystem.Run()
        {
            if (!_filter.IsEmpty())
                return;

            var runSystems = _systems.GetRunSystems();
            for (int i = 0; i < runSystems.Count; i++)
                _systems.SetRunSystemState(i, false);
        }
    }
}
