using Leopotam.Ecs;

namespace Mahjong.ECS
{
    sealed class TileDestroyMatchedSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Matched, Tile> _tiles = default;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _tiles)
                _tiles.GetEntity(i).Destroy();
        }
    }
}
