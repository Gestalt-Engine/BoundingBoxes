# **_Bounding Boxes User Guide_**

#### **Overview**
This guide should get a first time user up and running

[BoundingBoxes is open source](https://github.com/Gestalt-Engine/BoundingBoxes)

#### **Installation**
This package only includes scripts, so adding the package to your project
should be sufficient unles you wish to see examples.

#### *Quick Start*
You can add the demo WASDCamera script to your Camera in a scene via "*Add Component->Scripts->WASDCamera*"
This implements a basic 2D WASD Camera movement script with the bounding box
defining its boundaries.

You can implement the bounding box manually by inheriting from it in your class:

```
using BoundingBoxes;
public class SomeClass : BoundingBox {}
```

It already implements MonoBehavior so you do not need that inheritance.

### **Support & Documentation**
Should you have any questions, suggestions, or require assistance, please contact us via the Asset Store.

[Online Documentation](http://www.gestaltengine.com/bounding-boxes-documentation/)
