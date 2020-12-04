#include "Structs.h"

Death::Death(const char* cause, float time, int num) : timeOfDeath(time), playerNum(num)
{
	causeOfDeath = cause;
}

Death::~Death()
{
}
