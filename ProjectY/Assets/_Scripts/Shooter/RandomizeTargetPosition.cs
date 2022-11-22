using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeTargetPosition : MonoBehaviour
{
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _target;
    [SerializeField] private Transform _outerRing;

    private void Start() => Randomize();

    private void Randomize()
    {
        Bounds(out Vector3 bounds);
        float xOffSet = Random.Range(-bounds.x, bounds.x) + _body.position.x;
        float yOffSet = Random.Range(-bounds.y, bounds.y) + _body.position.y;
        Vector3 pos = new(xOffSet, yOffSet, _target.position.z);
        _target.position = pos;
    }

    private void Bounds(out Vector3 bounds)
    {
        bounds = Vector3.zero;
        if (_target == null || _target == null || _outerRing == null)
            return;
        float scale = _target.localScale.x;
        float outerRingDiameter = _outerRing.localScale.x;
        float totalDiameter = scale * outerRingDiameter;

        Vector3 targetSize = new(totalDiameter, totalDiameter);
        Vector3 b = _body.localScale - targetSize;
        b.z = 0.001f;
        bounds = b / 2;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.black;
    //    Bounds(out Vector3 bounds);
    //    Vector3 pos = _body.position;
    //    pos.z = _target.position.z;
    //    Gizmos.DrawCube(pos, bounds * 2);
    //}
}
