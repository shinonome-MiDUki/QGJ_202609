using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaySpreading : BaseMeshEffect
{
    private float ray_source = 200.0f;
    private float spreading = 1.0f;
    public override void ModifyMesh(VertexHelper vh)
    {
        if (!IsActive())
            return;

        var vertCount = vh.currentVertCount;
        UIVertex vert = new UIVertex();

        foreach (int i in new List<int>(){0, 3})
        {
            vh.PopulateUIVertex(ref vert, i);
            var pos = vert.position;
            float direction = i == 0 ? -1.0f : 1.0f;
            pos.x += direction * 50.0f * spreading;
            pos.y = -300.0f;
            vert.position = pos;
            vh.SetUIVertex(vert, i);
        }
        foreach (int i in new List<int>(){1, 2})
        {
            vh.PopulateUIVertex(ref vert, i);
            var pos = vert.position;
            pos.y = ray_source;
            vert.position = pos;
            vh.SetUIVertex(vert, i);
        }
    }

    public void RefreshMesh(float new_ray_source, float new_spreading)
    {
        ray_source = new_ray_source;
        spreading = new_spreading;
        if (graphic != null)
        {
            graphic.SetVerticesDirty(); 
        }
    }
}