using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityScript : MonoBehaviour
{
    //progress bar
    public Image[] _progressBar;
    float _barSize=1000.0f;
    float _barHeight=50.0f;
    public GameObject[] effects;
    Cutscene cutscene = null;
    public PlayerController playerController;
    public bool inCutscene;

    public TextMeshProUGUI topText;
    public TextMeshProUGUI middleText;
    public TextMeshProUGUI bottomText;

    public IEnumerator enterQTE(PlayerController player)
    {
        Debug.Log("Entering QTE");
        if (inCutscene || cutscene.cutsceneComplete || player.remotePlayer)
            yield return null;
        player.inCutscene = true;
        playerController = player;
        effects = player.GetComponentInChildren<AbilityEffectManager>().effects;
        inCutscene = true;
        for (int i=0;i<_progressBar.Length;i++)
            _progressBar[i].gameObject.SetActive(true);
        _progressBar[0].rectTransform.sizeDelta = new Vector2(_barSize, _barHeight);
        _progressBar[1].rectTransform.sizeDelta = new Vector2(0, _barHeight);
        switch (playerController.playerType)
        {
            case PlayerType.TURTLE:
                StartCoroutine(startTurtleQTE());
                break;
            case PlayerType.BISON:
                StartCoroutine(startBisonQTE());
                break;
            case PlayerType.POLAR_BEAR:
                StartCoroutine(startPolarQTE());
                break;
            case PlayerType.RATTLESNAKE:
                StartCoroutine(startSnakeQTE());
                break; 
        }
        yield return null;
    }
    IEnumerator resetValues()
    {
        Debug.Log("Resetting values");
        inCutscene = false;
        playerController.inCutscene = false;
        playerController = null;
        cutscene = null;
        topText.text= "";
        middleText.text= "";
        bottomText.text= "";
        for (int i=0;i<_progressBar.Length;i++)
        _progressBar[i].gameObject.SetActive(false);
        _progressBar[0].rectTransform.sizeDelta = new Vector2(_barSize, _barHeight);
        _progressBar[1].rectTransform.sizeDelta = new Vector2(0, _barHeight);
        yield return null;
    }
    bool circleCheck(int num)
    {
        Vector2[] _inputs = {new Vector2(0,1),new Vector2(1,0),new Vector2(0,-1),new Vector2(-1,0)};
        var move = playerController.moveInput;
        switch (num)
        {
            case 0:
                return (move.x < 0.3f && move.x > -0.3f && move.y > 0.7f);
            case 1:
                return (move.x > 0.7f && move.y < 0.3f && move.y > -0.3f);
            case 2:
                return (move.x < 0.3f && move.x > -0.7f && move.y < -0.7f);
            case 3:
                return (move.x < -0.7f && move.y < 0.3f && move.y > -0.3f);
            default:
                return false;
        }

    }
    IEnumerator startTurtleQTE()
    {
        //NOTE: Circular motion (10 circles)
        topText.text = "Bubble Shield";
        middleText.text = "";
        bottomText.text = "Move the joystick clockwise 10 times!";
        //NOTE: Add this to the other three once their effects are in

        float _timer = 60.0f;
        int _counter = 0;
        float _progress = 0.0f;
        while (_counter < 40 && _timer > 0.0f)
        {
            if (circleCheck(_counter % 4))
                _counter++;
            _progress = (float)_counter/40.0f;
            _progressBar[1].rectTransform.sizeDelta = new Vector2(_progress*_barSize, _barHeight);
            yield return new WaitForFixedUpdate();
            _timer-=Time.fixedDeltaTime;
        }
        if (_counter >= 40)
        {
            effects[0].SetActive(true);
            cutscene.effect = effects[0];
            cutscene.startCutscene();
        }
        StartCoroutine(resetValues());
        yield return null;
    }
    IEnumerator startBisonQTE()
    {
        //NOTE: Mash dash (progressive with drain)
        topText.text = "Ram";
        middleText.text = "";
        bottomText.text = "Mash B repeatedly!";

        float _timer=60.0f;
        float _progress=0.0f;
        float _drain=0.1f;
        float _mashProgress=0.025f;

        while (_timer > 0.0f && _progress < 1.0f)
        {
            _progress = Mathf.Max(_progress-(_drain*Time.deltaTime),0.0f);
            if (playerController.isDashing)
            {
                _progress+=_mashProgress;
                playerController.isDashing=false;
            }
            //NOTE: Replace with a bar later
            _progressBar[1].rectTransform.sizeDelta = new Vector2(_progress*_barSize, _barHeight);
            yield return new WaitForFixedUpdate();
            _timer-=Time.fixedDeltaTime;
        }
        if (_progress >= 1.0f)
            cutscene.startCutscene();
        StartCoroutine(resetValues());
    }
    IEnumerator startPolarQTE()
    {
        //NOTE: Four button rush (20 inputs randomized between ABXY)
        topText.text = "Ice Boxing";
        middleText.text = "";
        bottomText.text = "Input the correct button 20 times!";

        float _timer = 60.0f;
        int[] _inputs = new int[20];
        int _counter = 0;

        for (int i=0;i<20;i++)
            _inputs[i] = Mathf.FloorToInt(Random.Range(0.0f,4.0f));

        while (_timer > 0.0f && _counter < 20)
        {
            switch (_inputs[_counter])
            {
                case 0:
                    middleText.text = "A";
                    if (playerController.isJumping)
                        _counter++;
                    break;
                case 1:
                    middleText.text = "B";
                    if (playerController.isDashing)
                        _counter++;
                    break;
                case 2:
                    middleText.text = "Y";
                    if (playerController.toggle)
                        _counter++;
                    break;
                case 3:
                    middleText.text = "X";
                    if (playerController.attack)
                        _counter++;
                    break;
            }
            _progressBar[1].rectTransform.sizeDelta = new Vector2(_counter/20*_barSize, _barHeight);
            yield return new WaitForFixedUpdate();
            _timer-=Time.fixedDeltaTime;
        }
        if (_counter >= 20)
            cutscene.startCutscene();
        StartCoroutine(resetValues());
    }
    IEnumerator startSnakeQTE()
    {
        //NOTE: Four button Pattern (3 times, 5 buttons each)
        //NOTE: Maybe Simon Says if this is too basic
        topText.text = "Rattle";
        middleText.text = "";
        bottomText.text = "Copy the three patterns!";
        
        float _timer = 60.0f;

        string[] _input = {"A", "B", "Y", "X"};
        int[] _patternA = new int[5];
        int[] _patternB = new int[5];
        int[] _patternC = new int[5];
        int[] _pattern = _patternA;

        int _patternCounter=0;
        int _counter=0;

        for (int i=0;i<5;i++)
        {
            _patternA[i] = Mathf.FloorToInt(Random.Range(0.0f,4.0f));
            _patternB[i] = Mathf.FloorToInt(Random.Range(0.0f,4.0f));
            _patternC[i] = Mathf.FloorToInt(Random.Range(0.0f,4.0f));
        }

        while (_timer >0.0f && _counter < 3)
        {
            //Pattern text
            middleText.text = "";
            for (int i=0;i<_pattern.Length;i++)
                middleText.text += _input[_pattern[i]];

            //Pattern logic checking
            if (playerController.isJumping && _pattern[_patternCounter] == 0)
                _patternCounter++;
            else if (playerController.isDashing && _pattern[_patternCounter] == 1)
                _patternCounter++;
            else if (playerController.toggle && _pattern[_patternCounter] == 2)
                _patternCounter++;
            else if (playerController.attack && _pattern[_patternCounter] == 3)
                _patternCounter++;
            else if (playerController.isJumping || playerController.isDashing || playerController.toggle || playerController.attack)
                _patternCounter = 0;

            playerController.isJumping = false;
            playerController.isDashing = false;
            playerController.toggle = false;
            playerController.attack = false;

            _progressBar[1].rectTransform.sizeDelta = new Vector2((_counter/3+_patternCounter/5)/2*_barSize, _barHeight);

            //Pattern completion checking
            if (_patternCounter >= 5)
            {
                _patternCounter = 0;
                _counter++;

                switch (_counter)
                {
                    case 0:
                        _pattern = _patternA;
                        break;
                    case 1:
                        _pattern = _patternB;
                        break;
                    case 2:
                        _pattern = _patternC;
                        break;
                }
            }
            yield return new WaitForFixedUpdate();
            _timer-=Time.fixedDeltaTime;
        }
        if (_counter >=3)
            cutscene.startCutscene();
        StartCoroutine(resetValues());

    }
    public void setCutscene(Cutscene cs)
    {
        cutscene = cs;
    }

}
