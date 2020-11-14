#pragma once
#include <string>
struct String {
	const char* data;
};
struct Death {
	float timeOfDeath;
	const char* causeOfDeath;
	int playerNum;

	Death() = default;
	Death(const char* cause, float time, int num) :causeOfDeath(cause), timeOfDeath(time), playerNum(num) {}
	~Death() {

	}
};
struct MetricLog {
	int id = 0;
	std::string data = "";

	MetricLog(int id = 0, std::string data = "") :id(id), data(data) {}
};