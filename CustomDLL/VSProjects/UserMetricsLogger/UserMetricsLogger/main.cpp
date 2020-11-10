#include "UserMetricsLogger.h"
#include <ctime>

#ifdef _DEBUG

int main() {

	UserMetricsLogger logger;
	String str;
	str.data = "./";
	logger.SetDefaultWritePath(str);
	for (int i = 0; i < 5; i++) {

		logger.LogCheckpointTime(69);

		Death death;
		death.causeOfDeath = "Big dead disease";
		death.playerNum = 1;
		death.timeOfDeath = time(0);

		logger.LogDeath(death);
		logger.WriteUserMetricsToFile();
	}

	return 0;
}
#endif