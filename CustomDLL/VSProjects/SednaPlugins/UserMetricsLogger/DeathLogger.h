#pragma once
#include <vector>
#include "Structs.h"
#include "Logger.h"

class DeathLogger : public Logger
{
public:
	DeathLogger() {
		deathLists.push_back(std::vector<Death>());
		deathLists.push_back(std::vector<Death>());
		deathLists.push_back(std::vector<Death>());
		deathLists.push_back(std::vector<Death>());
	}
	void addDeath(Death death);
	std::string toString() override;
	void clearDeaths();

private:
	std::vector<std::vector<Death>> deathLists;
};

