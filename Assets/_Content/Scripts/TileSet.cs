using UnityEngine;
using System.Collections.Generic;

namespace Mahjong
{
    [CreateAssetMenu]
    sealed class TileSet : ScriptableObject
    {
        [SerializeField] private Vector3 _perspectiveOffset;
        public Vector3 PerspectiveOffset => _perspectiveOffset;

        [Space, SerializeField] private Sprite[] _sprites;
        public IReadOnlyList<Sprite> Sprites => _sprites;

        public int TileCount => _sprites.Length;
    }
}
