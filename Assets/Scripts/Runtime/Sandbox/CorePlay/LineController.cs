using System;
using UnityEngine;

namespace Runtime.Sandbox.CorePlay
{
    public class LineController : MonoBehaviour
    {
        private LineRenderer _lineRenderer;

        private void Awake()
        {
            if (!TryGetComponent(out _lineRenderer))
            {
                _lineRenderer = this.gameObject.AddComponent<LineRenderer>();
            }
        }
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        #region Function

        private void LineSetting()
        {
            _lineRenderer.loop = false;
            
        }

        #endregion
    }
}
