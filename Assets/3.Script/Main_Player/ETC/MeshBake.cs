using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ghost
{
    public MeshFilter[] meshFilters;
    public MeshRenderer[] meshRenderers;
    
    public Mesh[] bakedMeshes;
    public GameObject[] ghostContainers;

    public Material material;

    public Ghost()
    {
        
    }
    public void Init(int targetMeshCount)
    {
        bakedMeshes = new Mesh[targetMeshCount];
        ghostContainers = new GameObject[targetMeshCount];
        meshFilters = new MeshFilter[targetMeshCount];
        meshRenderers = new MeshRenderer[targetMeshCount];
        GameObject obj = new GameObject("MeshContainer");
        for (int i = 0; i < targetMeshCount; i++)
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
}

public class MeshBake : MonoBehaviour
{
    public SkinnedMeshRenderer[] skinnedMeshRenderers;
    public Material material;

    
    [SerializeField] Transform shoulder;

    //private MeshFilter[] meshFilters;
    //private MeshRenderer[] meshRenderers;

    //private Mesh[] bakedMeshes;
    //private GameObject[] ghostContainers;

    [SerializeField]
    private Ghost ghost1;
    [SerializeField]
    private Ghost ghost2;
    [SerializeField]
    private Ghost ghost3;
    [SerializeField]
    private Ghost ghost4;
    Ghost[] allghost = new Ghost[4];

    public float delayTime = 1.0f;
    private float nowT = 0.0f;

    public bool TrailMotion;

    private void Start()
    {
        nowT = 0.0f;
        allghost[0] = ghost1;
        allghost[1] = ghost2;
        allghost[2] = ghost3;
        allghost[3] = ghost4;
        int count = skinnedMeshRenderers.Length;

        ghost1.Init(count);
        ghost2.Init(count);
        ghost3.Init(count);
        ghost4.Init(count);
        //bakedMeshes = new Mesh[count];
        //ghostContainers = new GameObject[count];
        //meshFilters = new MeshFilter[count];
        //meshRenderers = new MeshRenderer[count];
        //GameObject obj = new GameObject("MeshContainer");
        //for (int i = 0; i < count; i++)
        //{
        //    bakedMeshes[i] = new Mesh();
        //    ghostContainers[i] = new GameObject("VFX_UnitGhost" + i);
        //    ghostContainers[i].transform.SetParent(obj.transform);
        //    meshFilters[i] = ghostContainers[i].AddComponent<MeshFilter>();
        //    meshFilters[i].sharedMesh = bakedMeshes[i];

        //    meshRenderers[i] = ghostContainers[i].AddComponent<MeshRenderer>();
        //    meshRenderers[i].material = material;
        //}
    }

    int index = 0;
    public void Update()
    {
        if (TrailMotion)
        {
            nowT += Time.deltaTime;

            if (delayTime < nowT)
            {
                nowT = 0.0f;
                if (index % 4 == 0)
                {
                    unitGhost(ghost1);
                }
                else if(index % 4 == 1)
                {
                    unitGhost(ghost2);
                }
                else if (index % 4 == 2)
                {
                    unitGhost(ghost3);
                }
                else if (index % 4 == 3)
                {
                    unitGhost(ghost4);
                }
                index++;
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
        for (int i = 0; i < allghost.Length; i++)
        {
            for (int j = 0; j < allghost[i].ghostContainers.Length; j++)
            {
                allghost[i].ghostContainers[j].SetActive(false);
            }
        }

    }

    public void unitGhost(Ghost ghost)
    {
        for (int i = 0; i < skinnedMeshRenderers.Length; i++)
        {
            skinnedMeshRenderers[i].BakeMesh(ghost.bakedMeshes[i]);
            if (i < 2)
            {
                ghost.ghostContainers[i].transform.rotation = shoulder.rotation;
                ghost.ghostContainers[i].transform.position = shoulder.position; }
            else 
            {
                ghost.ghostContainers[i].transform.rotation = transform.rotation;
                ghost.ghostContainers[i].transform.position = transform.position; 
            }
            ghost.ghostContainers[i].SetActive(true);
        }
    }

    public void DodgeEffect()
    {
        OffTrail();
        unitGhost(ghost1);
        unitGhost(ghost2);
        unitGhost(ghost3);
        unitGhost(ghost4);
        StartCoroutine(Dodge());
        IEnumerator Dodge()
        {
            float count = 0f;
            while (count < 0.6f)
            {
                ghost1.ghostContainers[0].transform.root.transform.Translate(3f*Vector3.forward * Time.deltaTime);
                ghost2.ghostContainers[0].transform.root.transform.Translate(3f*Vector3.back * Time.deltaTime);
                ghost3.ghostContainers[0].transform.root.transform.Translate(3f*Vector3.right * Time.deltaTime);
                ghost4.ghostContainers[0].transform.root.transform.Translate(3f*Vector3.left * Time.deltaTime);
                count += Time.deltaTime;
                yield return null;
            }
            for (int i = 0; i < allghost.Length; i++)
            {
                for (int j = 0; j < allghost[i].ghostContainers.Length; j++)
                {
                    allghost[i].ghostContainers[j].SetActive(false);
                }
            }
        }
    }
}
