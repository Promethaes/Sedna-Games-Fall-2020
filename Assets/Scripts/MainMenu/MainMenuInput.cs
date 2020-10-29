using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenuInput : MonoBehaviour
{
    Vector2 _moveInput = new Vector2();
    float _selectionCooldown = 0.25f;
    float _maxSelectionCooldown = 0.25f;
    bool _confirm = false;

    int _currentSelected = 0;
    public List<Image> menuOptions;
    Image _currentSelectedImage;


    // Start is called before the first frame update
    void Start()
    {
        _ChangeSelection();
    }

    // Update is called once per frame
    void Update()
    {

        _selectionCooldown -= Time.deltaTime;
        if (_selectionCooldown <= 0.0f)
            _selectionCooldown = 0.0f;


        if (_moveInput.y >= 0.5f && _selectionCooldown == 0.0f)
            _ScrollSelectionUp();
        else if (_moveInput.y <= -0.5f && _selectionCooldown == 0.0f)
            _ScrollSelectionDown();

        if (_confirm && _selectionCooldown == 0.0f)
            _ConfirmSelection();

    }

    void _ConfirmSelection()
    {
        _currentSelectedImage.color = new Color(0.0f, 0.0f, 1.0f, 1.0f);
        _currentSelectedImage.gameObject.GetComponent<MenuButtonFunctionality>().execute();
    }

    void _ChangeSelection()
    {
        _currentSelectedImage = menuOptions[_currentSelected];
        _currentSelectedImage.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
        foreach (var image in menuOptions)
        {
            if (image == _currentSelectedImage)
                continue;
            image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        _selectionCooldown = _maxSelectionCooldown;
    }

    void _ScrollSelectionUp()
    {
        _currentSelected = (_currentSelected + 1) % menuOptions.Count;
        _ChangeSelection();
    }
    void _ScrollSelectionDown()
    {
        _currentSelected = (_currentSelected - 1 + menuOptions.Count) % menuOptions.Count;
        _ChangeSelection();
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }
    public void OnConfirm(InputAction.CallbackContext ctx)
    {
        var temp = ctx.ReadValue<float>();

        if (temp >= 0.5f)
            _confirm = true;
        else
            _confirm = false;
    }

}
