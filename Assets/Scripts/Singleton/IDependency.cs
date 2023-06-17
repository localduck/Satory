using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public interface IDependency<T>
    {
        void Construct(T obj);
    }
}