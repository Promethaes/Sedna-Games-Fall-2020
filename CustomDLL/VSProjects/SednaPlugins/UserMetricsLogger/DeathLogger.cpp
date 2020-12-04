#include "DeathLogger.h"

void DeathLogger::addDeath(CDeath death)
{
	deathLists[death.playerNum - 1].push_back(death);
}

std::string DeathLogger::toString()
{
	std::string outString = "";
	for (int j = 0; j < deathLists.size(); j++) {
		outString += "\t\tP" + std::to_string(j + 1) + "Deaths:\n";

		for (int i = 0; i < deathLists[j].size(); i++) {
			std::string temp = "\t\t\t - Death #" + std::to_string(i + 1) + ":\n";
			temp += "\t\t\t\t - Time of Death: " + std::to_string(deathLists[j][i].timeOfDeath) + "\n";
			temp += "\t\t\t\t - Cause of Death: " + deathLists[j][i].causeOfDeath + "\n";
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

CDeath::CDeath(Death& death) :playerNum(death.playerNum),timeOfDeath(death.timeOfDeath)
{
	causeOfDeath = std::string(death.causeOfDeath);
}
