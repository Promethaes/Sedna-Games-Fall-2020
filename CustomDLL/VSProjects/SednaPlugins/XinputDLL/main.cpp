#include "GamepadManager.h"
#include <iostream>

#ifdef _DEBUG
int main() {

	SednaInput::GamepadManager manager{};

	while (1) {
		manager.UpdateGamepadList();
		auto f = manager.GetLeftStickValue(0).x;
		std::cout << f << "\n";
	}

	return 0;
}
#endif