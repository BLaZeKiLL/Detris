using System.Collections.Generic;

using UnityEngine;

namespace CodeBlaze.Detris.Grid {

    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class SimpleGrid : MonoBehaviour {

        [SerializeField]
        private int _gridSize;

        void Awake()
        {
            MeshFilter filter = gameObject.GetComponent<MeshFilter>();        
            var mesh = new Mesh();
            var verticies = new List<Vector3>();

            var indicies = new List<int>();
            for (int i = 0; i <= _gridSize; i++)
            {
                verticies.Add(new Vector3(i, 0, 0));
                verticies.Add(new Vector3(i, 0, _gridSize));

                indicies.Add(4 * i + 0);
                indicies.Add(4 * i + 1);

                verticies.Add(new Vector3(0, 0, i));
                verticies.Add(new Vector3(_gridSize, 0, i));

                indicies.Add(4 * i + 2);
                indicies.Add(4 * i + 3);
            }

            mesh.vertices = verticies.ToArray(); 
            mesh.SetIndices(indicies.ToArray(), MeshTopology.Lines, 0);
            filter.mesh = mesh;

            var meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material = new Material(Shader.Find("Sprites/Default")) {
                color = Color.white
            };
        }

    }

}