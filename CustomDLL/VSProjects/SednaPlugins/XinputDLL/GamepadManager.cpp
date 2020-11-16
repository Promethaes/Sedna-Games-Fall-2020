#include "GamepadManager.h"
namespace SednaInput {
	GamepadManager::GamepadManager()
	{
		_gamepads.resize(4);
		UpdateGamepadList();
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
				InputEventSystem temp2{ temp };
				_gamepads.push_back(temp2);
			}
		}

		for (auto& p : _gamepads)
			p.PollEvents();

	}
	bool GamepadManager::GetEventValue(int playerIndex, int e)
	{
		if (playerIndex < 0 || playerIndex > _gamepads.size() - 1)
			return false;

		return _gamepads[playerIndex].GetEventValue(e);
	}
	Vector2 GamepadManager::GetLeftStickValue(int playerIndex)
	{
		if (playerIndex < 0 || playerIndex > _gamepads.size() - 1)
			return Vector2(0.0f, 0.0f);
		return _gamepads[playerIndex].GetLeftStickValue();
	}
	Vector2 GamepadManager::GetRightStickValue(int playerIndex)
	{
		if (playerIndex < 0 || playerIndex > _gamepads.size() - 1)
			return Vector2(0.0f, 0.0f);
		return _gamepads[playerIndex].GetRightStickValue();
	}
}