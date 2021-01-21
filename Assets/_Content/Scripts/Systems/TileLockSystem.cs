using Leopotam.Ecs;
using Leopotam.Ecs.Types;
using System.Collections.Generic;

namespace Mahjong.ECS
{
    sealed class TileLockSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Tile, Position>.Exclude<Locked> _tiles = default;

        private readonly HashSet<Int3> _occupiedCache = new HashSet<Int3>(new Int3EqualityComparer());

        void IEcsRunSystem.Run()
        {
            _occupiedCache.Clear();
            foreach (var i in _tiles)
                _occupiedCache.Add(_tiles.Get2(i).Value);

            foreach (var i in _tiles)
            {
                var position = _tiles.Get2(i).Value;

                var leftLocked = false;
                foreach (var coord in TileGridUtils.LeftNeighbours)
                {
                    if (_occupiedCache.Contains(position + coord))
                    {
                        leftLocked = true;
                        break;
                    }
                }

                var rightLocked = false;
                foreach (var coord in TileGridUtils.RightNeigbours)
                {
                    if (_occupiedCache.Contains(position + coord))
                    {
                        rightLocked = true;
                        break;
                    }
                }

                if (leftLocked && rightLocked)
                {
                    _tiles.GetEntity(i).Get<Locked>();
                    continue;
                }

                foreach (var coord in TileGridUtils.TopNeigbours)
                {
                    if (_occupiedCache.Contains(position + coord))
                    {
                        _tiles.GetEntity(i).Get<Locked>();
                        continue;
                    }
                }
            }
        }
    }
}
