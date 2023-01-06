using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tanks
{
    public enum GameMode
    {
        PvP,
        PvE,
    }

    public class Tanks_GameManager : FastSingleton<Tanks_GameManager>
    {
        
        [SerializeField] private Tanks_ViewController view;


        private int redScore, blueScore;
        private GameMode mode;
        private void Start()
        {
            redScore = blueScore = 10;
        }

        private void OnStartGame()
        {
            view.SetGameMode(mode);
        }
        public void OnClickExitButton()
        {

        }

        public void OnClickBackButton()
        {
            view.SetScreenActive((int)ScreenState.Intro);
        }

        public void OnClickPvPButton()
        {
            mode = GameMode.PvP;
            OnStartGame();
        }

        public void OnClickPvEButotn()
        {
            mode = GameMode.PvE;
            view.SetScreenActive((int)ScreenState.Difficulty);
        }

        public void OnClickPlayButton()
        {
            view.SetScreenActive((int)ScreenState.Gameplay);
            OnStartGame();
        }

        public void DecreaseScore(string color)
        {
            Debug.Log("Decrease");
            if (color.Equals(Cache.TAG_REDTANK))
            {
                redScore--;
                view.SetRedScore(redScore);
            }
            else if (color.Equals(Cache.TAG_BLUETANK))
            {
                blueScore--;
                view.SetBlueScore(blueScore);
            }
            Invoke(nameof(CheckGameOver), 1f);
        }

        private void CheckGameOver()
        {
            if (redScore <= 0)
            {
                view.SetScreenActive((int)ScreenState.BlueWin);
                Time.timeScale = 0;
            }

            if (blueScore <= 0)
            {
                view.SetScreenActive((int)ScreenState.RedWin);
                Time.timeScale = 0;
            }

        }

        public void OnChangeValue()
        {
            view.SetRotateRoller();
        }

        public void OnShakeCamera()
        {
            view.ShakeCamera();
        }
    }
}

