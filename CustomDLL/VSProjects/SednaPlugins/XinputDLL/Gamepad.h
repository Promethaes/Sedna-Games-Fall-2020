#pragma once
#include <Windows.h>
#include <Xinput.h>
#include <vector>
#pragma comment(lib,"Xinput.lib")


namespace SednaInput {
	struct Vector2 {
		float x;
		float y;
		Vector2();
		Vector2(float X, float Y);
	};
	enum Button {
		DPAD_UP = XINPUT_GAMEPAD_DPAD_UP,
		DPAD_DOWN = XINPUT_GAMEPAD_DPAD_DOWN,
		DPAD_LEFT = XINPUT_GAMEPAD_DPAD_LEFT,
		DPAD_RIGHT = XINPUT_GAMEPAD_DPAD_RIGHT,

		START = XINPUT_GAMEPAD_START,
		BACK = XINPUT_GAMEPAD_BACK,

		THUMB_LEFT = XINPUT_GAMEPAD_LEFT_THUMB,
		THUMB_RIGHT = XINPUT_GAMEPAD_RIGHT_THUMB,

		SHOULDER_LEFT = XINPUT_GAMEPAD_LEFT_SHOULDER,
		SHOULDER_RIGHT = XINPUT_GAMEPAD_RIGHT_SHOULDER,

		A = XINPUT_GAMEPAD_A,
		B = XINPUT_GAMEPAD_B,
		X = XINPUT_GAMEPAD_X,
		Y = XINPUT_GAMEPAD_Y
	};

	class Gamepad {
	public:
		Gamepad(XINPUT_STATE& state);

		bool isButtonPressed(Button button);

		float getLeftTriggerValue();
		bool isLeftTriggerHeld();

		float getRightTriggerValue();
		bool isRightTriggerHeld();

		Vector2 getLeftStickValue();

	private:
		XINPUT_STATE _state;
	};

	class GamepadManager {
	public:
		GamepadManager();
	
		void UpdateGamepadList();
		size_t getNumGamepads() { return _gamepads.size(); }
	private:

		std::vector<Gamepad> _gamepads;

	};
}
