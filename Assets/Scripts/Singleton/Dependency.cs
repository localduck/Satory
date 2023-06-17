using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    public class Dependency : MonoBehaviour
    {
        protected virtual void BindAll(MonoBehaviour monoBehaviourInScene) { }
        protected void RindAllObjectToBind()
        {
            MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();

            for (int i = 0; i < monoInScene.Length; i++)
            {
                BindAll(monoInScene[i]);
            }
        }

        protected void Bind<T>(MonoBehaviour bindObject, MonoBehaviour target) where T : class
        {
            if (target is IDependency<T>) (target as IDependency<T>).Construct(bindObject as T);
        }
    }
}
