#pragma once
#include <vector>
#include "Structs.h"
#include "Logger.h"

class CDeath {
public:
	CDeath(Death& death);

	std::string causeOfDeath = "";
	int playerNum = 1;
	float timeOfDeath = 0.0f;

};

class DeathLogger : public Logger
{
public:
	DeathLogger() {
		deathLists.push_back(std::vector<CDeath>());
		deathLists.push_back(std::vector<CDeath>());
		deathLists.push_back(std::vector<CDeath>());
		deathLists.push_back(std::vector<CDeath>());
	}
	void addDeath(CDeath death);
	std::string toString() override;
	void clearDeaths();

private:
	std::vector<std::vector<CDeath>> deathLists;
};

