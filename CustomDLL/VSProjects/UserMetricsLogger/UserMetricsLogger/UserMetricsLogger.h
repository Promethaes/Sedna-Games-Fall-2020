#pragma once
#include "PluginSettings.h"

#include "CheckpointLogger.h"

struct String {
	const char* data;
};
struct MetricLog {
	int id = 0;
	std::string data = "";

	MetricLog(int id = 0, std::string data = "") :id(id), data(data) {}
};

class PLUGIN_API UserMetricsLogger
{
public:

	void LogCheckpointTime(float time) { cLogger.addTime(time); }

	void WriteUserMetricsToFile();
	void ClearUserMetricsFile(String str);

	void SetDefaultWritePath(String str) {defaultPath = str.data;}
private:
	std::vector<MetricLog> logData;
	std::string defaultPath = "LatestUserMetrics.txt";
	CheckpointLogger cLogger;
};

