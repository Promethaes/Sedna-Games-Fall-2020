#include "Wrapper.h"

GamepadManager gamepadManager;

void UpdateGamepadList()
{
	gamepadManager.UpdateGamepadList();
}

bool GetEventValue(int playerIndex, int e)
{
	return gamepadManager.GetEventValue(playerIndex,e);
}

Vector2 GetLeftStickValue(int playerIndex)
{
	return gamepadManager.GetLeftStickValue(playerIndex);
}

Vector2 GetRightStickValue(int playerIndex)
{
	return gamepadManager.GetRightStickValue(playerIndex);
}


