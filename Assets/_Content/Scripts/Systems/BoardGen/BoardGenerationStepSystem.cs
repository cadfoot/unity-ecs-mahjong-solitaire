using Leopotam.Ecs;
using Leopotam.Ecs.Types;
using System.Collections.Generic;

namespace Mahjong.ECS.BoardGen
{
    // can't think of a better name
    sealed class BoardGenerationStepSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Vacant, Tile, Position> _takenPositions = default;
        private readonly EcsFilter<Position>.Exclude<Vacant, Tile> _otherPositions = default;

        private readonly Dictionary<Int3, EcsEntity> _lookup = new Dictionary<Int3, EcsEntity>();

        void IEcsRunSystem.Run()
        {
            _lookup.Clear();

            foreach (var i in _otherPositions)
                _lookup[_otherPositions.Get1(i).Value] = _otherPositions.GetEntity(i);

            foreach (var i in _takenPositions)
            {
                _takenPositions.GetEntity(i).Del<Vacant>();

                var position = _takenPositions.Get3(i).Value;

                MarkNeighbours(TileGridUtils.TopNeigbours);
                MarkNeighbours(TileGridUtils.LeftNeighbours);
                MarkNeighbours(TileGridUtils.RightNeigbours);

                void MarkNeighbours(Int3[] neigbours)
                {
                    foreach (var coord in neigbours)
                    {
                        if (_lookup.TryGetValue(position + coord, out var entity))
                            entity.Get<Vacant>();
                    }
                }
            }
        }
    }
}
