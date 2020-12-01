#pragma once
#include "InputEventSystem.h"
#include "../UserMetricsLogger/PluginSettings.h"
namespace SednaInput {

	class GamepadManager {
	public:
		GamepadManager();

		void UpdateGamepadList();
		bool GetEventValue(int playerIndex, int e);
		Vector2 GetLeftStickValue(int playerIndex);
		Vector2 GetRightStickValue(int playerIndex);
		int GetNumGamepads();
	private:

		std::vector<InputEventSystem> _gamepads;

	};
}