using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolarBearAttackThreePlus : MonoBehaviour
{
    public GameObject[] crackPieces;
    IEnumerator Attack()
    {
        Transform temp = transform.parent;
        transform.parent = null;

        yield return new WaitForSeconds(0.6f);
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, new Vector3(0.0f,-1.0f,0.0f), 25.0f);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.tag == "Terrain")
            {
                //NOTE: Raycast is less accurate and results in some jank but less computationally heavy
                transform.rotation = Quaternion.LookRotation(hit.normal);
                
                //NOTE: Calculates normals based on closest 3 vertices of the mesh below groundcrack (need to figure out how to go from closest vertex -> face (tri) since 3 closest vertices may not be the correct face)
                //NOTE: Doesn't work because iceberg meshes' isReadable = false in import settings
                /*
                TerrainData data;
                if (hit.collider.gameObject.TryGetComponent(out data))
                {
                    var terrainSize = data.size;
                    float x = transform.position.x / terrainSize.x;
                    float y = transform.position.z / terrainSize.z;
                    var terrainNormal = data.GetInterpolatedNormal(x,y);
                    transform.rotation = Quaternion.LookRotation(terrainNormal);
                }
                else
                {
                    var meshdata = hit.collider.GetComponent<MeshCollider>().sharedMesh;
                    Vector3[] normals = meshdata.normals;
                    var vertices = meshdata.vertices;
                    int[] closestVert= new int[]{0,0,0};
                    float[] vertDistance= new float[]{1000.0f, 1000.0f,1000.0f};
                    for (int i=0;i<vertices.Length;i++)
                    {
                        if ((transform.position-vertices[i]).magnitude < vertDistance[0])
                        {
                            closestVert[0] = i;
                            vertDistance[0] = (transform.position-vertices[i]).magnitude;
                        }
                        else if ((transform.position-vertices[i]).magnitude < vertDistance[1])
                        {
                            closestVert[1] = i;
                            vertDistance[1] = (transform.position-vertices[i]).magnitude;
                        }
                        else if ((transform.position-vertices[i]).magnitude < vertDistance[2])
                        {
                            closestVert[2] = i;
                            vertDistance[2] = (transform.position-vertices[i]).magnitude;
                        }
                    }
                    
                    var terrainNormal = (normals[closestVert[0]] + normals[closestVert[1]] + normals[closestVert[2]]).normalized;
                    transform.rotation = Quaternion.LookRotation(terrainNormal);
                }
                */
                break;
            }
        }
        foreach (GameObject child in crackPieces)
            child.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        foreach (GameObject child in crackPieces)
            child.SetActive(false);

        transform.parent = temp;
        transform.localPosition = new Vector3(0.0f, 0.2f, 3.0f);
    }
    public void SlamAttack()
    {
        StartCoroutine(Attack());
    }
}
