using System;
using Leopotam.Ecs;

namespace Mahjong.ECS.BoardGen
{
    sealed class BoardTilePairCreateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Vacant, Position> _positions = default;

        private readonly Random _rng = new Random();

        private readonly int _tileTypeCount;

        public BoardTilePairCreateSystem(int tileTypeCount)
        {
            _tileTypeCount = tileTypeCount;
        }

        void IEcsRunSystem.Run()
        {
            var positionCount = _positions.GetEntitiesCount();

            if (positionCount < 2)
                return;

            var indexes = GetRandomPair();

            var first = _positions.GetEntity(indexes.first);
            var second = _positions.GetEntity(indexes.second);

            var value = _rng.Next(_tileTypeCount);

            first.Replace(new Tile { Value = value });
            second.Replace(new Tile { Value = value });

            (int first, int second) GetRandomPair()
            {
                var first = _rng.Next(positionCount);
                var second = first;
                while (first == second)
                    second = _rng.Next(positionCount);

                return (first, second);
            }
        }

    }
}
