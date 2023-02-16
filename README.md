# QR-Reader-Desktop
## Basic qr reader and creator for windows.

### How to create
1. Click `Create` button in the menu and choose what type of QR you want to create
2. Input required data in the corresponding fields
3. Here you go
----

To retrieve your QR code press right mouse button on an image and you'll see two options for saving the QR code, there are also shortcuts for them.

### How to scan

#### There are three available ways to scan QR code: clipboard, file and camera.

##### 1. Clipboard

Press `Ctrl` + `V` on main window to paste image with QR code, if image doesn't contain QR code it will display "No results"

> **Note**: You can also quick create QR code by copying desired text and pressing `Ctrl` + `V` on the main window

##### 2. File

Click `Scan` button in the menu and choose `From file` and select one or multiple images with QR codes, if you select image that doesn't contain QR code it will still be displayed inside of an app, and if you select **NOT** an image it will display an error indicating which file is not an image.

##### 3. Camera

   1. Click `Scan` button in the menu and choose `From camera`.
   2. Choose your camera
   3. Click `Start`

The program will automatically detect any QR codes, close window and display result.

> **Note**: OBS Virtual Camera **doesn't work** because of the specifics of AForge library.

### How to customize
#### Click `Settings` button in the menu and customize whatever you want: color, icon, size, outline, padding, and resolution.
##### Main colors
- Front color — color of squares. Black is default
- Back color — background color. White is default
##### Icon
- File — the image that will be placed in center, aka your logo, can be png, jpg and bmp formats.
- Pixels per unit — resolution of the image, the bigger - the better, but also the higher memory and cpu usage when creating QR codes. Default is 20
- Icon fill percentage — how much of the final image will be replaced by icon in the center. 100% — QR image = icon, 0% — no icon. Default is 15
- Icon border radius — Icon outline width. Default is 0
- Icon border color — Icon outline color. White is default.
##### Other
- Add padding — adds some padding around QR code. By default is enabled.

---

Additionally you can create start menu shortcut by clicking `About` button in the menu and pressing `Create start menu shortcut` button.

You can later remove it by pressing that button again.
