#include "Wrapper.h"

UserMetricsLogger logger;

void LogCheckpointTime(float time)
{
	logger.LogCheckpointTime(time);
}

void LogDeath(Death death)
{
	logger.LogDeath(death);
}

void WriteUserMetricsToFile()
{
	logger.WriteUserMetricsToFile();
}

void SetDefaultWritePath(String str)
{
	logger.SetDefaultWritePath(str);
}
