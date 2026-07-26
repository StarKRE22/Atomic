using System;
using DG.Tweening;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif
using UnityEngine;

namespace Game.UI
{
    public enum CellHighlightType
    {
        None,
        Move,
        Attack
    }
    
    public sealed class GameBoardCellView : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private float _highlightHeight = -0.2f;
        [SerializeField] private float _duration = 0.3f;

        [Header("Colors")]
        [SerializeField] private Color _moveColor = new(0.6f, 1f, 0.6f, 1f);
        [SerializeField] private Color _attackColor = new(1f, 0.4f, 0.4f, 1f);
        [SerializeField] private float _blend = 0.3f;

        private MaterialPropertyBlock _propertyBlock;
        private Color _baseColor;

        private static readonly int ColorId = Shader.PropertyToID("_Color");

        private void Awake()
        {
            _propertyBlock = new MaterialPropertyBlock();
            _baseColor = _renderer.sharedMaterial.GetColor(ColorId);
        }

        public void SetMaterial(Material material)
        {
            _renderer.sharedMaterial = material;
            _baseColor = material.GetColor(ColorId);
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void SetHighlight(CellHighlightType type)
        {
            switch (type)
            {
                case CellHighlightType.None:
                    transform.DOLocalMoveY(0f, _duration);
                    // SetColor(_baseColor);
                    break;

                case CellHighlightType.Move:
                    transform.DOLocalMoveY(_highlightHeight, _duration);
                    // SetColor(Color.Lerp(_baseColor, _moveColor, _blend));
                    break;

                case CellHighlightType.Attack:
                    transform.DOLocalMoveY(_highlightHeight, _duration);
                    // SetColor(Color.Lerp(_baseColor, _attackColor, _blend));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        private void SetColor(Color color)
        {
            _renderer.GetPropertyBlock(_propertyBlock);
            _propertyBlock.SetColor(ColorId, color);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}