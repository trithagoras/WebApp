# Simple WebApp

## Task
Create a web application that allows a user to request a random image that the user can like or dislike

## User story
1. A random image is displayed to the user
2. User likes or dislikes the image
3. Preference is sent to the server to be persisted
4. The user can request a new random image or view the history of previously
liked or disliked images

## Serve instructions
1. navigate to `/API/`
2. run `dotnet ef migrations add InitialCreate`
3. run `dotnet ef database update`
4. run `dotnet run`
5. navigate to `/ui/`
6. run `npm install`
7. run `npm start`