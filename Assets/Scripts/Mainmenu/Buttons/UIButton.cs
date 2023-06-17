using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Satory23
{
    public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] protected bool Interactable;
        public bool InteractablePub => Interactable;
        private bool focuse = false;
        public bool Focuse => focuse;

        public UnityEvent OnClick;

        public event UnityAction<UIButton> PointerEnter;
        public event UnityAction<UIButton> PointerExit;
        public event UnityAction<UIButton> PointerClick;


        public virtual void SetFocuse()
        {
            if (Interactable == false) return;

            focuse = true;
        }

        public virtual void SetUnFocuse()
        {
            if (Interactable == false) return;

            focuse = false;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerEnter?.Invoke(this);
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerExit?.Invoke(this);
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (Interactable == false) return;

            PointerClick?.Invoke(this);
            OnClick?.Invoke();
        }
    }
}