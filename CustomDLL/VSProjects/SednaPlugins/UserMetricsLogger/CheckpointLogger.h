#pragma once
#include <vector>
#include "Logger.h"
class CheckpointLogger : public Logger {
public:

	void addTime(float time) { checkpointTimes.push_back(time); }

	std::string toString() override;
	void clearCheckpointTimes() { checkpointTimes.clear(); }

private:
	std::vector<float> checkpointTimes;

};