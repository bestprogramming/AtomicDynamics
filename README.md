# Atomic Dynamics
This project investigates the interaction between elementary particles and atomic nuclei.
The aim of the project is to simulate electron orbits.
For example, it is to visualize the 8 electrons being held around the nucleus of an oxygen atom without any dispersion.

## How it works
At the most basic level, calculations are done with large numbers. The integer part of the number can go to infinity, but the decimal part (default 200) is limited.
Physical constants are defined by these numbers.
For example Plank constant: <br><br>
public static readonly Big H = Parse("6.62607015") * Pow(10, -34);<br><br>
The forces acting on all objects in a small time interval are calculated. The motion of individual objects in this small time interval is calculated and simulated.

https://www.youtube.com/watch?v=r1ZsNVisYeQ<br>
https://www.youtube.com/watch?v=5OY9VKzKv8U

## Categories
- **Big numbers**: Contains basic mathematical operations with large numbers.
- **Physics constants**: Physics constants are determined with large numbers
- **Elementary particles**: The gravitational and electromagnetic forces that fundamental physics particles and atomic nuclei exert on each other are calculated.
- **Physics simulation**: As a result, simulation is performed with OpenGL.
