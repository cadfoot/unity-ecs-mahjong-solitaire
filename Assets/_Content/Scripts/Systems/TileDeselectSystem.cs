using Leopotam.Ecs;

namespace Mahjong.ECS
{
    sealed class TileDeselectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Selected, Tile> _tiles = default;

        void IEcsRunSystem.Run()
        {
            if (_tiles.GetEntitiesCount() < 2)
                return;

            foreach (var i in _tiles)
                _tiles.GetEntity(i).Del<Selected>();
        }
    }
}
