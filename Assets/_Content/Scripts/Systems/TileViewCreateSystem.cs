using Leopotam.Ecs;

namespace Mahjong.ECS
{
    sealed class TileViewCreateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Tile, Position>.Exclude<TileView> _tiles = default;

        private readonly ITileViewService _tileViewService = default;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _tiles)
            {
                var value = _tiles.Get1(i).Value;
                var position = _tiles.Get2(i).Value;

                var tileView = _tileViewService.CreateTileAt(value, position);

                _tiles.GetEntity(i).Get<TileView>().Value = tileView;
            }
        }
    }
}
