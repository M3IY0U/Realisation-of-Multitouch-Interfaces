# Realisation of Multitouch Interfaces
This repository contains the practical for the module [Construction of Multi-Touch and Multi-User Interfaces](https://obs.fbi.h-da.de/mhb/modul.php?nr=30.2578)
and was implemented during the summer semester 2020. 
## Part 1: Finger Tracking
The first part of the practical consists of using various filters to detect fingers and tracking their movements using a nearest neighbour algorithm.
## Part 2: Gesture Recognition/Project
The second part of the practical is a  music/media controller written in C#. It uses WinForms, [TUIO](http://tuio.org/) and a custom port of the [$1 Recognizer](http://depts.washington.edu/acelab/proj/dollar/index.html).\
The user can use certain gestures to pause the music, or skip to the previous/next track. 
Each action can be hooked up to a gesture from the samples we were provided with, but it is also possible to record custom gestures and use those.
