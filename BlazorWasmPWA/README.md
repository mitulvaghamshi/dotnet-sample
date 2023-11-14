# Walkthrough 22 - Blazor WebAssembly PWA

## Setup

- Click `Create a new project`.
- Select the `Blazor WebAssembly App` template, click `Next`.
- Set Project name to `BlazorWasmPWA`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is not selected, click
  `Next`.
- Set version to `.NET 5.0`, and:
  - unselect `Configure for HTTPS`,
  - unselect `ASP.NET Core hosted`,
  - select `Progressive Web Application`
- Click `Create`.

## index.html

- Open `wwwroot/index.html`.
- In the head section, notice the _manifest_ and _icon_ references.
- Scroll to the bottom of the file and notice `script` tag for the
  _service-worker_.

## manifest.json

- Open `wwwroot/manifest.json`.
- Inspect it.

## service-worker.js

- Open `wwwroot / service-worker.js`
- Notice how simple it is.
- This version of the file is used during development.
- In Solution Explorer, expand `service-worker.js` and select
  `service-worker.published.js`.
- Briefly inspect it.
- This version of the file is used in production.

## Running the PWA

- Run the site and test each of the 3 pages.
- The functionality is identical to `BlazorServerIntro`, `BlazorWasmIntro` and
  `BlazorWasmServer`.
- Depending on the browser, there will be a button to _install the app_.
- In Chrome and Edge, the button is at the right-end of the address bar.
- Click the button and click `Install`.
- The app will appear in its own window.
- When installing, you will be given options on how to install the app.
- Select pin to Start Menu and any other options you wish.
- Close the browser and close the app.
- While in development, as long as your Visual Studio web server is running,
  represented by an icon in the taskbar notification area, the PWA will work.
- Open the app again.
- Close the app.
- Access the Windows menu, right-click `BlazorWasmPWA` and select `uninstall`.
