using UnityEngine;
using System.Collections.Generic;
using System;
public class ColliderToFit : MonoBehaviour
{
    [ContextMenu("Fit Colliders")]
    public void FitColliderToChildren()
    {
        var parentObject = gameObject;
        var bc = parentObject.GetComponent<BoxCollider>();
        if (bc == null) bc = parentObject.AddComponent<BoxCollider>();
        var bounds = new Bounds(Vector3.zero, Vector3.zero);
        var hasBounds = false;
        var renderers = parentObject.GetComponentsInChildren<Renderer>();
        var children = GetComponentsInChildren<Transform>();
        foreach (var t in children)
        {
            var childgo = t.gameObject;
            var childmc = childgo.GetComponent<MeshCollider>();
            var childbc = childgo.GetComponent<BoxCollider>();
            if (childmc)
                DestroyImmediate(t.gameObject.GetComponent<MeshCollider>());
            if (!childbc && childgo.GetComponent<Renderer>())
            {
                if (childbc != null)
                {
                    DestroyImmediate(childgo.GetComponent<BoxCollider>());
                }
                if (!childbc.transform.parent.GetComponent<BoxCollider>())
                {
                    childgo.AddComponent<BoxCollider>();
                }
            }
        }
        foreach (var render in renderers)
        {
            if (hasBounds)
                bounds.Encapsulate(render.bounds);
            else
            {
                bounds = render.bounds;
                hasBounds = true;
            }
        }
        if (hasBounds)
        {
            bc.center = bounds.center - parentObject.transform.position;
            bc.size = bounds.size;
        }
        else
        {
            bc.size = bc.center = Vector3.zero;
            bc.size = Vector3.zero;
        }
    }
}