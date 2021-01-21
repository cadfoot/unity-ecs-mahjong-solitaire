using System.Collections;
using System.Collections.Generic;
using Leopotam.Ecs.Types;
using System;

namespace Mahjong
{
    sealed class TextMapService : IMapService
    {
        private readonly string[] _raw;

        public TextMapService(string text)
        {
            _raw = text.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        }

        public IEnumerator<Int3> GetEnumerator()
        {
            foreach (var line in _raw)
            {
                var coords = line.Split(' ');
                var layer = int.Parse(coords[0]);
                var x = int.Parse(coords[1]);
                var y = int.Parse(coords[2]);

                yield return new Int3(x, y, layer);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
