using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LevelGenerator : MonoBehaviour
{
    public GameObject capRoom;
    public GameObject[] availableRooms;
    public List<Room> currentRooms = new List<Room>();
    public int roomCount;
    public int generatedLevelLayer;
    private int currentCount;

    public delegate void CompletionCallback();

    public void NewLevel(CompletionCallback Callback)
    {
        currentRooms.Add(Instantiate(capRoom).GetComponent<Room>());
        currentRooms[0].transform.position = Vector3.zero;
        Build(currentRooms[0], Callback);
    }

    void Build(Room room, CompletionCallback Callback)
    {
        if (currentCount < roomCount)
        {
            for (int i = 0; i < room.anchors.Length; i++)
            {
                Transform trans = room.anchors[i];
                if (trans.childCount <= 0)
                {
                    int rand = Random.Range(0, availableRooms.Length);
                    Room newRoom = Instantiate(availableRooms[rand]).GetComponent<Room>();

                    newRoom.transform.position = trans.position;
                    newRoom.transform.rotation = trans.rotation;
                    newRoom.transform.SetParent(trans);

                    rand = Random.Range(0, newRoom.orientations.Length);
                    Room.Orientation orien = newRoom.orientations[rand];
                    Transform child = newRoom.child;

                    child.localRotation = Quaternion.Euler(0, orien.rotation, 0);
                    child.localPosition = orien.offset;

                    GameObject empty = new GameObject();
                    empty.transform.SetParent(orien.point);

                    currentRooms.Add(newRoom);
                    currentCount++;

                    Build(newRoom, Callback);
                }
                else
                {
                    currentCount++;
                }
            }
        }
        else
        {
            for (int i = 0; i < room.anchors.Length; i++)
            {
                Transform trans = room.anchors[i];
                if (trans.childCount <= 0)
                {
                    int rand = Random.Range(0, availableRooms.Length);
                    Room newRoom = Instantiate(capRoom).GetComponent<Room>();

                    newRoom.transform.position = trans.position;
                    newRoom.transform.rotation = trans.rotation;
                    newRoom.transform.SetParent(trans);

                    rand = Random.Range(0, newRoom.orientations.Length);
                    Room.Orientation orien = newRoom.orientations[rand];
                    Transform child = newRoom.child;

                    child.localRotation = Quaternion.Euler(0, orien.rotation, 0);
                    child.localPosition = orien.offset;

                    currentRooms.Add(newRoom);

                    Combine(Callback);

                }
            }
        }
    }

    void Combine(CompletionCallback Callback)
    {
        GameObject newMesh = new GameObject("Level");
        GameObject props = new GameObject("Props");

        foreach (Room room in currentRooms)
        {
            room.props.transform.SetParent(props.transform);
        }

        // Find all mesh filter submeshes and separate them by their cooresponding materials
        ArrayList materials = new ArrayList();
        ArrayList combineInstanceArrays = new ArrayList();

        MeshFilter[] meshFilters = currentRooms[0].GetComponentsInChildren<MeshFilter>();

        foreach (MeshFilter meshFilter in meshFilters)
        {
            MeshRenderer meshRenderer = meshFilter.GetComponent<MeshRenderer>();

            // Handle bad input
            if (!meshRenderer)
            {
                Debug.LogError("MeshFilter does not have a coresponding MeshRenderer.");
                continue;
            }
            if (meshRenderer.materials.Length != meshFilter.sharedMesh.subMeshCount)
            {
                Debug.LogError("Mismatch between material count and submesh count. Is this the correct MeshRenderer?");
                continue;
            }

            for (int s = 0; s < meshFilter.sharedMesh.subMeshCount; s++)
            {
                int materialArrayIndex = 0;
                for (materialArrayIndex = 0; materialArrayIndex < materials.Count; materialArrayIndex++)
                {
                    if (materials[materialArrayIndex] == meshRenderer.sharedMaterials[s])
                        break;
                }

                if (materialArrayIndex == materials.Count)
                {
                    materials.Add(meshRenderer.sharedMaterials[s]);
                    combineInstanceArrays.Add(new ArrayList());
                }

                CombineInstance combineInstance = new CombineInstance();
                combineInstance.transform = meshRenderer.transform.localToWorldMatrix;
                combineInstance.subMeshIndex = s;
                combineInstance.mesh = meshFilter.sharedMesh;
                (combineInstanceArrays[materialArrayIndex] as ArrayList).Add(combineInstance);
            }
        }

        // Get / Create mesh filter
        MeshFilter meshFilterCombine = newMesh.GetComponent<MeshFilter>();
        if (!meshFilterCombine)
            meshFilterCombine = newMesh.AddComponent<MeshFilter>();

        // Combine by material index into per-material meshes
        // also, Create CombineInstance array for next step
        Mesh[] meshes = new Mesh[materials.Count];
        CombineInstance[] combineInstances = new CombineInstance[materials.Count];

        for (int m = 0; m < materials.Count; m++)
        {
            CombineInstance[] combineInstanceArray = (combineInstanceArrays[m] as ArrayList).ToArray(typeof(CombineInstance)) as CombineInstance[];
            meshes[m] = new Mesh();
            meshes[m].CombineMeshes(combineInstanceArray, true, true);

            combineInstances[m] = new CombineInstance();
            combineInstances[m].mesh = meshes[m];
            combineInstances[m].subMeshIndex = 0;
        }

        // Combine into one
        meshFilterCombine.sharedMesh = new Mesh();
        meshFilterCombine.sharedMesh.CombineMeshes(combineInstances, false, false);

        // Destroy other meshes
        foreach (Mesh mesh in meshes)
        {
            mesh.Clear();
            DestroyImmediate(mesh);
        }
        
        // Get / Create mesh renderer
        MeshRenderer meshRendererCombine = newMesh.GetComponent<MeshRenderer>();
        if (!meshRendererCombine)
            meshRendererCombine = newMesh.AddComponent<MeshRenderer>();

        // Assign materials
        Material[] materialsArray = materials.ToArray(typeof(Material)) as Material[];
        meshRendererCombine.materials = materialsArray;

        currentRooms[0].gameObject.SetActive(false);
        newMesh.AddComponent<MeshCollider>();
        newMesh.GetComponent<MeshCollider>().sharedMesh = newMesh.GetComponent<MeshFilter>().sharedMesh;
        newMesh.layer = generatedLevelLayer;
        Callback();
    }
}

