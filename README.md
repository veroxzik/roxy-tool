# Roxy Tool

This is a simple tool to configure the Roxy and arcin boards.

## Current Release

The current version is available [on the release page](https://github.com/veroxzik/roxy-tool/releases).

## Configuration Options

See [the wiki](https://github.com/veroxzik/roxy-tool/wiki) for a description of the configuration options.

## Flashing

With either Roxy or arcin, there is the ability to flash a new firmware using the built-in bootloader. This bootloader may be activated via HID reports, or by holding Button 1 and Button 2 while plugging in the board.

Select an *.elf file to be loaded and press "Flash to Chip". If this button is not enabled, then the program has not detected an appropriate board.

**NOTICE:** Linux may not detect the board after it has been reset to bootloader via software. Use the "Check for Devices" button to manually force a re-enumeration of attached devices. The program will automatically flash once if it has been commanded to.

This program will *not* flash binaries that have already been combined into an *.exe. You should just use the *.exe.