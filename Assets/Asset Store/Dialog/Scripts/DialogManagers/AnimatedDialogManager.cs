using UnityEngine;

namespace Dialog {

    public class AnimatedDialogManager : Dialog.BaseDialogManager {
        public Animator Animator;

        public override void Awake() {
            base.Awake();
            DialogBox.SetActive(true);
        }

        protected override void ActivateDialogGameObject() {
            Animator.SetTrigger("Show");
            Animator.ResetTrigger("Hide");
        }

        protected override void DeactivateDialogGameObject() {
            Animator.SetTrigger("Hide");
            Animator.ResetTrigger("Show");
        }
    }

}