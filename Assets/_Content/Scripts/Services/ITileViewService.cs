using Leopotam.Ecs.Types;

namespace Mahjong
{
    interface ITileViewService
    {
        Float2 GetViewSize();
        ITileView CreateTileAt(int value, Int3 position);
    }
}
