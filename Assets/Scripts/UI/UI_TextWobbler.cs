using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class UI_TextWobbler : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private Vector2 _strength;
        [SerializeField] private float _speed;
        private Mesh _mesh;
        private Vector3[] _vs;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        private void FixedUpdate()
        {
            _text.ForceMeshUpdate();
            _mesh = _text.mesh;
            _vs = _mesh.vertices;

            for (int i = 0; i < _text.textInfo.characterCount; i++)
            {
                int index = _text.textInfo.characterInfo[i].vertexIndex;
                Vector3 offset = Wobble((Time.time + i) * _speed);
                _vs[index] += offset;
                _vs[index + 1] += offset;
                _vs[index + 2] += offset;
                _vs[index + 3] += offset;
            }

            _mesh.vertices = _vs;
            _text.canvasRenderer.SetMesh(_mesh);
        }

        private Vector2 Wobble(float time)
        {
            return new Vector2(Mathf.Cos(time), Mathf.Sin(time)) * _strength;
        }
    }
}