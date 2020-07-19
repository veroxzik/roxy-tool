# Roxy Tool

This is a simple tool to configure the Roxy and arcin boards.

## Current Release

The current version is [v0.3.1](https://github.com/veroxzik/roxy-tool/releases).

## Configuration Options (Roxy)

Below is a list of options that can be configured on Roxy. See below for arcin.

| Option | Description |
| :----: | ----------- |
| Board Label | Appends an additional string to the device name (max 11 characters). |
| Hide Serial Number | Hides the serial number associated with the board. |
| Invert QE1 | Inverts the direction of QE1. |
| Invert QE2 | Inverts the direction of QE2. |
| LED1 always on | Turns LED1 on. It remains off otherwise. |
| Analog Input (Not QE) | Switches the analog inputs to use the ADC, commonly used for "SDVX AC Knobs" or other potentiometers. |
| Enable Analog Buttons | Enables conversion of Analog inputs into Joystick buttons. This option is for games where the Joystick Axes are not supported. The Axes are not disabled. |
| QE1 Sensitivity | Changes ratio that is applied to QE1's rotation. Set this so one rotation of your encoder is as close to 1 full swing through the Axis as possible. |
| QE2 Sensitivity | Same as above for QE2. |
| PS2 Mode| Enables PS2 support through the SPI2 port. **This is not currently implemented.** |
| RGB Interface | Selects the method used to output to RGB lights. |
| RGB Brightness | Sets the brightness of the RGB lights. The max value is 255. |
| Debounce Time | Sets the debounce value, in milliseconds. This is a leading edge debounce, where after a state has changed, it cannot change again for the duration of the debounce. |
| RGB Mode | Sets the mode of the RGBs. HID is always active for the RGBs if the appropriate values are set. |
| RGB Color | Set either the color of the on-board RGB connectors in RGB Mode 1 or of the chasing lights in RGB Mode 2. |
| ASC Emulation | Sets the controller VID/PID/Poll rate to match that of the official Konami controllers. Useful for playing Infinitas or SDVX Cloud. |
| Axis Debounce Time | Sets the axis button debounce value, in milliseconds. Only functional if "Enable Analog Buttons" is enabled. This is again a leading edge debounce. |

## Configuration Options (arcin running Roxy firmware)

Below is a list of options that can be confirmed if your arcin is flashed using the Roxy firmware.

| Option | Description |
| :----: | ----------- |
| Board Label | Appends an additional string to the device name (max 11 characters). |
| Hide Serial Number | Hides the serial number associated with the board. |
| Invert QE1 | Inverts the direction of QE1. |
| Invert QE2 | Inverts the direction of QE2. |
| LED1 always on | Turns LED1 on. It remains off otherwise. |
| LED2 always on | Turns LED2 on. It remains off otherwise. |
| Analog Input (Not QE) | Switches the analog inputs to use the ADC, commonly used for "SDVX AC Knobs" or other potentiometers. |
| Enable Analog Buttons | Enables conversion of Analog inputs into Joystick buttons. This option is for games where the Joystick Axes are not supported. The Axes are not disabled. |
| QE1 Sensitivity | Changes ratio that is applied to QE1's rotation. Set this so one rotation of your encoder is as close to 1 full swing through the Axis as possible. |
| QE2 Sensitivity | Same as above for QE2. |
| PS2 Mode| Enables PS2 support through the SPI2 port. **This is not currently implemented.** |
| WS2812B Mode | Selects whether to use the ws2812b interface on B9. |
| Debounce Time | Sets the debounce value, in milliseconds. This is a leading edge debounce, where after a state has changed, it cannot change again for the duration of the debounce. |
| ASC Emulation | Sets the controller VID/PID/Poll rate to match that of the official Konami controllers. Useful for playing Infinitas or SDVX Cloud. |
| Axis Debounce Time | Sets the axis button debounce value, in milliseconds. Only functional if "Enable Analog Buttons" is enabled. This is again a leading edge debounce. |

## Configuration Options (arcin)

Below is a list of options that can be configured on the standard arcin firmware. Some options may not function depending on the build loaded.

| Option | Description |
| :----: | ----------- |
| Board Label | Appends an additional string to the device name (max 11 characters). |
| Hide Serial Number | Hides the serial number associated with the board. |
| Invert QE1 | Inverts the direction of QE1. |
| Invert QE2 | Inverts the direction of QE2. |
| LED1 always on | Turns LED1 on. It remains off otherwise. |
| LED2 always on | Turns LED2 on. It remains off otherwise. |
| Analog Input (Not QE) | Switches the analog inputs to use the ADC, commonly used for "SDVX AC Knobs" or other potentiometers. |
| QE1 Sensitivity | Changes ratio that is applied to QE1's rotation. Set this so one rotation of your encoder is as close to 1 full swing through the Axis as possible. |
| QE2 Sensitivity | Same as above for QE2. |
| PS2 Mode| Enables PS2 support through the SPI2 port. |
| WS2812B Mode | Selects whether to use the ws2812b interface on B9. |

## Flashing

With either Roxy or arcin, there is the ability to flash a new firmware using the built-in bootloader. This bootloader may be activated via HID reports, or by holding Button 1 and Button 2 while plugging in the board.

Select an *.elf file to be loaded and press "Flash to Chip". If this button is not enabled, then the program has not detected an appropriate board.

**NOTICE:** Linux may not detect the board after it has been reset to bootloader via software. Use the "Check for Devices" button to manually force a re-enumeration of attached devices. The program will automatically flash once if it has been commanded to.

This program will *not* flash binaries that have already been combined into an *.exe. You should just use the *.exe.