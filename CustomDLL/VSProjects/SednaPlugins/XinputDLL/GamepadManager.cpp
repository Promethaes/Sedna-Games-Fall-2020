#include "GamepadManager.h"
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
				Gamepad temp{ state };
				InputEventSystem temp2 { temp };
				_gamepads.push_back(temp2);
			}
		}

		for (auto& p : _gamepads) 
			p.PollEvents();
		
	}
	bool GamepadManager::GetEventValue(int playerIndex, int e)
	{
		return _gamepads[playerIndex].GetEventValue(e);
	}
}