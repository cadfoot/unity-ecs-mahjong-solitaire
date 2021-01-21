using Leopotam.Ecs;

namespace Mahjong.ECS.BoardGen
{
    sealed class BoardLayoutInitSystem : IEcsPreInitSystem
    {
        private readonly EcsWorld _world = default;

        private readonly IMapService _map = default;

        void IEcsPreInitSystem.PreInit()
        {
            foreach (var coord in _map)
            {
                var pos = coord;
                pos.Z--;
                _world.NewEntity().Replace(new Position() { Value = pos });
            }
        }
    }
}
