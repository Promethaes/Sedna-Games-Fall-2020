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

float GetLeftStickValueXORY(int playerIndex, bool xOrY) {
	if (xOrY)
		return gamepadManager.GetLeftStickValue(playerIndex).y;
	else
		return gamepadManager.GetLeftStickValue(playerIndex).x;

}
float GetRightStickValueXORY(int playerIndex, bool xOrY) {
	if (xOrY)
		return gamepadManager.GetRightStickValue(playerIndex).y;
	else
		return gamepadManager.GetRightStickValue(playerIndex).x;
}
