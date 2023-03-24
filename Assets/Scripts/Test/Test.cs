using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class Test : MonoBehaviour
    {
        private bool inProgress;
        private DateTime endTime;
        private DateTime startTime;

        [Header("Production Time")]
        public int days;
        public int hours;
        public int minutes;
        public int seconds;

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI textStartTime;
        [SerializeField]
        private TextMeshProUGUI textEndTime;
        [SerializeField]
        private Slider sliderTimeLeft;
        [SerializeField]
        private TextMeshProUGUI textTimeLeft;
        [SerializeField]
        private Button buttonStart;

        #region MAIN

        private void Start()
        {
            buttonStart.onClick.AddListener(StartTimer);
        }

        #endregion

        #region TIMER EVENT

        private void StartTimer()
        {
            startTime = DateTime.Now;
            endTime = startTime.Add(new TimeSpan(days, hours, minutes, seconds));
            inProgress = true;

            StartCoroutine(DoInBackground());

            BindUIWindow();
        }

        private IEnumerator DoInBackground()
        {
            var currentTime = DateTime.Now;
            var secondsToFinished = (endTime - currentTime).TotalSeconds;
            yield return new WaitForSeconds(Convert.ToSingle(secondsToFinished));
            inProgress = false;
            Debug.Log("Finished");
        }

        #endregion

        #region UI METHODS

        private void BindUIWindow()
        {
            if (inProgress)
            {
                textStartTime.text = "Start Time: \n" + startTime;
                textEndTime.text = "End Time: \n" + endTime;

                StartCoroutine(RenderProgressBar());
            }
            else
            {
                textStartTime.text = "Start Time: \n";
                textEndTime.text = "End Time: \n";
            }
        }

        private IEnumerator RenderProgressBar()
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

        #endregion
    }
}