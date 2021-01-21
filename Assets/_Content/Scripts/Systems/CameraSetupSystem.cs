using UnityEngine;
using Leopotam.Ecs;
using Leopotam.Ecs.Types;

namespace Mahjong.ECS
{
    sealed class CameraSetupSystem : IEcsInitSystem
    {
        private readonly EcsFilter<Position> _positions = default;

        private readonly ITileViewService _tileViewService = default;

        private readonly Camera _camera;

        public CameraSetupSystem(Camera camera)
        {
            _camera = camera;
        }

        private CameraSetupSystem() { }

        public void Init()
        {
            var viewExtents = _tileViewService.GetViewSize() * .5f;

            var boardCenter = new Float2();
            var count = 0;

            foreach (var i in _positions)
            {
                var position = _positions.Get1(i).Value;
                if (position.Z != 0)
                    continue;

                boardCenter.X += position.X;
                boardCenter.Y += position.Y;
                count++;
            }

            boardCenter *= viewExtents;
            boardCenter /= count;

            _camera.transform.position = new Vector3(
                boardCenter.X,
                boardCenter.Y,
                _camera.transform.position.z
            );
        }
    }
}
