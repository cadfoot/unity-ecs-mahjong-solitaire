using Leopotam.Ecs;
using UnityEngine;

namespace Mahjong.ECS
{
    [RequireComponent(typeof(TileViewService))]
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private bool _createDebugObserversOnStartup = false;

        [Space, SerializeField] private TextAsset _mapFile;
        [SerializeField] private TileSet _tileSet;

        private EcsWorld _world;
        private EcsSystems _mainSystems;

        private void Start()
        {
            var camera = Camera.main;

            _world = new EcsWorld();
            _mainSystems = new EcsSystems(_world);
#if UNITY_EDITOR
            if (_createDebugObserversOnStartup)
            {
                Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
                Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_mainSystems);
            }
#endif

            var boardGenSystems = new EcsSystems(_world, "board_gen")
                .Add(new BoardGen.BoardLayoutInitSystem())
                .Add(new BoardGen.BoardGenerationInitSystem())
                .Add(new BoardGen.BoardTilePairCreateSystem(_tileSet.TileCount))
                .Add(new BoardGen.BoardGenerationStepSystem())
                .Add(new BoardGen.BoardGenerationCompleteSystem())

                .Add(new CameraSetupSystem(camera))
                
                .Inject((IMapService)new TextMapService(_mapFile.text));
                
            boardGenSystems.Inject(boardGenSystems);

            _mainSystems
                .Add(boardGenSystems)

                .Add(new TileLockSystem())
                .Add(new TileSelectionSystem(camera))
                .Add(new TileMatchSystem())

                .Add(new TileViewCreateSystem())
                .Add(new TileViewUpdateSystem())

                .Add(new TileDestroyMatchedSystem())

                .OneFrame<Locked>()
                .OneFrame<Matched>()
                .OneFrame<Mismatched>()

                .Inject(GetComponent<ITileViewService>())

                .Init();
        }

        private void Update()
        {
            _mainSystems?.Run();
        }

        private void OnDestroy()
        {
            if (_mainSystems != null)
            {
                _mainSystems.Destroy();
                _mainSystems = null;
                _world.Destroy();
                _world = null;
            }
        }
    }
}
