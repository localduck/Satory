using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Satory23
{
    [RequireComponent(typeof(AudioSource))]
    public class UIButtonSound : MonoBehaviour
    {
        [SerializeField] private AudioClip click;
        [SerializeField] private AudioClip select;

        private new AudioSource audio;

        private UIButton[] uIButton;

        private void Start()
        {
            audio = GetComponent<AudioSource>();

            uIButton = GetComponentsInChildren<UIButton>(true);

            for (int i = 0; i < uIButton.Length; i++)
            {
                uIButton[i].PointerEnter += OnPointerEnter;
                uIButton[i].PointerClick += OnPointerClicked;
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < uIButton.Length; i++)
            {
                uIButton[i].PointerEnter -= OnPointerEnter;
                uIButton[i].PointerClick -= OnPointerClicked;
            }
        }

        private void OnPointerClicked(UIButton arg0)
        {
            if (arg0.InteractablePub == false) return;
            audio.PlayOneShot(click);
        }

        private void OnPointerEnter(UIButton arg0)
        {
            if (arg0.InteractablePub == false) return;
            audio.PlayOneShot(select);
        }
    }
}
