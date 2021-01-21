using Leopotam.Ecs;

namespace Mahjong.ECS
{
    sealed class TileViewUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TileView> _tiles = default;

        void IEcsRunSystem.Run()
        {
            foreach (var i in _tiles)
            {
                var entity = _tiles.GetEntity(i);
                var view = _tiles.Get1(i).Value;

                if (entity.Has<Locked>())
                    view.SetState(TileState.Locked);
                else if (entity.Has<Selected>())
                    view.SetState(TileState.Selected);
                else if (entity.Has<Mismatched>())
                    view.SetState(TileState.Mismatched);
                else if (entity.Has<Matched>())
                    view.SetState(TileState.Released);
                else
                    view.SetState(TileState.Default);
            }
        }
    }
}
