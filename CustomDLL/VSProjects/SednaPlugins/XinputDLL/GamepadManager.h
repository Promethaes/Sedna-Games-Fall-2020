#pragma once
#include "InputEventSystem.h"
namespace SednaInput {

	class GamepadManager {
	public:
		GamepadManager();

		void UpdateGamepadList();
		bool GetEventValue(int playerIndex, int e);
		Vector2 GetLeftStickValue(int playerIndex) { return _gamepads[playerIndex].GetLeftStickValue(); }
		Vector2 GetRightStickValue(int playerIndex) { return _gamepads[playerIndex].GetRightStickValue(); }
	private:

		std::vector<InputEventSystem> _gamepads;

	};
}