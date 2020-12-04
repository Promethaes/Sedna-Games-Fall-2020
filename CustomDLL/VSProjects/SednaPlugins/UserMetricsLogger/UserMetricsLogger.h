#pragma once
#include "PluginSettings.h"

#include "Structs.h"

#include "CheckpointLogger.h"
#include "DeathLogger.h"

#define NUM_LOGGERS 2
class PLUGIN_API UserMetricsLogger
{
public:

	void LogCheckpointTime(float time) { cLogger.addTime(time); }

	void LogDeath(Death death);//so much death

	void WriteUserMetricsToFile();
	void ClearUserMetricsLogger();
	void ClearUserMetricsLoggerFileOnly();

	void SetDefaultWritePath(String str) {defaultPath = str.data;}
private:

	std::vector<MetricLog> logData;
	std::string defaultPath = "LatestUserMetrics.txt";

	CheckpointLogger cLogger;
	DeathLogger dLogger;
};

