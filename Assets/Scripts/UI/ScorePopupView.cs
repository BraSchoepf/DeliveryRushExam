using System;
using TMPro;
using UnityEngine;

namespace DeliveryRushExam.UI
{
    public class ScorePopupView : MonoBehaviour
    {
        [SerializeField] private TMP_Text messageText;
        [SerializeField] private float lifetime = 1.1f;
        [SerializeField] private float moveSpeed = 55f;

        //cambios realizados ----------------------------------
        private CanvasGroup canvasGroup;
        private float age;

        public Action<ScorePopupView> OnLifetimeEnded;
        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void Setup(string message)
        {
            age = 0f;
            messageText.text = message;

            if (canvasGroup != null)
                canvasGroup.alpha = 1f;
        }

        private void Update()
        {
            age += Time.deltaTime;
            transform.localPosition += Vector3.up * moveSpeed * Time.deltaTime;

            if (canvasGroup != null)
            {
                canvasGroup.alpha = 1f - age / lifetime;
            }

            if (age >= lifetime)
            {
                OnLifetimeEnded?.Invoke(this);
            }
        }
    }
}
