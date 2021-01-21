using UnityEngine;
using DG.Tweening;

namespace Mahjong
{
    public sealed class TileView : MonoBehaviour, ITileView
    {
        private static int FALL_DIR = 1;

        private readonly Color COLOR_SELECTED = Color.yellow;
        private readonly Color COLOR_LOCKED = new Color(.5f, .5f, .5f, 1);

        public SpriteRenderer TileSprite;
        [SerializeField] private GameObject _selection;
        [SerializeField] private Rigidbody2D _rigidbody;

        private TileState _currentState = TileState.Default;

        void ITileView.SetState(TileState state)
        {
            if (state == _currentState || _currentState == TileState.Released)
                return;

            _currentState = state;

            switch (state)
            {
                case TileState.Default:
                    AnimateColor(Color.white);
                    break;
                case TileState.Locked:
                    AnimateColor(COLOR_LOCKED);
                    break;
                case TileState.Mismatched:
                    DOTween.Kill(TileSprite, true);
                    TileSprite.color = Color.red;
                    AnimateColor(Color.white);
                    TileSprite.transform.DOPunchPosition(Vector2.right * .1f, .5f, elasticity: 0);
                    break;
                case TileState.Selected:
                    AnimateColor(COLOR_SELECTED);
                    break;
                case TileState.Released:
                    Release();
                    break;
            }
        }

        private void Release()
        {
            DOTween.Kill(TileSprite, true);
            TileSprite.color = Color.white;

            _rigidbody.isKinematic = false;
            _rigidbody.angularVelocity = 200 * FALL_DIR;
            _rigidbody.AddForce(Vector2.left * FALL_DIR, ForceMode2D.Impulse);

            TileSprite.sortingOrder = 999;

            Destroy(gameObject, 5f);

            FALL_DIR = -FALL_DIR;
        }

        private void AnimateColor(Color targetColor)
        {
            TileSprite.DOColor(targetColor, .5f);
        }
    }
}
