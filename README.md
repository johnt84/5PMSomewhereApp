# 5PMSomewhereApp

Simple app which answers the question of where in the world is it currently 5 PM.

App shows either the current countries where it is currently 5 PM if its currently on the hour or its shows which countries it was just 5 PM and the countires where it is just about to turn 5 PM.  The main heading shows a random country from the list of countires returned for the current or previous countries where it turned 5 PM.

There is share functionality which allows you to share where it is or was 5 PM via sharing apps such as Social Media (Facebook/Twitter, etc), WhastsApp or Email.

App is a Blazor application in .Net 8.

* Uses MudBlazor for the UI components
* Utilises the Rest Countries API to get a list of countries for the timezones
* Utilises the Dot Net TimeZoneInfo class to find which timezone it is/was and going to be be 5 PM