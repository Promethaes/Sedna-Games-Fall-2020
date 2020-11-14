#include "Gamepad.h"
#include <iostream>
int main() {
	SednaInput::GamepadManager xinput;


	while (1) {

		xinput.UpdateGamepadList();
		std::cout << xinput.getNumGamepads();
	}

	return 0;
}