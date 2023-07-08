using System;
using UnityEngine;

namespace Runtime.Sandbox.CorePlay
{
    public class PolygonDraw : MonoBehaviour
    {
        #region 字段
        [SerializeField]
        private Vector2 _centre = Vector2.zero;
        [SerializeField]
        private float _radius = 1;
        [SerializeField]
        private int _segments = 20;
        
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        /*private Mesh _mesh;*/

        private bool _dirty;
        

        #endregion
        

        #region 属性
        public float Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                SetDirty();
            }
        }

        public int Segments
        {
            get => _segments;
            set
            {
                _segments = value;
                SetDirty();
            }
        }
        

        #endregion
        

        #region lifetime
        private void Awake()
        {
            if (!TryGetComponent(out _meshFilter))
            {
                _meshFilter = this.gameObject.AddComponent<MeshFilter>();
            }

            if (!TryGetComponent(out _meshRenderer))
            {
                _meshRenderer = this.gameObject.AddComponent<MeshRenderer>();
            }

            //_mesh = _meshFilter.mesh == null ? new Mesh() : _meshFilter.mesh;

        }
        void Start()
        {
            DrawMesh();
        }

        // Update is called once per frame
        void Update()
        {
            if (_dirty)
            {
                DrawMesh();
            }
        }
        

        #endregion
        
        #region Helper
        
        public Mesh CreateMesh(Vector2 centre,float radius,int segments)
        {
            //vertices:
            int verticesCount = segments + 1;
            Vector3[] vertices = new Vector3[verticesCount];
            vertices[0] = Vector3.zero;
            float angleDegree = 360.0f;
            float angleRad = Mathf.Deg2Rad * angleDegree;
            float angleCur = angleRad;
            float angleDelta = angleRad / segments;
            for(int i=1;i< verticesCount; i++)
            {
                float cosA = Mathf.Cos(angleCur);
                float sinA = Mathf.Sin(angleCur);

                vertices[i] = new Vector3(radius * cosA,radius * sinA, 0);
                angleCur -= angleDelta;
            }

            //triangles
            int triangleCount = segments * 3;
            int[] triangles = new int[triangleCount];
            for(int i=0,vi=1;i<= triangleCount-1;i+=3,vi++)     //因为该案例分割了60个三角形，故最后一个索引顺序应该是：0 60 1；所以需要单独处理
            {
                triangles[i] = 0;
                triangles[i + 1] = vi;
                triangles[i + 2] = vi + 1;
            }
            triangles[triangleCount - 3] = 0;
            triangles[triangleCount - 2] = verticesCount - 1;
            triangles[triangleCount - 1] = 1;                  //为了完成闭环，将最后一个三角形单独拎出来

            //uv:
            Vector2[] uvs = new Vector2[verticesCount];
            for (int i = 0; i < verticesCount; i++)
            {
                uvs[i] = new Vector2(vertices[i].x / radius / 2 + 0.5f, vertices[i].y / radius / 2 + 0.5f);
            }

            //负载属性与mesh
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.uv = uvs;
            return mesh;
        }

        public void DrawMesh()
        {
            _meshFilter.mesh = CreateMesh(_centre, _radius, _segments);
            _dirty = false;
        }
        public void SetDirty()
        {
            _dirty = true;
        }
        

        #endregion

        
        
        
    }
}
