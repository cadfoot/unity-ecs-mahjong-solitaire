using Leopotam.Ecs.Types;
using System.Collections.Generic;

// Q: why this exists?
// A: to reduce garbage alloc when comparing Int3's (at least in Unity Editor)
sealed class Int3EqualityComparer : IEqualityComparer<Int3>
{
    int IEqualityComparer<Int3>.GetHashCode(Int3 obj)
    {
        return obj.GetHashCode();
    }

    bool IEqualityComparer<Int3>.Equals(Int3 x, Int3 y)
    {
        return x == y;
    }
}
