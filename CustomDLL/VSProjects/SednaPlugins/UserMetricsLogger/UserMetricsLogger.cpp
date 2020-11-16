#include "UserMetricsLogger.h"
#include <fstream>
#include <cstdlib>
#include <time.h>



void UserMetricsLogger::WriteUserMetricsToFile()
{
	srand(time(0));
	int id = rand();
	logData.push_back(MetricLog(id, cLogger.toString()));
	logData.push_back(MetricLog(id, dLogger.toString()));
	cLogger.clearCheckpointTimes();
	dLogger.clearDeaths();
	std::ofstream file(defaultPath + "LatestUserMetrics.txt");
	file.clear();

	for (size_t i = 0; i < logData.size(); i += NUM_LOGGERS) {

		file << "METRICS LOG ENTRY ID " + std::to_string(logData[i].id) << "\n";

		//checkpoints
		file << "\tCHECKPOINTS:\n";
		file << logData[i].data;

		//deaths
		file << "\tDEATHS:\n";
		file << logData[(i+1)].data;
	}

	file.close();
}

void UserMetricsLogger::ClearUserMetricsFile(String str)
{
	std::ofstream file(str.data);
	file.clear();
	file.close();
}
