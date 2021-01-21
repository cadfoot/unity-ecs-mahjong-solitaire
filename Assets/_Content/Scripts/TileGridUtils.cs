using Leopotam.Ecs.Types;

namespace Mahjong
{
    static class TileGridUtils
    {
        public static readonly Int3[] LeftNeighbours = new [] {
            new Int3(-2, -1, +0),
            new Int3(-2, +0, +0),
            new Int3(-2, +1, +0),
        };

        public static readonly Int3[] RightNeigbours = new [] {
            new Int3(2, -1, +0),
            new Int3(2, +0, +0),
            new Int3(2, +1, +0),
        };

        public static readonly Int3[] TopNeigbours = new [] {
            new Int3(-1, -1, +1),
            new Int3(-1, +0, +1),
            new Int3(-1, +1, +1),
            new Int3(+0, -1, +1),
            new Int3(+0, +0, +1),
            new Int3(+0, +1, +1),
            new Int3(+1, -1, +1),
            new Int3(+1, +0, +1),
            new Int3(+1, +1, +1),
        };
    }
}
