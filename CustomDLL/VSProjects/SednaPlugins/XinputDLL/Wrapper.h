#pragma once
#include "../UserMetricsLogger/PluginSettings.h"
#include "GamepadManager.h"

using namespace SednaInput;
#ifdef __cplusplus

extern "C" {
#endif

	PLUGIN_API void UpdateGamepadList();
	PLUGIN_API bool GetEventValue(int playerIndex, int e);
	PLUGIN_API float GetLeftStickValueXORY(int playerIndex,bool xOrY);
	PLUGIN_API float GetRightStickValueXORY(int playerIndex, bool xOrY);

#ifdef __cplusplus
}
#endif

