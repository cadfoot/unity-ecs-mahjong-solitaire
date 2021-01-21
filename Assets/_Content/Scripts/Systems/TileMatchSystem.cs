using Leopotam.Ecs;

namespace Mahjong.ECS
{
    sealed class TileMatchSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Selected, Tile> _selected = default;

        void IEcsRunSystem.Run()
        {
            if (_selected.GetEntitiesCount() < 2)
                return;

            var mismatch = false;

            var value = _selected.Get2(0).Value;
            foreach (var i in _selected)
            {
                if (_selected.Get2(i).Value != value)
                {
                    mismatch = true;
                    break;
                }
            }

            foreach (var i in _selected)
            {
                var entity = _selected.GetEntity(i);
                if (mismatch)
                    entity.Get<Mismatched>();
                else
                    entity.Get<Matched>();
                entity.Del<Selected>();
            }
        }
    }
}
