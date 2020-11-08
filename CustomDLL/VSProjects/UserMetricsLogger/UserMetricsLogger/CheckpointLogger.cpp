#include "CheckpointLogger.h"

std::string CheckpointLogger::toString()
{
	std::string outString = "";
	for (int i = 0; i < checkpointTimes.size(); i++) {
		std::string temp = "\t\t - Checkpoint #" + std::to_string(i + 1) + ":";
		temp += std::to_string(checkpointTimes[i]) + "\n";
		outString += temp;
	}

	return outString;
}
