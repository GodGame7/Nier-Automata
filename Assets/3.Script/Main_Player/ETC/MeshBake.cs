using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshBake : MonoBehaviour
{
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    public Material material;

    private MeshFilter[] meshFilters;
    private MeshRenderer[] meshRenderers;

    private Mesh[] bakedMeshes;
    private GameObject[] ghostContainers;

    public float delayTime = 1.0f;
    private float nowT = 0.0f;

    public bool TrailMotion;

    private void Start()
    {
        nowT = 0.0f;
        skinnedMeshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
        int count = skinnedMeshRenderers.Length;

        bakedMeshes = new Mesh[count];
        ghostContainers = new GameObject[count];
        meshFilters = new MeshFilter[count];
        meshRenderers = new MeshRenderer[count];
        GameObject obj = new GameObject("MeshContainer");
        for (int i = 0; i < count; i++)
        {
            bakedMeshes[i] = new Mesh();
            ghostContainers[i] = new GameObject("VFX_UnitGhost" + i);
            ghostContainers[i].transform.SetParent(obj.transform);
            meshFilters[i] = ghostContainers[i].AddComponent<MeshFilter>();
            meshFilters[i].sharedMesh = bakedMeshes[i];

            meshRenderers[i] = ghostContainers[i].AddComponent<MeshRenderer>();
            meshRenderers[i].material = material;
        }
    }

    public void Update()
    {
        if (TrailMotion)
        {
            nowT += Time.deltaTime;

            if (delayTime < nowT)
            {
                nowT = 0.0f;

                unitGhost();
            }
        }
    }

    public void OnTrail()
    {
        TrailMotion = true;
    }
    public void OffTrail()
    {
        TrailMotion = false;
        for (int i = 0; i < ghostContainers.Length; i++)
        {
            ghostContainers[i].SetActive(false);
        }
    }
    public void unitGhost()
    {
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            skinnedMeshRenderers[i].BakeMesh(bakedMeshes[i]);
            ghostContainers[i].transform.rotation = transform.rotation;
            ghostContainers[i].transform.position = transform.position;
            ghostContainers[i].SetActive(true);
        }
    }

}
