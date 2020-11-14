#include "Gamepad.h"


namespace SednaInput {
	GamepadManager::GamepadManager()
	{
		UpdateGamepadList();
		_gamepads.resize(4);
		_gamepads.reserve(4);
	}
	void GamepadManager::UpdateGamepadList()
	{
		_gamepads.clear();

		DWORD result;
		for (DWORD i = 0; i < 4; i++) {
			XINPUT_STATE state;
			ZeroMemory(&state, sizeof(XINPUT_STATE));

			result = XInputGetState(i, &state);

			if (result == ERROR_SUCCESS) {
				_gamepads.push_back(Gamepad(state));
			}
		}
	}

	SednaInput::Gamepad::Gamepad(XINPUT_STATE& state)
		:_state(state)
	{
	}

	bool SednaInput::Gamepad::isButtonPressed(Button button)
	{
		return _state.Gamepad.wButtons & button;
	}
	float Gamepad::getLeftTriggerValue()
	{
		return _state.Gamepad.bLeftTrigger / 255.0f;
	}
	bool Gamepad::isLeftTriggerHeld()
	{
		return getLeftTriggerValue() > 0.5f ? true : false;
	}
	float Gamepad::getRightTriggerValue()
	{
		return _state.Gamepad.bRightTrigger / 255.0f;
	}
	bool Gamepad::isRightTriggerHeld()
	{
		return getRightTriggerValue() > 0.5f ? true : false;
	}
	Vector2 Gamepad::getLeftStickValue()
	{
		if (_state.Gamepad.sThumbLX < XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE &&
			_state.Gamepad.sThumbLX > -XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE &&
			_state.Gamepad.sThumbLY > -XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE &&
			_state.Gamepad.sThumbLY < XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE
			)
			return Vector2(0.0f, 0.0f);

		float x = 0, y = 0;
		if (_state.Gamepad.sThumbLX < 0)
			x = _state.Gamepad.sThumbLX / 32768;
		else
			x = _state.Gamepad.sThumbLX / 32767;

		if(_state.Gamepad.sThumbLY < 0)
			y = _state.Gamepad.sThumbLY / 32768;
		else
			y = _state.Gamepad.sThumbLY / 32767;

		return Vector2(x, y);
	}
}
