#include "Gamepad.h"


namespace SednaInput {
	

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
			x = _state.Gamepad.sThumbLX / 32768.f;
		else
			x = _state.Gamepad.sThumbLX / 32767.f;

		if(_state.Gamepad.sThumbLY < 0)
			y = _state.Gamepad.sThumbLY / 32768.f;
		else
			y = _state.Gamepad.sThumbLY / 32767.f;

		return Vector2(x, y);
	}
	Vector2 Gamepad::getRightStickValue()
	{
		if (_state.Gamepad.sThumbRX <  XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE &&
			_state.Gamepad.sThumbRX > -XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE &&
			_state.Gamepad.sThumbRY > -XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE &&
			_state.Gamepad.sThumbRY <  XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE
			)
			return Vector2(0.0f, 0.0f);

		float x = 0, y = 0;
		if (_state.Gamepad.sThumbRX < 0)
			x = _state.Gamepad.sThumbRX / 32768;
		else
			x = _state.Gamepad.sThumbRX / 32767;

		if (_state.Gamepad.sThumbRY < 0)
			y = _state.Gamepad.sThumbRY / 32768;
		else
			y = _state.Gamepad.sThumbRY / 32767;

		return Vector2(x, y);
	}
}
	Vector2::Vector2(float X, float Y)
	{
		x = X;
		y = Y;
	}
