using System.Collections.Generic;

using UnityEngine;
using UnityEngine.U2D.Animation;

using BGS_Task.Gameplay.Common.Items.Handler;

namespace BGS_Task.Gameplay.Character.View
{
    public class CharacterView : MonoBehaviour
    {
        #region EXPOSED_FIELDS
        [SerializeField] private Animator animator = null;
        [SerializeField] private SpriteLibrary hair = null;
        [SerializeField] private SpriteLibrary top = null;
        [SerializeField] private SpriteLibrary bottom = null;
        [SerializeField] private AudioSource footsetpSound = null;
        #endregion

        #region CONSTANTS
        private const string xValue = "X";
        private const string yValue = "Y";
        #endregion

        #region PUBLIC_METHODS
        public void Configure(List<CharacterItemConfig> equipedParts)
        {
            bool foundHair = false;
            for (int i = 0; i < equipedParts.Count; i++)
            {
                switch (equipedParts[i].AvatarItemType)
                {
                    case AVATAR_ITEM_TYPE.HAIR:
                        foundHair = true;
                        hair.spriteLibraryAsset = equipedParts[i].SpriteLibrary;
                        break;
                    case AVATAR_ITEM_TYPE.TOP:
                        top.spriteLibraryAsset = equipedParts[i].SpriteLibrary;
                        break;
                    case AVATAR_ITEM_TYPE.BOTTOM:
                        bottom.spriteLibraryAsset = equipedParts[i].SpriteLibrary;
                        break;
                    default:
                        Debug.LogError("Invalid Avatar Item Type");
                        break;
                }
            }

            hair.gameObject.SetActive(foundHair);
        }

        public void DoMovementAnimation(Vector2 movement)
        {
            animator.SetFloat(xValue, movement.x);
            animator.SetFloat(yValue, movement.y);
        }

        #region ANIMATOR_METHODS
        public void PlayFootstepSound()
        {
            footsetpSound.Play();
        }
        #endregion
        #endregion
    }
}
