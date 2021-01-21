using UnityEngine;
using DG.Tweening;
using Leopotam.Ecs.Types;

namespace Mahjong
{
    sealed class TileViewService : MonoBehaviour, ITileViewService
    {
        [SerializeField] TileSet _tileSet;
        [SerializeField] TileView _tileViewPrefab;

        private Transform _parent;

        private void Awake()
        {
            _parent = new GameObject("Tiles").transform;
        }

        public Float2 GetViewSize()
        {
            return (Vector2)_tileViewPrefab.TileSprite.bounds.size;
        }

        public ITileView CreateTileAt(int value, Int3 position)
        {
            var offset = _tileSet.PerspectiveOffset;
            var targetPosition = new Vector3(
                position.X * offset.x + position.Z * offset.z,
                position.Y * offset.y + position.Z * offset.z,
                -position.Z
            );

            var initialPosition = targetPosition;
            initialPosition.y += 20f;

            var tile = Object.Instantiate(_tileViewPrefab, initialPosition, Quaternion.identity, _parent);
            if (value >= 0 && value < _tileSet.Sprites.Count)
            {
                tile.TileSprite.sprite = _tileSet.Sprites[value];
                tile.TileSprite.sortingOrder = position.Z * 100 - position.X - position.Y;
            }

            tile.transform.DOLocalMove(targetPosition, .5f)
                .SetDelay(position.X * .01f + position.Y * .01f + position.Z * .25f);
            
            return tile;
        }
    }
}
