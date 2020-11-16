#include "InputEventSystem.h"
namespace SednaInput {


	SednaInput::InputEventSystem::InputEventSystem(Gamepad& gamepad)
		:_gamepad(gamepad),_left(0.0f,0.0f),_right(0.0f,0.0f)
	{
	}
	void InputEventSystem::PollEvents()
	{
		_PollButtonEvents();
		_PollTriggerEvents();
		_PollJoystickEvents();
	}
	void InputEventSystem::_PollButtonEvents()
	{
		//so the whole idea is to loop through the enum, except the last 4 members
		//don't follow the doubling pattern how you'd think, so thats why theres
		//an if statement there
		for (int i = 1; i < Button::Y; i += i) {
			Button b = static_cast<Button>(i);

			_events[i] = _gamepad.isButtonPressed(static_cast<Button>(b));

			if (i == 512)
				i = 2048;
		}
	}
	void InputEventSystem::_PollTriggerEvents()
	{
		_events[Trigger::LEFT_TRIGGER] = _gamepad.isLeftTriggerHeld();
		_events[Trigger::RIGHT_TRIGGER] = _gamepad.isRightTriggerHeld();
	}
	void InputEventSystem::_PollJoystickEvents()
	{
		_events[Joysticks::LEFT_STICK] = (_left = _gamepad.getLeftStickValue()).mag() > 0.0 ? true : false;
		_events[Joysticks::RIGHT_STICK] = (_right = _gamepad.getRightStickValue()).mag() > 0.0 ? true : false;
	}
}
