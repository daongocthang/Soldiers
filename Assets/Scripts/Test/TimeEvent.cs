using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Test
{
    public abstract class TimeEvent : MonoBehaviour
    {
        protected Coroutine doInBackground;
        protected Coroutine postRender;

        protected DateTime startTime;
        protected DateTime endTime;

        public bool inProgress { get; private set; }

        [SerializeField]
        private Slider sliderTimeLeft;
        [SerializeField]
        private TextMeshProUGUI textTimeLeft;


        public void StartCountdown(TimeSpan timeSpan)
        {
            Assert.IsFalse(inProgress);

            startTime = DateTime.Now;
            endTime = startTime.Add(timeSpan);
            inProgress = true;
            OnBindGUI();

            doInBackground = StartCoroutine(DoInBackground());
            postRender = StartCoroutine(PostRender());
        }

        public void StopCountdown()
        {
            StopCoroutine(postRender);
            StopCoroutine(doInBackground);

            OnUnbindGUI();
            inProgress = false;
        }

        protected abstract void OnBindGUI();

        protected abstract void OnUnbindGUI();

        protected abstract void OnCompleted();


        private IEnumerator DoInBackground()
        {
            var secondsToFinished = (endTime - DateTime.Now).TotalSeconds;
            yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));
            inProgress = false;
            
            OnCompleted();
            Debug.Log("Finished");
        }

        private IEnumerator PostRender()
        {
            var timeLeft = endTime - DateTime.Now;
            var totalSecondsLeft = timeLeft.TotalSeconds;
            var totalSeconds = (endTime - startTime).TotalSeconds;

            while (sliderTimeLeft.IsActive())
            {
                var result = "";
                sliderTimeLeft.value = 1 - Convert.ToSingle((endTime - DateTime.Now).TotalSeconds / totalSeconds);
                if (totalSecondsLeft > 1)
                {
                    if (timeLeft.Days > 0)
                    {
                        result += timeLeft.Days + "d ";
                        result += timeLeft.Hours + "h";
                        if (sliderTimeLeft.value > 0.01)
                            yield return new WaitForSeconds(timeLeft.Minutes * 60);
                    }
                    else if (timeLeft.Hours > 0)
                    {
                        result += timeLeft.Hours + "h ";
                        result += timeLeft.Minutes + "m";
                        if (sliderTimeLeft.value > 0.01)
                            yield return new WaitForSeconds(timeLeft.Seconds);
                    }
                    else if (timeLeft.Minutes > 0)
                    {
                        var ts = TimeSpan.FromSeconds(totalSecondsLeft);
                        result += ts.Minutes + "m ";
                        result += ts.Seconds + "s";
                    }
                    else
                    {
                        result += Mathf.FloorToInt((float) totalSecondsLeft) + "s";
                    }

                    textTimeLeft.text = result;
                    totalSecondsLeft -= Time.deltaTime;
                    yield return null;
                }
                else
                {
                    textTimeLeft.text = "Finished";
                    inProgress = false;
                    break;
                }
            }

            yield return null;
        }
    }
}