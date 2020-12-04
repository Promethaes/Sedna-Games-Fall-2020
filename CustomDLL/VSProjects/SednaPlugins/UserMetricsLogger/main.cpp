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

		Death death = Death("ah",2.0f,1);

		logger.LogDeath(death);
		logger.WriteUserMetricsToFile();
	}

	return 0;
}
#endif