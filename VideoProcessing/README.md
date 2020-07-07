# Tracking Fingers in a video stream
This project uses [OpenCvSharp](https://github.com/shimat/opencvsharp), a wrapper for .NET to find fingers in the frame of a video frame and an implementation of the nearest neighbour algorithm to keep track of them.\
The main program isolates touches in the video by using background subtraction, blur and segmentation methods provided by OpenCV, while the rest of the classes listed below are responsible for tracking and sending them.\
\
Quick Overview about the important classes:
 - [Blob](./Processing/Blob.cs): Used for detecting finger blobs after the frame has been processed. Simple class that only holds x and y coordinates.
 - [Touch](./Processing/Touch.cs): Smarter version of Blob, has a unique id and keeps track of its age and path.
 - [Tracker](./Processing/Tracker.cs): Manages a list of current touches and adds/updates/removes them based on a nearest neighbour algorithm. 
 - [TuioSender](./Util/TuioSender.cs): Sends OSC bundles of all current touches using an OSC transmitter.
 - [External](./External): This folder would usually contain a TUIO- and OSC implementation, however for this project, the TUIO one was not needed.