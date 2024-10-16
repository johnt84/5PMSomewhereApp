# 5 PM Somewhere Blazor App

Simple web app which answers the universal question of where in the world is it currently 5 PM.

The app shows either the current countries where it is currently 5 PM if its currently on the hour or its shows which countries it was just 5 PM and the countries where it is just about to turn 5 PM.  The main heading shows a random country from the list of countires returned for the current or previous countries where it turned 5 PM.

There is also share functionality which allows you to share where it is or was 5 PM via sharing apps such as Social Media (Facebook/Twitter, etc), WhastsApp or Email.

Web App is developed as Blazor application in .Net 8/C#.

* Uses MudBlazor for the UI components
* Utilises the Rest Countries API to get a list of countries for the timezones
* Utilises the Dot Net TimeZoneInfo class to find which timezone it is/was % PM and which timezone its about to turn 5 PM
