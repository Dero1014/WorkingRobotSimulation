# Educational robot simulation  :robot:

Create a simulation of a working environment with robots in mind and apply sensors and programming to make an efficent work space. A student project.

## Table of contents

- [About](#about)
- [Features](#features)
- [Setting up a working environment](#setting-up-a-working-environment)
  - [Camera](#camera)
  - [Adding Items](#adding-items)
  - [Inspector](#inspector)
- [Robot](#robot)
  - [Manipulating the robot](#manipulating-the-robot)
  - [Programming the robot](#programming-the-robot)
    - [Syntax](#syntax)
- [About](#about)

## About

Educational robot simulation or ERS is a student project with the prupouse to teaching the use of robotics in industry working environments, focus is on programming, standards, rules and how to setup a robot in a specific working environment. It offers the use of various items such as treadmills, sensors and robots themeselves as well as a basic programming language for the robots and a roboust editor to create your industry environment. 

## Features
- Gripper robots
- Treadmill
- Sensors (laser and capacitive)
- Editor
- Inspector
- Custom programming language for robots

## Setting up a working environment

The editor features camera controls, inspector and a menu to add items to the scene.

### Camera

The camera has basic controls similar to that of the unity engine.

- RIGHT MOUSE BUTTON - Camera rotation
- LALT - Camera Focus on object
- MIDDLE MOUSE BUTTON - Camera pan
- LCTRL + MOUSE WHEEL - Camera move forward/backward
- MOUSE WHEEL - zoom

### Adding Items

The editor has a basic menu of items that the user can pick and place around.

[Image of the menu]

### Inspector

While selecting an object it will tell its transform and additional options that can be changed for the particular item.

[image of the inspector]

## Robot

The robot that is provided is a simple gripper robot. Having 6 axis of movement it also has a gripper that can grab and release objects.

### Manipulating the robot

The robot can be manipulated to establish positions for users preference, these positions can later be used in code to call on the robot which position to move onto next.

[Show image of robot being manipulated with gizmo]

### Programming the robot

Selecting any robot will open up its programming interface. The programing language is a compilation of CNC and Basic offering simple to use commands but enough complexity to create more elaborate procedures.

#### Syntax

- INT - the only variable that can be declared, a simple interger
- Simple mathematical operations
-  MPN - Move to Point N, moves the robot arm to a new position (N)
-  WNS/WNMS - Wait for N Seconds or Wait for N MicroSeconds, halts the program for N seconds or microseconds
-  SN - Speed N, sets the speed of the robot (N), default being 30
-  G and R - Grab and Release, specific commands for the robot to grab or release and object
-  IF(condition) - Simple IF block, closes with IEND
-  LOOP - Infinite while loop, breakes with BREAK and closes with LEND
-  REPEAT(N) - For loop where N is the amount of repetition, closes with REND

## Student continuation

While the project was finished for the sake of graduation, the project did live longer by handing it over to students to have fun and learn new things in programming and robotics, where they did an excelent job. You can see more about it [here](https://github.com/Dero1014/Robot_Simulation_Students).

