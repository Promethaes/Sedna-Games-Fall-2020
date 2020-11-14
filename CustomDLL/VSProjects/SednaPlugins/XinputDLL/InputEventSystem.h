#pragma once
#include <unordered_map>
#include "Gamepad.h"

namespace SednaInput {

	class InputEventSystem
	{
	public:
		InputEventSystem() = default;
		InputEventSystem(Gamepad& gamepad);

		void PollEvents();
		Vector2 GetLeftStickValue() { return _left; }
		Vector2 GetRightStickValue() { return _right; }

		bool GetEventValue(int e){return _events[e]; }
	private:
		void _PollButtonEvents();
		void _PollTriggerEvents();
		void _PollJoystickEvents();

		Gamepad _gamepad;

		Vector2 _left = {};
		Vector2 _right = {};

		std::unordered_map<int, bool> _events;
	};
}

