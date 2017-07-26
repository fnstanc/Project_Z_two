using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PolyImage : Image
{
    /// <summary>
    /// </summary>
    /// <param name="vh"></param>
    private float pi = 3.1415f;
    [SerializeField]
    private float rid = 60;
    protected override void OnPopulateMesh(VertexHelper vh)
    {
        var r = GetPixelAdjustedRect();
        var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);

        Color32 color32 = color;
        vh.Clear();
        //vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(0f, 0f));
        //vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(0f, 1f));
        //vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(1f, 1f));
        //vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(1f, 0f));        

        vh.AddVert(new Vector3((float)(rid * Math.Cos(240 * pi / 180)), (float)(rid * Math.Sin(240 * pi / 180))), color32, new Vector2(0.25f, 0f));
        vh.AddVert(new Vector3((float)(rid * Math.Cos(300 * pi / 180)), (float)(rid * Math.Sin(300 * pi / 180))), color32, new Vector2(0.75f, 0f));
        vh.AddVert(new Vector3((float)(rid * Math.Cos(360 * pi / 180)), (float)(rid * Math.Sin(360 * pi / 180))), color32, new Vector2(1f, 0.5f));
        vh.AddVert(new Vector3((float)(rid * Math.Cos(60 * pi / 180)), (float)(rid * Math.Sin(60 * pi / 180))), color32, new Vector2(0.75f, 1f));
        vh.AddVert(new Vector3((float)(rid * Math.Cos(120 * pi / 180)), (float)(rid * Math.Sin(120 * pi / 180))), color32, new Vector2(0.25f, 1f));
        vh.AddVert(new Vector3((float)(rid * Math.Cos(180 * pi / 180)), (float)(rid * Math.Sin(180 * pi / 180))), color32, new Vector2(0f, 0.5f));

        vh.AddTriangle(0, 1, 2);
        vh.AddTriangle(2, 3, 4);
        vh.AddTriangle(4, 5, 0);
        vh.AddTriangle(0, 2, 4);
    }


}
