#pragma once
#include <vector>
#include <string>
class CheckpointLogger {
public:

	void addTime(float time) { checkpointTimes.push_back(time); }

	std::string timesToString();
	void clearCheckpointTimes() { checkpointTimes.clear(); }

private:
	std::vector<float> checkpointTimes;

};