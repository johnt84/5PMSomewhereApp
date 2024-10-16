# 5 PM Somewhere Blazor App

Simple web app which answers the universal question of where in the world is it currently 5 PM.

The app shows either the current countries where it is currently 5 PM if its currently on the hour or it shows which countries it was just 5 PM within the last hour and the countries where it is just about to turn 5 PM within the next hour.  The title heading shows a random country from the list of countries where it is or was just 5 PM.

![image](https://github.com/user-attachments/assets/705e7446-ceaa-4df9-b1d6-1aabc6df7b3d)

There is also a share button which allows you to share where it is or was 5 PM via sharing apps such as Social Media (Facebook/Twitter, etc), WhatsApp or Email.

![image](https://github.com/user-attachments/assets/737ad359-a972-49e2-a695-83ed5b7aeac6)

The web App is developed as a Blazor application in .Net 8 / C#.

* Uses MudBlazor for the UI components
* Utilises the Rest Countries API to get a list of countries for the current/previous and next timezones
* Utilises the Dot Net TimeZoneInfo class to find which timezone it is/was 5 PM and which timezone it is about to turn 5 PM in
