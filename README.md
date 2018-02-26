# Time Bomb

This is a Unity project for a mobile based game.

## Build Instructions

### Android/iOS

Build as per Unity's instructions.

### Headless (for server)

#### Building

The server is deployed through Docker.

To build the server executable, open `File > Build Settings` and select `PC, Mac & Linux Standalone`.

Ensure the following settings are used:

    Target Platform is Linux
    Architecture    is x86_64
    Headless Mode   is ticked

Save the executable in the `bin` folder on the [TimeBomb Server](https://bitbucket.org/tangrs/timebomb-server) repository - overwriting the existing binary. Add and commit the binary to the repository as usual.

### Codebase

The root of our codebase is located in the Assets directory.

### Testing

All testing is run WITHIN the Unity editor:

Integration tests can be found on the scene IntegrationScene found in folder 'Integration Test'

Unit testing can be accessed by going to Window -> Editor Tests Runner. From there you can run all of our unit tests.
The location of the unit tests is UnityTestTools/UnitTesting/Editor from the root of the project.

See the [TimeBomb Server](https://bitbucket.org/tangrs/timebomb-server) repository README for more information

