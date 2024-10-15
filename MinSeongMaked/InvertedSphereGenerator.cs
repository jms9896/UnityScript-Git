using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class InvertedSphereGenerator : MonoBehaviour
{
    [Range(4, 100)]
    public int segments = 32;

    private void Start()
    {
        GenerateSphere();
    }

    private void OnValidate()
    {
        GenerateSphere();
    }

    private void GenerateSphere()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[(segments + 1) * (segments + 1)];
        int[] triangles = new int[segments * segments * 6];
        Vector2[] uv = new Vector2[vertices.Length];

        for (int lat = 0; lat <= segments; lat++)
        {
            for (int lon = 0; lon <= segments; lon++)
            {
                int index = lat * (segments + 1) + lon;
                float theta = lat * Mathf.PI / segments;
                float phi = lon * 2 * Mathf.PI / segments;

                float x = Mathf.Sin(theta) * Mathf.Cos(phi);
                float y = Mathf.Cos(theta);
                float z = Mathf.Sin(theta) * Mathf.Sin(phi);

                vertices[index] = new Vector3(x, y, z);
                uv[index] = new Vector2((float)lon / segments, (float)lat / segments);
            }
        }

        int triIndex = 0;
        for (int lat = 0; lat < segments; lat++)
        {
            for (int lon = 0; lon < segments; lon++)
            {
                int current = lat * (segments + 1) + lon;
                int next = current + segments + 1;

                // 수정된 부분: 삼각형 순서를 반대로 하여 내부를 향하게 함
                triangles[triIndex++] = current;
                triangles[triIndex++] = next;
                triangles[triIndex++] = current + 1;

                triangles[triIndex++] = next;
                triangles[triIndex++] = next + 1;
                triangles[triIndex++] = current + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;
        mesh.RecalculateNormals();
    }
}