#pragma once
#include "../UserMetricsLogger/PluginSettings.h"
#include "GamepadManager.h"

using namespace SednaInput;
#ifdef __cplusplus

extern "C" {
#endif

	PLUGIN_API void UpdateGamepadList();
	PLUGIN_API bool GetEventValue(int playerIndex, int e);
	PLUGIN_API Vector2 GetLeftStickValue(int playerIndex);
	PLUGIN_API Vector2 GetRightStickValue(int playerIndex);

#ifdef __cplusplus
}
#endif

