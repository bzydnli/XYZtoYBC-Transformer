Bend Calculator: XYZ to YBC Transformation

This C# application calculates bend angles, rotation angles, and segment lengths from a sequence of XYZ coordinates.  
It is particularly useful for pipe bending operations, CNC applications, and mechanical design where spatial transformation matters.

Features

- Computes bend angles between three consecutive points (BA and BC)
- Adjusts straight lengths based on the tangent of half the bend angle
- Calculates rotation angles between bending planes using vector math (cross/dot products)
- Organized with helper methods for readability and reuse

Example Use Case

Used in systems where precise bending path must be derived from XYZ measurements, e.g., robotic arms, CNC pipe bending, 3D fabrication processes.


Personal Note

> This algorithm reflects not only spatial geometry, but my fascination with how mathematical intuition becomes programmable structure.  
It’s a quiet form of transformation — from shape to logic, from vision to execution.
