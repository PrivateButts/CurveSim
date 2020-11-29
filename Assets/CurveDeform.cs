using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurveDeform : MonoBehaviour{
    Mesh deformingMesh, physMesh;
    Vector3[] originalVertices, displacedVertices;
    float mpDist;
    
    public float radius;

    // Start is called before the first frame update
    void Start(){
        deformingMesh = GetComponent<MeshFilter>().mesh;
        originalVertices = deformingMesh.vertices;
        displacedVertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++) {
            displacedVertices[i] = originalVertices[i];
        }
        Deform();
    }

    void Deform() {
        for (int i = 0; i < originalVertices.Length; i++) {
            displacedVertices[i] = originalVertices[i];
            mpDist = Mathf.Sqrt(Mathf.Pow(originalVertices[i].x - 0, 2) + Mathf.Pow(originalVertices[i].z - 0, 2)) * 2;
            displacedVertices[i].y = displacedVertices[i].y - (radius - Mathf.Sqrt(radius * radius - mpDist * mpDist / 4));
        }
        deformingMesh.vertices = displacedVertices;
        deformingMesh.RecalculateNormals();

        DestroyImmediate(gameObject.GetComponent<MeshCollider>());
        var collider = gameObject.AddComponent<MeshCollider>();
        collider.convex = true;
        collider.sharedMesh = deformingMesh;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetKeyDown("r")) {
            Deform();
        }
    }
}
