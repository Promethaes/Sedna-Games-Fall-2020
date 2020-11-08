#include "UserMetricsLogger.h"
#include <fstream>
#include <cstdlib>
#include <time.h>



void UserMetricsLogger::WriteUserMetricsToFile()
{
	srand(time(0));
	logData.push_back(MetricLog(rand(),cLogger.timesToString()));
	cLogger.clearCheckpointTimes();
	std::ofstream file(defaultPath + "LatestUserMetrics.txt");
	file.clear();

	for (auto x : logData) {


		file << "METRICS LOG ENTRY ID " + std::to_string(rand()) << "\n";
		
		//add more of the different loggers in here

		//checkpoints
		file << "\tCHECKPOINTS:\n";
		file << x.data;
	}

	file.close();
}

void UserMetricsLogger::ClearUserMetricsFile(String str)
{
	std::ofstream file(str.data);
	file.clear();
	file.close();
}
