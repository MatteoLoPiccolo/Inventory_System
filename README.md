# Inventory_System

This project is a small-scale implementation of a Shop/Inventory system, focusing on the architecture and core functionality of buying and selling items. My primary objective is to explore and apply several design patterns and architectural concepts I've learned, such as:

    Singleton
    MVC (Model-View-Controller)
    Service Locator
    Events and the Observer Pattern
    Basic Dependency Injection

UI Management

For the user interface, I've worked on managing elements using Rect Transforms, Horizontal and Vertical Layout Groups, Layout Elements, and Canvas Groups.
Current Progress

The project is still a work in progress, and I'm continuously improving it as I learn more about handling data and refining the design patterns. Some concepts are easier to grasp, while others require more time and experimentation to fully understand their usefulness in the larger architecture of the application.

Although this is mainly a test project, I'm committed to writing clean and efficient code to make it as good as possible.
Features

    Inventory and Shop Toggle: Pressing I toggles between the inventory and the shop.
    Item Selection: Use the left mouse button to select items from the shop list (currently, there are 4 items in the shop, with a total of 7 slots to demonstrate empty ones).
    Purchasing Items: Right-click on an item to open a popup and buy the item. Currently, you can only buy one unit at a time, but I plan to add the ability to buy multiple units in the future.
    Inventory Update: When switching from the shop to the inventory, the purchased items and their quantities are displayed.
    Selling Items: Right-clicking on an item in the inventory opens a popup, allowing you to sell items back to the shop.
    Money Management: Both player and shop finances are managed via events, which update dynamically as transactions occur.

To Do

    Complete the selling feature (still in progress).
