Space Battle Arena - .Net Core
==============================

![Build Badge](https://codebuild.us-west-2.amazonaws.com/badges?uuid=eyJlbmNyeXB0ZWREYXRhIjoidVRpbVB3aXM3VVp0WEI0Q0xjTDNCdXJCYmZSK2Q3M3hLMnRhYmJmZm5ET21wYzZLVVlLMjhneWIzcjdkc1RyUHoyVnQwWkVzTVB5bzhnaVdQeUtMKzBRPSIsIml2UGFyYW1ldGVyU3BlYyI6IlIwQk5TMjFJaHEvSk9ybkoiLCJtYXRlcmlhbFNldFNlcmlhbCI6MX0%3D&branch=master)

Space Battle Arena is a ‘[Programming Game](http://en.wikipedia.org/wiki/Programming_game)‘ where you must write code to autonomously control a space ship to accomplish specified objectives.  

To find out more visit our **[GitHub Website](http://mikeware.github.io/SpaceBattleArena)**.  Complete Learning materials and other guides are available there as well.

Space Battle Arena is [licensed](LICENSE) under the GPLv2.

Student Environment
-------------------
It is expected that students have completed a full year of programming in high school or just over a semester of programming at the college level.

We use [Visual Studio Code](https://code.visualstudio.com/) as our IDE of choice when working with High School students, but any C# IDE that supports Nuget package references can be used.

Getting Started
---------------

To start a new spaceship project, please make sure the .Net Core SDK is installed on your system, and with a sufficiently high version number. To do so, start a command prompt and type `dotnet --version`. You should see somethng like the following:

```
2.2.202
```

If the command isn't recognized, or your version number is lower than 2.2, please visit https://dotnet.microsoft.com/download to downlaod and install the latest version. Once the SDK is installed, run the following commands:

```
mkdir spaceship_1 && cd spaceship_1
dotnet new console
dotnet add package SBA_Client.Net
```

Now you're *almost* ready to start coding!  First, you'll need to add the following boilerplate code to the file `Program.cs` which was created by the above commands:

```
using System;
using System.Collections.Generic;
using System.Drawing;
using SpaceBattleArena;

namespace APCS
{
    class MyShip : BasicSpaceship {
	override public RegistrationData registerShip(int numImages, int worldWidth, int worldHeight)
        {
            return new RegistrationData("My Cool Ship!", Color.White, 0);
        }

	override public ShipCommand getNextCommand(BasicEnvironment env)
        {
            return new IdleCommand(.1);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var ship = new MyShip();
            TextClient<BasicGameInfo>.run("127.0.0.1", ship, 2012);
        }
    }
}
```

To build and run your ship, type `dotnet run`.  You will likely need to change the IP address 127.0.0.1 to the one provided by your instructor, unless you're running the SBA_Serv.exe application locally for testing (in which case, leave it be).

Resources
---------
* [Initial Guides](http://mikeware.github.io/SpaceBattleArena/client/guides/)
* [Server Setup](http://mikeware.github.io/SpaceBattleArena/server/)
* Talks
    * [You Have Died of Dysentery: Games in Education Are Still Alive - PAXDev 2014](http://www.mikeware.com/2014/08/you-have-died-of-dysentery-games-in-education-are-still-alive/)
    * [Reach for the Stars - PAXDev 2012](http://www.mikeware.com/2012/09/reach-for-the-stars-educating-the-next-generation-using-games/)
* [Development](http://mikeware.github.io/SpaceBattleArena/dev)

