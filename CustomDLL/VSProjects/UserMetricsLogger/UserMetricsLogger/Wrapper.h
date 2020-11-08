#pragma once

#include "UserMetricsLogger.h"

#ifdef __cplusplus

extern "C" {
#endif

	PLUGIN_API void LogCheckpointTime(float time);
	PLUGIN_API void WriteUserMetricsToFile();
	PLUGIN_API void SetDefaultWritePath(String str);


#ifdef __cplusplus
}
#endif



