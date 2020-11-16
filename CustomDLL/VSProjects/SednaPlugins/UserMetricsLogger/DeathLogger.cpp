#include "DeathLogger.h"

void DeathLogger::addDeath(Death death)
{
	switch (death.playerNum) {
	case 1:
		deathLists[0].push_back(death);
	case 2:
		deathLists[1].push_back(death);
	case 3:
		deathLists[2].push_back(death);
	case 4:
		deathLists[3].push_back(death);
	}
}

std::string DeathLogger::toString()
{
	std::string outString = "";
	for (int j = 0; j < deathLists.size(); j++) {
		outString += "\t\tP" + std::to_string(j + 1) + "Deaths:\n";

		for (int i = 0; i < deathLists[j].size(); i++) {
			std::string temp = "\t\t\t - Death #" + std::to_string(i + 1) + ":\n";
			temp += "\t\t\t\t - Time of Death: " + std::to_string(deathLists[j][i].timeOfDeath) + "\n";
			temp += "\t\t\t\t - Cause of Death: " + std::string(deathLists[j][i].causeOfDeath) + "\n";
			outString += temp;
		}
	}

	return outString;
}

void DeathLogger::clearDeaths()
{
	for (auto& x : deathLists) 
		x.clear();
	
	
}
