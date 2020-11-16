#pragma once
#include <Windows.h>
#include <Xinput.h>
#include <vector>
#include <math.h>
#pragma comment(lib,"Xinput.lib")


	struct Vector2 {
		float x;
		float y;
		Vector2() = default;
		Vector2(float X, float Y);

		float mag() {
			return sqrt((x * x) + (y * y));
		}
	};
namespace SednaInput {
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

	enum Trigger {
		LEFT_TRIGGER = 5,
		RIGHT_TRIGGER = 6
	};

	enum Joysticks {
		LEFT_STICK = 9,
		RIGHT_STICK = 10
	};

	class Gamepad {
	public:
		Gamepad() = default;
		Gamepad(XINPUT_STATE& state);

		bool isButtonPressed(Button button);

		float getLeftTriggerValue();
		bool isLeftTriggerHeld();

		float getRightTriggerValue();
		bool isRightTriggerHeld();

		Vector2 getLeftStickValue();
		Vector2 getRightStickValue();

	private:
		XINPUT_STATE _state;
	};

}
