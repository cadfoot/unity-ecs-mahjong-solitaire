using Leopotam.Ecs;
using UnityEngine;

namespace Mahjong.ECS
{
    sealed class TileSelectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TileView>.Exclude<Locked> _selectableTiles = default;
        private readonly EcsFilter<Selected, Tile> _selectedTiles = default;

        private readonly Camera _camera;

        public TileSelectionSystem(Camera camera)
        {
            _camera = camera;
        }

        private TileSelectionSystem() { }

        void IEcsRunSystem.Run()
        {
            if (!Input.GetMouseButtonUp(0))
                return;

            var origin = _camera.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(origin, Vector2.zero);

            if (hit && hit.collider.TryGetComponent<ITileView>(out var hitView))
            {
                foreach (var i in _selectableTiles)
                {
                    if (_selectableTiles.Get1(i).Value != hitView)
                        continue;

                    var entity = _selectableTiles.GetEntity(i);
                    if (entity.Has<Selected>())
                        entity.Del<Selected>();
                    else
                        entity.Get<Selected>();
                }
            }
            else
            {
                foreach (var i in _selectedTiles)
                    _selectedTiles.GetEntity(i).Del<Selected>();
            }
        }
    }
}
