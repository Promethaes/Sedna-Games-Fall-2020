using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityScript : MonoBehaviour
{
    //progress bar
    Cutscene cutscene = null;
    public PlayerController playerController;
    public bool inCutscene;

    public TextMeshProUGUI topText;
    public TextMeshProUGUI middleText;
    public TextMeshProUGUI bottomText;

    public void enterQTE(PlayerController player)
    {
        Debug.Log("Entering QTE");
        if (inCutscene)
            return;
        player.inCutscene = true;
        playerController = player;
        inCutscene = true;
        switch (playerController.playerType)
        {
            case PlayerType.TURTLE:
                startTurtleQTE();
                break;
            case PlayerType.BISON:
                startBisonQTE();
                break;
            case PlayerType.POLAR_BEAR:
                startPolarQTE();
                break;
            case PlayerType.RATTLESNAKE:
                startSnakeQTE();
                break; 
        }
        Debug.Log("Resetting values");
        inCutscene = false;
        player.inCutscene = false;
        playerController = null;
        cutscene = null;
        topText.text= "";
        middleText.text= "";
        bottomText.text= "";
    }
    void startTurtleQTE()
    {
        Debug.Log("Starting Turtle QTE");
        //NOTE: Circular motion (10 circles)
        topText.text = "Bubble Shield";
        middleText.text = "";
        bottomText.text = "Move the joystick clockwise 10 times!";

        float _timer = 60.0f;
        int _counter = 0;
        float _progress = 0.0f;
        Vector2[] _inputs = {new Vector2(0,1),new Vector2(1,0),new Vector2(0,-1),new Vector2(-1,0)};
        while (_counter < 40 && _timer > 0.0f)
        {
            if (playerController.moveInput == _inputs[_counter % 4])
                _counter++;
            _progress = _counter/40;
            //NOTE: Replace with a bar later
            middleText.text = _progress.ToString();
            _timer-=Time.fixedTime;
            Debug.Log(_counter);
        }
        if (_counter >= 40)
            cutscene.startCutscene();
        Debug.Log("Ending Turtle QTE");
    }
    void startBisonQTE()
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
            _progress = Mathf.Max(_progress-(_drain*Time.fixedTime),0.0f);
            if (playerController.isDashing)
            {
                _progress+=_mashProgress;
                playerController.isDashing=false;
            }
            //NOTE: Replace with a bar later
            middleText.text = _progress.ToString();
            _timer-=Time.fixedTime;
        }
        if (_progress >= 1.0f)
            cutscene.startCutscene();
    }
    void startPolarQTE()
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
            _timer-=Time.fixedTime;
        }
        if (_counter >= 20)
            cutscene.startCutscene();
    }
    void startSnakeQTE()
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
                //NOTE: This is where you input wrong and it sends you back to the beginning
                _patternCounter = 0;

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

            _timer-=Time.fixedTime;
        }
        if (_counter >=3)
            cutscene.startCutscene();

    }
    public void setCutscene(Cutscene cs)
    {
        cutscene = cs;
    }

}
