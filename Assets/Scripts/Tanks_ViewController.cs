using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Tanks
{
    public enum ScreenState
    {
        Intro,
        Difficulty,
        Gameplay,
        RedWin,
        BlueWin,
    }
    public class Tanks_ViewController : MonoBehaviour
    {
        [SerializeField] private Tanks player1Tank, player2Tank, botTank;
        [SerializeField] private Transform tank1Pos, tank2Pos;
        [SerializeField] private Joystick moveJtP2, aimJtP2;
        [SerializeField] private TMP_Text redScoreText, blueScoreText;
        [SerializeField] private Slider difficultySlider;
        [SerializeField] private GameObject[] ScreenList;
        [SerializeField] private RectTransform textRoller;
        [SerializeField] private Gradient gradient;
        [SerializeField] private Image fillArea, playButton, faceImage;
        [SerializeField] private Transform mainCam;

        private bool isShakingCamera = false;
        private float shakeTime = 0;
        private Vector3 defaultCamPos;
        private GameObject currentScreen;
        private void Awake()
        {
            
        }

        private void Start()
        {
            SetScreenActive((int)ScreenState.Intro);
            difficultySlider.minValue = -90;
            difficultySlider.maxValue = 90;
            difficultySlider.value = 0;
            fillArea.color = gradient.Evaluate(0.5f);
            playButton.color = gradient.Evaluate(0.5f);
            faceImage.color = gradient.Evaluate(0.5f);
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (difficultySlider.value < -45)
                {
                    difficultySlider.value = -90;
                }
                else if (difficultySlider.value < 45)
                {
                    difficultySlider.value = 0;
                }
                else
                {
                    difficultySlider.value = 90;
                }
            }

            if (isShakingCamera)
            {
                if (shakeTime < 0.2f)
                {
                    shakeTime += Time.deltaTime;
                    Vector3 shakePos = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
                    mainCam.position += shakePos * 0.1f;
                }
                else
                {
                    mainCam.position = defaultCamPos;
                    shakeTime = 0;
                    isShakingCamera = false;
                }
            }
        }
        public void SetActiveGameObject(GameObject gobj, bool isActive)
        {
            gobj.SetActive(isActive);
        }

        public void SetGameMode(GameMode mode)
        {
            
            SetActiveGameObject(player1Tank.gameObject, true);
            player1Tank.SetPosition(tank1Pos.position);

            if (mode == GameMode.PvE)
            {
                SetActiveGameObject(player2Tank.gameObject, false);
                SetActiveGameObject(botTank.gameObject, true);
                botTank.SetPosition(tank2Pos.position);

                SetActiveGameObject(moveJtP2.gameObject, false);
                SetActiveGameObject(aimJtP2.gameObject, false);
            }
            else if (mode == GameMode.PvP)
            {
                SetScreenActive((int)ScreenState.Gameplay);
                SetActiveGameObject(player2Tank.gameObject, true);
                SetActiveGameObject(botTank.gameObject, false);
                player2Tank.SetPosition(tank2Pos.position);

                SetActiveGameObject(moveJtP2.gameObject, true);
                SetActiveGameObject(aimJtP2.gameObject, true);
            }
        }

        public void SetRedScore(int score)
        {
            redScoreText.text = score.ToString();
        }

        public void SetBlueScore(int score)
        {
            blueScoreText.text = score.ToString();
        }

        public void SetScreenActive(int i)
        {
            if (currentScreen != null)
            {
                currentScreen.SetActive(false);
            }
            currentScreen = ScreenList[i];
            currentScreen.SetActive(true);
        }

        public void SetRotateRoller()
        {
            textRoller.rotation = Quaternion.Euler(difficultySlider.value, 0, 0);
            fillArea.color = gradient.Evaluate((difficultySlider.value + 90) / (difficultySlider.maxValue + 90));
            playButton.color = gradient.Evaluate((difficultySlider.value + 90) / (difficultySlider.maxValue + 90));
            faceImage.color = gradient.Evaluate((difficultySlider.value + 90) / (difficultySlider.maxValue + 90));
        }

        public void ShakeCamera()
        {
            isShakingCamera = true;
            defaultCamPos = mainCam.position;
        }

        
    }
}

