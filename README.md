# Roost
Team 18:  Lisa Campbell, Zhuoya Chen, Joseph Khawly, Andrew Ring, David Worley, Daniel Piotti

Some interests or activities require more than one person to do them, and sometimes finding other people who want to do the same activity is hard.  We aim to solve this problem by providing a way to locate others in the area with the same interest or activity.  This app would be unique because it will be cross-platform, and we will help create ways to manage groups of people with the same interest, which is different from our main competitor, InCommon, that only helps people find a friend who has more than one common interest with the other person in question.

## Deployment Information:
### Local Deployment - Visual Studio
For local deployment, the server deploys to port 60756.  To deploy using Visual Studio, use IIS Express run button inside of Visual Studio.  
If it doesn't build, manage NuGet packages and add React.AspNet, Microsoft.NETCore.App, and Microsoft.NETCore.Mvc
### Local Deployment - Command Line
If you'd like to run it via your terminal...
Then go to the directory with the repo and go to `/RoostApp/src/RoostApp`
Type the following: `dotnet publish -c Release -r osx`
Note:  Must have dotnet cli installed for this to work...if you have issues, run `dotnet restore`
To deploy, run `dotnet run` after that and it'll tell you which port it deploys to.
