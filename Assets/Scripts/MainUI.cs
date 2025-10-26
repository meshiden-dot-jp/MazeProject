using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MazeProject
{
    public class MainUI : MonoBehaviour
    {
        public TextMeshProUGUI TextTime;
        public TextMeshProUGUI TextMessage;
        public float LimitedTime = 30f;

        private float startTime;
        public int remainingTime;
        public Player Player;
        public bool isFinished = false;
 
        void Start()
        {
            startTime = Time.time;
        }

        void Update()
        {
            if (isFinished)
            {
                // クリア or ゲームオーバー後は表示ロック
                return;
            }
            float remaining = LimitedTime - (Time.time - startTime);

            if (remaining > 0)
            {
            // TODO: "ゴール時にタイマーを停止、クリアを表示";
                TextTime.text = $"Time : {Mathf.FloorToInt(remaining):00}";
                remainingTime = Mathf.FloorToInt(remaining);
                if (Player != null && Player.isGoal)
                {
                    TextTime.text = $"Time : {remainingTime:00}";
                    TextMessage.text = "Game Clear";
                    isFinished = true;
                    return;
                }
            }
            else if (!Player.isGoal)
            {
                TextTime.text = "Time : 00";
                TextMessage.text = "Game Over";
                isFinished = true;
                return;
            }
        }

        public float GetRemainingTime()
        {
            return Mathf.Max(0, LimitedTime - (Time.time - startTime));
        }
    }
}
