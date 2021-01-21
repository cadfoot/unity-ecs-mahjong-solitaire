using Leopotam.Ecs;
using Leopotam.Ecs.Types;
using System.Collections.Generic;

namespace Mahjong.ECS.BoardGen
{
    // can't think of a better name
    sealed class BoardGenerationInitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<Position> _positions = default;

        void IEcsInitSystem.Init()
        {
            var lookup = new HashSet<Int3>(new Int3EqualityComparer());
            foreach (var i in _positions)
                lookup.Add(_positions.Get1(i).Value);

            foreach (var i in _positions)
            {
                var position = _positions.Get1(i).Value;
                if (position.Z != 0)
                    continue;

                var hasLeftNeighbour = false;
                foreach (var coord in TileGridUtils.LeftNeighbours)
                {
                    if (lookup.Contains(position + coord))
                    {
                        hasLeftNeighbour = true;
                        break;
                    }
                }

                var hasRightNeighbour = false;
                foreach (var coord in TileGridUtils.RightNeigbours)
                {
                    if (lookup.Contains(position + coord))
                    {
                        hasRightNeighbour = true;
                        break;
                    }
                }

                if (hasRightNeighbour ^ hasLeftNeighbour)
                    _positions.GetEntity(i).Replace(new Vacant());
            }
        }
    }
}
